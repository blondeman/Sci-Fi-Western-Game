using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BUILDING_CREATOR : EditorWindow
{
	int tool = 2;

	int current_tile = 0;
	Vector2 scroll_position;

	int x = 0;
	int y = 0;
	int[,] array = new int[0,0];

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
				GUILayout.Label("Array Settings", EditorStyles.boldLabel);
				///ARRAY SETTINGS
				EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
				{
					GUILayout.Label("Width : ");
					x = EditorGUILayout.DelayedIntField(x, GUILayout.Width(50));
					GUILayout.Label("Height : ");
					y = EditorGUILayout.DelayedIntField(y, GUILayout.Width(50));

					int[,] new_array = new int[x, y];
					for (int i = 0; i < array.GetLength(0); i++)
					{
						for (int j = 0; j < array.GetLength(1); j++)
						{
							if (i < x && j < y)
								new_array[i, j] = array[i, j];
						}
					}
					array = new_array;
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();

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

							if (GUILayout.RepeatButton(image, GUIStyle.none))
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

	}

	void Load()
	{

	}

	void New()
	{

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
}