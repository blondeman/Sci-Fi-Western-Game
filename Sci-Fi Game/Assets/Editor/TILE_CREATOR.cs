using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TILE_CREATOR : EditorWindow
{
	new string	name;
	int			weight;
	Sprite		sprite;
	TILE_TYPE[] types;

	[MenuItem("Custom/Tile Editor")]
	public static void ShowWindow()
	{
		GetWindow(typeof(TILE_CREATOR));
	}

	void OnGUI()
	{
		GUILayout.Label("Tile Creator", EditorStyles.boldLabel);

		EditorGUILayout.LabelField("ID", Get_ID().ToString());
		name	= EditorGUILayout.TextField("Name", name);
		weight	= EditorGUILayout.IntField("Weight", weight);
		sprite	= (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false);
		EditorGUILayout.Space();

		if (GUILayout.Button("Create"))
		{
			TILE_DATA asset = CreateInstance<TILE_DATA>();
			asset.id = Get_ID();
			asset.name = name;
			asset.weight = weight;
			asset.sprite = sprite;
			asset.types = types;

			AssetDatabase.CreateAsset(asset, "Assets/Resources/tile_data/" + name + ".asset");
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}
	}
	
	int Get_ID()
	{
		TILE_DATA[] tile_data = FindAssetsByType<TILE_DATA>().ToArray();
		int previous = 0;

		for (int i = 0; i < tile_data.Length; i++)
		{
			bool found = false;
			for (int j = 0; j < tile_data.Length; j++)
			{
				if(tile_data[j].id == previous + 1)
				{
					found = true;
					previous = tile_data[j].id;
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

	public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
			if (asset != null)
			{
				assets.Add(asset);
			}
		}
		return assets;
	}
}