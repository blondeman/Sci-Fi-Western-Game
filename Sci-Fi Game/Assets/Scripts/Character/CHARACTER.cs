using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER : MonoBehaviour
{
	public CHARACTER_MOVE		move;
	public CHARACTER_HEALTH		health;
	public CHARACTER_INVENTORY	inventory;

	public void FixedUpdate()
	{
		move.FixedUpdate();
	}
}
