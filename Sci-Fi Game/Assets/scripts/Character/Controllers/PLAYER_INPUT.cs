using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_INPUT : CONTROLLER
{
	public Camera cam;
	public LayerMask mask;

	private void Update()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Set_Direction_CONTROLLER(new Vector2(h, v));

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 20, mask))
		{
			Set_Look_CONTROLLER(new Vector2(hit.point.x, hit.point.z));
		}

		if (Input.GetButtonDown("Fire1"))
		{
			Attack_CONTROLLER();
		}

		if (Input.GetButtonDown("Run"))
		{
			Run_CONTROLLER(true);
		}
		else if (Input.GetButtonUp("Run"))
		{
			Run_CONTROLLER(false);
		}
	}
}
