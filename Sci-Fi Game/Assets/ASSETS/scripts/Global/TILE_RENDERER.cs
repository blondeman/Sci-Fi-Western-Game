using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_RENDERER : MonoBehaviour
{
	public static TILE_RENDERER				instance;
	[HideInInspector] public TILE_DATA[]	tile_data;

	public int world_size;//amount of chunks in the x and y dimentions
	public int chunk_size;//amount of chunks in each chunk in the x and y dirmention

	public Transform chunk_loader;
	public int chunk_radius;
	public List<TILE_CHUNK> active_chunks = new List<TILE_CHUNK>();

	public int seed;
	public PERLIN_NOISE noise;
	public float size;

	int total_weight;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		noise = new PERLIN_NOISE(seed, Vector2.one * size);
		Load_Tile_Data_TILE_RENDERER();
	}

	void Load_Tile_Data_TILE_RENDERER()
	{
		Object[] temp_object_array = Resources.LoadAll("tile_data", typeof(TILE_DATA));
		tile_data = new TILE_DATA[temp_object_array.Length];
		for (int i = 0; i < temp_object_array.Length; i++)
		{
			for (int j = 0; j < temp_object_array.Length; j++)
			{
				TILE_DATA tile = (TILE_DATA)temp_object_array[j];
				if (tile.id == i)
				{
					tile_data[i] = tile;
					total_weight += tile_data[i].weight;
					break;
				}
			}
		}
	}

	public void Update()
	{
		Render_Chunks_TILE_RENDERER();
	}

	public void Render_Chunks_TILE_RENDERER()
	{
		Vector2Int position = Current_Chunk_TILE_RENDERER(chunk_loader.position);
		List<TILE_CHUNK> chunks_to_delete = new List<TILE_CHUNK>();
		chunks_to_delete.AddRange(active_chunks);

		for (int i = -chunk_radius; i <= chunk_radius; i++)
		{
			for (int j = -chunk_radius; j <= chunk_radius; j++)
			{
				TILE_CHUNK new_chunk = Chunk_Exists_TILE_RENDERER(position.x + i, position.y + j);
				if (new_chunk != null)
				{
					chunks_to_delete.Remove(new_chunk);
				}
				else
				{
					int x = (position.x + i);
					int y = (position.y + j);

					TILE_CHUNK clone = Instantiate(PREFABS.instance.chunk, new Vector2(x * chunk_size, y * chunk_size), Quaternion.identity, transform);					
					Vector2Int chunk_data = Chunk_Data_TILE_RENDERER(x, y);
					clone.transform.name = (chunk_data.x + ", " + chunk_data.y);
					clone.Init_TILE_CHUNK(x, y, chunk_data.x, chunk_data.y, chunk_size);
					clone.Load_Chunk_TILE_CHUNK(noise);
					active_chunks.Add(clone);
				}
			}
		}

		int count = chunks_to_delete.Count;
		for (int i = 0; i < count; i++)
		{
			TILE_CHUNK chunk = chunks_to_delete[0];
			active_chunks.Remove(chunk);
			chunks_to_delete.Remove(chunk);
			chunk.Unload_Chunk_TILE_CHUNK();
			Destroy(chunk.gameObject);
		}
	}

	public TILE_CHUNK Chunk_Exists_TILE_RENDERER(int x, int y)
	{
		foreach(TILE_CHUNK chunk in active_chunks)
		{
			if(chunk.chunk_x == x && chunk.chunk_y == y)
			{
				return chunk;
			}
		}
		return null;
	}

	public Vector2Int Current_Chunk_TILE_RENDERER(Vector2 position)
	{
		return new Vector2Int(Mathf.RoundToInt((position.x - (chunk_size / 2)) / chunk_size), Mathf.RoundToInt((position.y - (chunk_size / 2)) / chunk_size));
	}

	public Vector2Int Chunk_Data_TILE_RENDERER(int x, int y)
	{
		int pos_x;
		int pos_y;

		if (x >= 0)
			pos_x = x % world_size;
		else
		{
			pos_x = world_size - (Mathf.Abs(x) % world_size);
			if (pos_x == 8) pos_x = 0;
		}

		if (y >= 0)
			pos_y = y % world_size;
		else
		{
			pos_y = world_size - (Mathf.Abs(y) % world_size);
			if (pos_y == 8) pos_y = 0;
		}


		return new Vector2Int(pos_x, pos_y);
	}

	public float World_Size_TILE_RENDERER()
	{
		return world_size * chunk_size;
	}

	public int Get_Weight_TILE_RENDERER(float value)
	{
		value *= total_weight;
		int current_weight = 0;
		for(int i = 0; i < tile_data.Length; i++)
		{
			if(value > current_weight && value < current_weight + tile_data[i].weight)
			{
				return i;
			}

			current_weight += tile_data[i].weight;
		}
		return 0;
	}
}
