using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_RENDERER : MonoBehaviour
{
	public static TILE_RENDERER		instance;
	public float					tile_size;
	public Vector2Int				world_dimensions;
	public int						boundary_width;
	private TILE_ARRAY				tile_array;
	private TILE_DATA[]				tile_data;
	[HideInInspector] public float	bounds_neg_x, bounds_neg_y, bounds_pos_x, bounds_pos_y;

	private void Start()
	{
		if (instance == null)
			instance = this;

		float boundary_width_size = boundary_width * tile_size;

		bounds_neg_x = boundary_width_size/2 - tile_size/2;
		bounds_neg_y = boundary_width_size/ 2 -+ tile_size / 2;
		bounds_pos_x = (boundary_width + world_dimensions.x) * tile_size + boundary_width_size / 2 - tile_size / 2;
		bounds_pos_y = (boundary_width + world_dimensions.y) * tile_size + boundary_width_size / 2 - tile_size / 2;

		Object[] temp_object_array = Resources.LoadAll("tile_data", typeof(TILE_DATA));
		tile_data = new TILE_DATA[temp_object_array.Length];

		for (int i = 0; i < temp_object_array.Length; i++)
		{
			tile_data[i] = (TILE_DATA)temp_object_array[i];
		}

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
		TILE_OBJECT clone = Instantiate(PREFABS.instance.tile, new Vector2(x * tile_size, y * tile_size), Quaternion.identity, this.transform).GetComponent<TILE_OBJECT>();
		clone.Initialize_TILE_OBJECT(tile_data[tile_data_id], x, y);
	}

	public void Get_Bounds_TILE_RENDERER(out float neg_x, out float neg_y, out float pos_x, out float pos_y)
	{
		neg_x = bounds_neg_x;
		neg_y = bounds_neg_y;
		pos_x = bounds_pos_x;
		pos_y = bounds_pos_y;
	}

	public bool Node_Valid_TILE_RENDERER(int x, int y)
	{
		return tile_array.Node_Valid_TILE_ARRAY(x, y);
	}
}
