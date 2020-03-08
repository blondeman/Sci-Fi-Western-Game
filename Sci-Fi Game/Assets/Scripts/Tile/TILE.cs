using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TILE
{
	public struct TILE_DATA {
		public int			array_x;
		public int			array_y;
		public Vector2		position;
		public TILE_TYPE	tile_type;
	}

	public static void Populate_TILE(out TILE_DATA tile_data, int x, int y, TILE_TYPE type)
	{
		tile_data = new TILE_DATA();
		tile_data.array_x =		x;
		tile_data.array_y =		y;
		tile_data.tile_type =	type;
		//calculate real world position
	}
}