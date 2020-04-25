using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_HEALTH : MonoBehaviour
{
	public float max_health;
	public float current_health;

	private void Start()
	{
		current_health = max_health;
	}

	public void Take_Damage_CHARACTER_HEALTH(float damage)
	{
		current_health -= damage;
		if (current_health <= 0)
		{
			Die_CHARACTER_HEALTH();
		}
	}

	void Die_CHARACTER_HEALTH()
	{

	}
}
