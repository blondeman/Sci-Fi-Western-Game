using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_MOVE : MonoBehaviour
{
	public new Rigidbody2D	rigidbody;
	public float			speed;

	Vector2					direction;

	public void Set_Direction_ABSTRACT_CHARACTER(Vector2 direction)
	{
		this.direction = direction;
	}

	public void FixedUpdate()
	{
		rigidbody.velocity = direction.normalized * speed;
	}

	public static Vector2 Get_World_Pos_CHARACTER_MOVE(Transform transform)
	{
		float pos_x = transform.position.x / TILE_RENDERER.instance.World_Size_TILE_RENDERER();
		float pos_y = transform.position.y / TILE_RENDERER.instance.World_Size_TILE_RENDERER();

		if (pos_x > 1)
			pos_x -= (int)(pos_x);
		if (pos_x < 0)
			pos_x -= (int)(pos_x - 1);

		if (pos_y > 1)
			pos_y -= (int)(pos_y);
		if (pos_y < 0)
			pos_y -= (int)(pos_y - 1);
			pos_y -= (int)(pos_y - 1);

		return new Vector2(pos_x, pos_y);
	}
}
