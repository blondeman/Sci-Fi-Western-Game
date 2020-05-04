using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CONTROLLER : MonoBehaviour
{
	public CHARACTER character;

	public void Set_Direction_CONTROLLER(Vector2 direction)
	{
		character.move.Set_Direction_CHARACTER_MOVE(direction);
	}

	public void Set_Look_CONTROLLER(Vector2 target)
	{
		character.move.Set_Look_CHARACTER_MOVE(target);
	}

	public void Run_CONTROLLER(bool running)
	{
		character.move.Run_CHARACTER_MOVE(running);
	}

	public void Attack_CONTROLLER()
	{

	}
}
