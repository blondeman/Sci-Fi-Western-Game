using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MINIMAP : MonoBehaviour
{
	public Transform target;
	public RectTransform self;
	public RectTransform marker;
	public Vector2 square_pos;
	public Vector2 circle_pos;

	private void Update()
	{
		square_pos = Get_Position_Ratio_MINIMAP();
		circle_pos = Square_To_Circle_MINIMAP(square_pos);
		marker.anchoredPosition = new Vector2(circle_pos.x * self.sizeDelta.x/2, circle_pos.y * self.sizeDelta.x/2);
	}

	Vector2 Square_To_Circle_MINIMAP(Vector2 pos)
	{
		return new Vector2(
		pos.x * Mathf.Sqrt(1 - pos.y * pos.y / 2),
		pos.y * Mathf.Sqrt(1 - pos.x * pos.x / 2)
		);
	}

	Vector2 Get_Position_Ratio_MINIMAP()
	{
		float pos_x = target.position.x / (TILE_RENDERER.instance.world_size * TILE_RENDERER.instance.chunk_size);
		float pos_y = target.position.y / (TILE_RENDERER.instance.world_size * TILE_RENDERER.instance.chunk_size);

		if (pos_x > 1)
			pos_x -= (int)(pos_x);
		if (pos_x < 0) 
			pos_x -= (int)(pos_x-1);

		if (pos_y > 1)
			pos_y -= (int)(pos_y);
		if (pos_y < 0)
			pos_y -= (int)(pos_y - 1);

		return new Vector2(2 * (pos_x - .5f), 2 * (pos_y - .5f));
	}
}
