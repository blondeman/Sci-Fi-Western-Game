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
	public TILE_DATA[]				tile_data;
	public TILE_OBJECT				tile_prefab;
	[HideInInspector] public float	bounds_neg_x, bounds_neg_y, bounds_pos_x, bounds_pos_y;

	[Header("TESTING")]
	public Vector2Int test_start, test_end;
	private Vector2Int previous_test_start, previous_test_end;
	private NODE path;

	private void Start()
	{
		if (instance == null)
			instance = this;
		Render_TILE_RENDERER();

		float boundary_width_size = boundary_width * tile_size;

		bounds_neg_x = boundary_width_size/2 - tile_size/2;
		bounds_neg_y = boundary_width_size/ 2 -+ tile_size / 2;
		bounds_pos_x = (boundary_width + world_dimensions.x) * tile_size + boundary_width_size / 2 - tile_size / 2;
		bounds_pos_y = (boundary_width + world_dimensions.y) * tile_size + boundary_width_size / 2 - tile_size / 2;
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

	public void OnDrawGizmos()
	{
		PATHFINDER pathfinder = new PATHFINDER();
		NODE start = new NODE(test_start.x, test_start.y);
		NODE end = new NODE(test_end.x, test_end.y);

		if (test_end != previous_test_end || test_start != previous_test_start)
		{
			path = pathfinder.A_Star_PATHFINDER(start, end);
			Debug.Log("qwe");
		}

		if (path == null)
		{
			Debug.Log("ASD");
			path = new NODE();
		}

		Gizmos.color = Color.blue;
		for(int i = path.distance; i >0; i--)
		{
			Gizmos.DrawLine(path.Get_Parent_NODE(i).Get_Position_NODE(), path.Get_Parent_NODE(i-1).Get_Position_NODE());
		}

		previous_test_end = test_end;
		previous_test_start = test_start;
	}
}
