using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_CHUNK : MonoBehaviour
{
	public int world_id = 0;
	public int chunk_x, chunk_y;
	public int data_x, data_y;
	public int[,] array;
	public List<TILE_OBJECT> tiles = new List<TILE_OBJECT>();

	public void Init_TILE_CHUNK(int chunk_x, int chunk_y, int data_x, int data_y, int array_size)
	{
		this.chunk_x = chunk_x;
		this.chunk_y = chunk_y;
		this.data_x = data_x;
		this.data_y = data_y;
		array = new int[array_size, array_size];
	}

	public void Load_Chunk_TILE_CHUNK(PERLIN_NOISE noise)
	{
		//bool exists = SAVE_CHUNK.Read_From_File_SAVE_CHUNK(this);
		//if (!exists)
		//{
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array.GetLength(1); j++)
			{//xOrg + x / noiseTex.width * scale;
				float xCoord = (data_x * array.GetLength(0) + i) / TILE_RENDERER.instance.World_Size_TILE_RENDERER() * noise.size.x;
				float yCoord = (data_y * array.GetLength(1) + j) / TILE_RENDERER.instance.World_Size_TILE_RENDERER() * noise.size.y;
				float value = noise.Get_Fractal_PERLIN_NOISE(new Vector2(xCoord, yCoord), 5, .2f);
				array[i, j] = TILE_RENDERER.instance.Get_Weight_TILE_RENDERER(value);
			}
		}
		//}

		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array.GetLength(1); j++)
			{
				TILE_OBJECT clone = Instantiate(PREFABS.instance.tile, new Vector2(chunk_x * array.GetLength(0) + i, chunk_y * array.GetLength(1) + j), Quaternion.identity, transform);
				clone.transform.name = (i + ", " + j);
				clone.Init_TILE_OBJECT(TILE_RENDERER.instance.tile_data[array[i, j]], i, j);
				tiles.Add(clone);
			}
		}
	}

	public void Unload_Chunk_TILE_CHUNK()
	{
		//SAVE_CHUNK.Write_To_File_SAVE_CHUNK(this);
	}
}
