using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BUILDING_CREATOR : EditorWindow
{
	int tool = 2;

	int current_tile = 0;
	Vector2 scroll_position;

	int size_x = 0;
	int size_y = 0;
	int[,] array = new int[0,0];
	float richness;
	string building_name;
	bool show_settings = false;

	[MenuItem("Custom/Building Editor")]
	public static void ShowWindow()
	{
		GetWindow(typeof(BUILDING_CREATOR));
	}

	void OnGUI()
	{
		TILE_DATA[] tile_data = UTILITY.FindAssetsByType<TILE_DATA>().ToArray();

		///TOOLBAR
		EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
		{
			if (GUILayout.Button("Save", EditorStyles.toolbarButton)) Save();
			if (GUILayout.Button("Load", EditorStyles.toolbarButton)) Load();
			if (GUILayout.Button("New", EditorStyles.toolbarButton)) New();
			GUILayout.Space(5);
			if (GUILayout.Button("Zoom -", EditorStyles.toolbarButton)) Zoom_Out();
			if (GUILayout.Button("Zoom +", EditorStyles.toolbarButton)) Zoom_In();
			GUILayout.Space(5);
			if (GUILayout.Toggle(tool == 0, "Remove", EditorStyles.toolbarButton)) Delete(); 
			if (GUILayout.Toggle(tool == 1, "Dropper", EditorStyles.toolbarButton)) Eye_Drop();
			if (GUILayout.Toggle(tool == 2, "Draw", EditorStyles.toolbarButton)) Draw();
			GUILayout.Space(5);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Refresh", EditorStyles.toolbarButton)) Refresh();
		}
		GUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.BeginVertical();
			{
				//GUILayout.Label("Building Settings", EditorStyles.boldLabel);
				///ARRAY SETTINGS
				show_settings = EditorGUILayout.BeginFoldoutHeaderGroup(show_settings, "Building Settings");
				EditorGUILayout.BeginVertical(EditorStyles.helpBox);
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label("Width : ");
						size_x = EditorGUILayout.DelayedIntField(size_x, GUILayout.Width(50));
						GUILayout.Label("Height : ");
						size_y = EditorGUILayout.DelayedIntField(size_y, GUILayout.Width(50));

						int[,] new_array = new int[size_x, size_y];
						for (int i = 0; i < array.GetLength(0); i++)
						{
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (i < size_x && j < size_y)
									new_array[i, j] = array[i, j];
							}
						}
						array = new_array;
						GUILayout.FlexibleSpace();
					}
					EditorGUILayout.EndHorizontal();

					if (show_settings)
					{
						EditorGUILayout.LabelField("ID", Get_ID().ToString());
						building_name = EditorGUILayout.TextField("Name", building_name);
						richness = EditorGUILayout.Slider("Richness",richness, 0f, 1f);
					}
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndFoldoutHeaderGroup();
				///FIELD
				EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
				{
					for (int i = 0; i < array.GetLength(0); i++)
					{
						EditorGUILayout.BeginHorizontal();
						for (int j = 0; j < array.GetLength(1); j++)
						{
							Texture2D image;
							if (array[i, j] !=-1)
							{
								int sprite_scale = 3;
								image = UTILITY.Resize(tile_data[array[i, j]].sprite, sprite_scale);
							}
							else
							{
								image = new Texture2D(48, 48);
							}
							
							if (GUILayout.Button(image, GUIStyle.none))
							{
								switch (tool)
								{
									case 0:
										array[i, j] = -1;
										break;
									case 1:
										if (array[i, j] != -1)
											current_tile = array[i, j];
										break;
									case 2:
										array[i, j] = current_tile;
										break;
								}
							}
						}
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndVertical();

			///TILE SELECT
			scroll_position = GUILayout.BeginScrollView(scroll_position, GUILayout.Width(200));
			{
				for(int i = 0;i<tile_data.Length;i++)
				{
					EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
					{
						int sprite_scale = 3;
						Texture2D image = UTILITY.Resize(tile_data[i].sprite, sprite_scale);
						if (GUILayout.Toggle(i == current_tile, image, "Button", GUILayout.Width(20 * sprite_scale)))
						{
							current_tile = i;
						}

						EditorGUILayout.BeginVertical();
						{
							GUILayout.Label("Name : ");
							GUILayout.Label("ID : ");
							GUILayout.Label("Weight : ");
						}
						GUILayout.EndVertical();

						EditorGUILayout.BeginVertical();
						{
							GUILayout.Label(tile_data[i].name);
							GUILayout.Label(tile_data[i].id.ToString());
							GUILayout.Label(tile_data[i].weight.ToString());
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndHorizontal();
	}

	void Refresh()
	{
		Repaint();
	}

	void Save()
	{
		BUILDING_DATA asset = CreateInstance<BUILDING_DATA>();
		asset.id = Get_ID();
		asset.data_array = array;
		asset.name = building_name;
		asset.richness = richness;

		AssetDatabase.CreateAsset(asset, "Assets/Resources/building_data/" + building_name + ".asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

	void Load()
	{

	}

	void New()
	{
		array = new int[size_x, size_y];
		for(int i = 0; i < size_x; i++)
		{
			for (int j = 0; j < size_x; j++)
			{
				array[i, j] = -1;
			}
		}

		richness = 0f;
		building_name = "new_building";
	}

	void Zoom_In()
	{

	}

	void Zoom_Out()
	{

	}

	void Delete()
	{
		tool = 0;
	}

	void Eye_Drop()
	{
		tool = 1;
	}

	void Draw()
	{
		tool = 2;
	}

	int Get_ID()
	{
		BUILDING_DATA[] building_data = UTILITY.FindAssetsByType<BUILDING_DATA>().ToArray();
		int previous = 0;

		for (int i = 0; i < building_data.Length; i++)
		{
			bool found = false;
			for (int j = 0; j < building_data.Length; j++)
			{
				if (building_data[j].id == previous + 1)
				{
					found = true;
					previous = building_data[j].id;
					break;
				}
			}

			if (!found)
			{
				previous++;
				break;
			}
		}
		return previous;
	}
}