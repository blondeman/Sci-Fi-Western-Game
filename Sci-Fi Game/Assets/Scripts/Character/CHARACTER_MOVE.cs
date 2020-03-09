using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CHARACTER_MOVE
{
	public Rigidbody2D		rigidbody;
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
}
