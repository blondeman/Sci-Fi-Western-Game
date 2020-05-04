using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_MOVE : MonoBehaviour
{
	public new Rigidbody	rigidbody;
	public Transform		graphics;
	public float			walk_speed;
	public float			run_speed;
	float					speed;

	public float			rotation_speed;

	Vector3					direction;

	public void Init()
	{
		Run_CHARACTER_MOVE(false);
	}

	public void Set_Direction_CHARACTER_MOVE(Vector2 direction)
	{
		this.direction = new Vector3(direction.x, 0, direction.y);
	}

	public void Set_Look_CHARACTER_MOVE(Vector2 target)
	{
		Vector3 direction = new Vector3(target.x, transform.position.y, target.y) - transform.position;
		float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.Euler(0, angle,0), Time.deltaTime * rotation_speed);
	}

	public void FixedUpdate()
	{
		rigidbody.velocity = direction.normalized * speed;
	}

	public void Run_CHARACTER_MOVE(bool running)
	{
		speed = running ? run_speed : walk_speed;
	}

	public static Vector2 Get_World_Pos_CHARACTER_MOVE(Transform transform)
	{
		float pos_x = transform.position.x / TILE_RENDERER.instance.World_Size_TILE_RENDERER();
		float pos_y = transform.position.z / TILE_RENDERER.instance.World_Size_TILE_RENDERER();

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
