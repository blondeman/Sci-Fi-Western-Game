using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SAVE_INVENTORY
{
	public static void Write_To_File_SAVE_INVENTORY(CHARACTER_INVENTORY inventory, int character_id)
	{
		Data data = new Data();
		data.Size = inventory.max_size;
		data.Items = new Data.Item[inventory.item_array.Count];
		for(int i = 0; i < inventory.item_array.Count; i++)
		{
			data.Items[i] = new Data.Item();
			data.Items[i].Name		= inventory.item_array[i].name;
			data.Items[i].Position	= inventory.item_array[i].position;
			data.Items[i].Count		= inventory.item_array[i].count;
		}

		string path = Path.Combine(Application.dataPath, "Character_Data/Character" + character_id);
		string json = JsonUtility.ToJson(data, true);
		
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path = path + "/Inventory.json";

		//Debug.Log("Writing to " + path);
		if (File.Exists(path))
		{
			File.WriteAllText(path, json);
		}
		else
		{
			StreamWriter sr = File.CreateText(path);
			sr.Write(json);
			sr.Close();
		}
	}

	public static Data Read_From_File_SAVE_INVENTORY(int character_id)
	{
		string path = Path.Combine(Application.dataPath, "Character_Data/Character" + character_id + "/Inventory.json");

		if (File.Exists(path))
		{
			//Debug.Log("Reading from " + path);
			string json = File.ReadAllText(path);
			Data data = JsonUtility.FromJson<Data>(json);

			return data;
		}

		return null;
	}
}

public class Data
{
	[System.Serializable]
	public class Item
	{
		public string Name;
		public int Position;
		public int Count;
	}

	public int Size;
	public Item[] Items;
}