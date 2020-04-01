using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENTITY : MonoBehaviour
{
	Vector2[] positions = new Vector2[9];

	public void Start()
	{
		WORLD_LOOP.instance.entities.Add(this);
	}

	public void OnDestroy()
	{
		WORLD_LOOP.instance.entities.Remove(this);
	}

	public void Set_Positions_ENTITY()
	{
		float neg_x, neg_y, pos_x, pos_y;
		TILE_RENDERER.instance.Get_Bounds_TILE_RENDERER(out neg_x, out neg_y, out pos_x, out pos_y);

		positions[0] = transform.position;
		positions[1] = new Vector2(transform.position.x - pos_x + neg_x, transform.position.y);
		positions[2] = new Vector2(transform.position.x - neg_x + pos_x, transform.position.y);
		positions[3] = new Vector2(transform.position.x, transform.position.y - pos_y + neg_y);
		positions[4] = new Vector2(transform.position.x, transform.position.y - neg_y + pos_y);
		positions[5] = new Vector2(transform.position.x - pos_x + neg_x, transform.position.y - pos_y + neg_y);
		positions[6] = new Vector2(transform.position.x - neg_x + pos_x, transform.position.y - pos_y + neg_y);
		positions[7] = new Vector2(transform.position.x - pos_x + neg_x, transform.position.y - neg_y + pos_y);
		positions[8] = new Vector2(transform.position.x - neg_x + pos_x, transform.position.y - neg_y + pos_y);
	}

	public Vector2[] positions_ENTITY()
	{
		Set_Positions_ENTITY();

		return positions;
	}
}
