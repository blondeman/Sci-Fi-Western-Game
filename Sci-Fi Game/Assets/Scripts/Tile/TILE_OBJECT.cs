using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILE_OBJECT : MonoBehaviour
{
	public SpriteRenderer sprite_renderer;
	[HideInInspector] public TILE_DATA tile_data;
	[HideInInspector] public int array_x;
	[HideInInspector] public int array_y;

	public void Initialize_TILE_OBJECT(TILE_DATA tile_data, int array_x, int array_y)
	{
		this.tile_data = tile_data;
		this.array_x = array_x;
		this.array_y = array_y;
		sprite_renderer.sprite = tile_data.sprite;
		transform.name = tile_data.name;
	}
}
