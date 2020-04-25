using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SAVE_CHUNK
{
	public static void Write_To_File_SAVE_CHUNK(TILE_CHUNK data)
	{
		string path = Path.Combine(Application.dataPath, "Save_Data/Chunk_Data/World" + data.world_id);

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path = path + "/Chunk" + data.data_x + "_" + data.data_y + ".dat";

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(path);
		bf.Serialize(file, data.array);
		file.Close();
	}

	public static bool Read_From_File_SAVE_CHUNK(TILE_CHUNK data)
	{
		string path = Path.Combine(Application.dataPath, "Save_Data/Chunk_Data/World" + data.world_id + "/Chunk" + data.data_x + "_" + data.data_y + ".dat");

		if (File.Exists(path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);
			data.array = (int[,])bf.Deserialize(file);
			file.Close();

			return true;
		}
		return false;
	}
}