using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class SAVE_INVENTORY
{
	public static void Write_To_File_SAVE_INVENTORY(CHARACTER_INVENTORY data, int character_id)
	{
		JSONObject node = new JSONObject();
		JSONArray array = new JSONArray();
		for (int i = 0; i < data.item_array.Count; i++)
		{
			JSONObject item = new JSONObject();
			item.Add("Name", data.item_array[i].name);
			item.Add("Position", data.item_array[i].position);
			item.Add("Count", data.item_array[i].count);
			array.Add(item);
		}
		node.Add("Size", data.max_size);
		node.Add("Items", array);

		string path = Path.Combine(Application.dataPath, "Save_Data/Character_Data/Character" + character_id);
		
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path = path + "/Inventory.json";
		
		string write = node.ToString(2);
		if (File.Exists(path))
		{
			File.WriteAllText(path, write);
		}
		else
		{
			StreamWriter sr = File.CreateText(path);
			sr.Write(write);
			sr.Close();
		}
	}

	public static bool Read_From_File_SAVE_INVENTORY(CHARACTER_INVENTORY data, int character_id)
	{
		string path = Path.Combine(Application.dataPath, "Save_Data/Character_Data/Character" + character_id + "/Inventory.json");

		if (File.Exists(path))
		{
			JSONObject node = (JSONObject)JSON.Parse(File.ReadAllText(path));
			
			data.max_size = node["Size"];
			JSONArray array = node["Items"].AsArray;
			
			for(int i = 0; i < array.Count; i++)
			{
				JSONObject item = array[i].AsObject;
				data.item_array.Add(new ITEM_COUNT(
				item["Count"],
				item["Position"],
				item["Name"]
				));
			}

			return true;
		}
		return false;
	}
}