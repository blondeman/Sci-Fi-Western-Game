using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class SAVE_CHUNK
{
	public static void Write_To_File_SAVE_CHUNK(TILE_CHUNK data)
	{
		JSONArray array = new JSONArray();
		for(int i = 0; i < data.array.GetLength(0); i++)
		{
			JSONArray array_deeper = new JSONArray();
			for (int j = 0; j < data.array.GetLength(1); j++)
			{
				array_deeper.Add(data.array[i, j]);
			}
			array.Add(array_deeper);
		}

		JSONObject node = new JSONObject();
		node.Add("Tiles", array);


		string path = Path.Combine(Application.dataPath, "Save_Data/Chunk_Data/World"+data.world_id);

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path = path + "/Chunk" + data.data_x + "_" + data.data_y + ".json";

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

	public static bool Read_From_File_SAVE_CHUNK(TILE_CHUNK data)
	{
		string path = Path.Combine(Application.dataPath, "Save_Data/Chunk_Data/World" + data.world_id + "/Chunk" + data.data_x + "_" + data.data_y + ".json");

		if (File.Exists(path))
		{
			JSONObject node = (JSONObject)JSON.Parse(File.ReadAllText(path));

			JSONArray array = node["Tiles"].AsArray;
			for (int i = 0; i < array.Count; i++)
			{
				JSONArray array_deeper = array[i].AsArray;
				for (int j = 0; j < array_deeper.Count; j++)
				{
					data.array[i, j] = array_deeper[j];
				}
			}

			return true;
		}
		return false;
	}
}