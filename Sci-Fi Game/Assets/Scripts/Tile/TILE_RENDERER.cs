using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_RENDERER : MonoBehaviour
{
	public float		tile_size;
	public Vector2Int	world_dimensions;
	public int			boundary_width;
	private TILE_ARRAY	tile_array;
	public TILE_DATA[]	tile_data;
	public TILE_OBJECT	tile_prefab;

	private void Start()
	{
		Render_TILE_RENDERER();
	}

	public void Render_TILE_RENDERER()
	{
		tile_array = new TILE_ARRAY();
		tile_array.Initialize_TILE_ARRAY(world_dimensions.x + boundary_width * 2, world_dimensions.y + boundary_width * 2);

		for(int i = 0; i < tile_array.width; i++)
		{
			for(int j = 0; j < tile_array.height; j++)
			{
				Create_Tile_TILE_RENDERER(i, j, 0);
			}
		}
	}

	public void Create_Tile_TILE_RENDERER(int x, int y, int tile_data_id)
	{
		TILE_OBJECT clone = Instantiate(tile_prefab, new Vector2(x * tile_size, y * tile_size), Quaternion.identity, this.transform);
		clone.Initialize_TILE_OBJECT(tile_data[tile_data_id], x, y);
	}
}
