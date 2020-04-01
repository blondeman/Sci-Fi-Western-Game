using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WORLD_LOOP : MonoBehaviour
{
	public static WORLD_LOOP instance;
	public List<ENTITY> entities = new List<ENTITY>();
	public Transform loop_handler;
	public float loop_distance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Update()
	{
		Set_Position_WORLD_LOOP(loop_handler);

		foreach (ENTITY entity in entities)
		{
			Vector2 lowest_position = entity.transform.position;
			float lowest_distance = loop_distance;
			foreach(Vector2 position in entity.positions_ENTITY())
			{
				float distance = Vector2.Distance(position, loop_handler.position);
				if (distance < loop_distance)
				{
					lowest_distance = distance;
					lowest_position = position;
				}
			}
			entity.transform.position = lowest_position;
		}
	}

	public void Set_Position_WORLD_LOOP(Transform move_object)
	{
		float neg_x, neg_y, pos_x, pos_y;
		TILE_RENDERER.instance.Get_Bounds_TILE_RENDERER(out neg_x, out neg_y, out pos_x, out pos_y);

		if (move_object.position.x > pos_x)
		{
			float offset = move_object.position.x - pos_x;
			move_object.position = new Vector2(neg_x + offset, move_object.position.y);
		}

		if (move_object.position.x < neg_x)
		{
			float offset = move_object.position.x - neg_x;
			move_object.position = new Vector2(pos_x + offset, move_object.position.y);
		}

		if (move_object.position.y > pos_y)
		{
			float offset = move_object.position.y - pos_y;
			move_object.position = new Vector2(move_object.position.x, neg_y + offset);
		}

		if (move_object.position.y < neg_y)
		{
			float offset = move_object.position.y - neg_y;
			move_object.position = new Vector2(move_object.position.x, pos_y + offset);
		}
	}
}
