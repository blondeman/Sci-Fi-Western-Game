using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_MOVE : MonoBehaviour
{
	public new Rigidbody2D	rigidbody;
	public float			speed;

	Vector2					direction;
	int						array_x;
	int						array_y;

	public void Set_Direction_ABSTRACT_CHARACTER(Vector2 direction)
	{
		this.direction = direction;
	}

	public void FixedUpdate()
	{
		rigidbody.velocity = direction.normalized * speed;
	}

	private void Update()
	{
		Check_Bounds_CHARACTER_MOVE();
	}

	public void Check_Bounds_CHARACTER_MOVE()
	{
		float neg_x, neg_y, pos_x, pos_y;
		TILE_RENDERER.instance.Get_Bounds_TILE_RENDERER(out neg_x, out neg_y, out pos_x, out pos_y);

		if (transform.position.x > pos_x)
		{
			float offset = transform.position.x - pos_x;
			transform.position = new Vector2(neg_x + offset, transform.position.y);
		}

		if (transform.position.x < neg_x)
		{
			float offset = transform.position.x - neg_x;
			transform.position = new Vector2(pos_x + offset, transform.position.y);
		}

		if (transform.position.y > pos_y)
		{
			print("Greater");
			float offset = transform.position.y - pos_y;
			transform.position = new Vector2(transform.position.x, neg_y + offset);
		}

		if (transform.position.y < neg_y)
		{
			print("lesser");
			float offset = transform.position.y - neg_y;
			transform.position = new Vector2(transform.position.x, pos_y + offset);
		}
	}

	/*
	public void Move_TILE_RENDERER(int axis)//axis begins with positive x, positive y, negative x, negative y
	{
		switch (axis)
		{
			case 0:
				transform.position = new Vector2(transform.position.x + (world_dimensions.x + boundary_width) * tile_size, transform.position.y);
				break;
			case 1:
				transform.position = new Vector2(transform.position.x, transform.position.y + (world_dimensions.y + boundary_width) * tile_size);
				break;
			case 2:
				transform.position = new Vector2(transform.position.x - (world_dimensions.x + boundary_width) * tile_size, transform.position.y);
				break;
			case 3:
				transform.position = new Vector2(transform.position.x, transform.position.y - (world_dimensions.y + boundary_width) * tile_size);
				break;
			default:break;
		}
	}*/
}
