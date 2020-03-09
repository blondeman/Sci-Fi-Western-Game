using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_INPUT : CONTROLLER
{
	private void Update()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Set_Direction_CONTROLLER(new Vector2(h, v));

		if (Input.GetButtonDown("Fire1"))
		{
			Attack_CONTROLLER();
		}
	}
}
