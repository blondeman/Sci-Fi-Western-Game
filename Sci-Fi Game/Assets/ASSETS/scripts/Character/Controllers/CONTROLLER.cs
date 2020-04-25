using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CONTROLLER : MonoBehaviour
{
	public CHARACTER character;

	public void Set_Direction_CONTROLLER(Vector2 direction)
	{
		character.move.Set_Direction_ABSTRACT_CHARACTER(direction);
	}

	public void Attack_CONTROLLER()
	{

	}
}
