using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_ARRAY
{
	public int		width;
	public int		height;
	public int[,]	int_array;

	public void Initialize_TILE_ARRAY(int width, int height)
	{
		this.width =	width;
		this.height =	height;
		int_array =		new int[width, height];
	}

	public void Set_Tile_TILE_ARRAY(int x, int y, TILE_TYPE tile_type)
	{
		int_array[x, y] = (int)tile_type;
	}
}

public enum TILE_TYPE
{
	floor,
	roofed,
	solid,
	transparent
}
