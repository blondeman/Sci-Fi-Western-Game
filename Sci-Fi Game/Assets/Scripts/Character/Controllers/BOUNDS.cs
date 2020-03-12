using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOUNDS : MonoBehaviour
{
	private void LateUpdate()
	{
		Bound_Position_TILE_RENDERER();
	}

	public void Bound_Position_TILE_RENDERER()
	{
		float neg_x, neg_y, pos_x, pos_y;
		TILE_RENDERER.instance.Get_Bounds_TILE_RENDERER(out neg_x, out neg_y, out pos_x, out pos_y);

		if (transform.position.x > pos_x) TILE_RENDERER.instance.Move_TILE_RENDERER(0);
		if (transform.position.y > pos_y) TILE_RENDERER.instance.Move_TILE_RENDERER(1);
		if (transform.position.x < neg_x) TILE_RENDERER.instance.Move_TILE_RENDERER(2);
		if (transform.position.y < neg_y) TILE_RENDERER.instance.Move_TILE_RENDERER(3);
	}
}
