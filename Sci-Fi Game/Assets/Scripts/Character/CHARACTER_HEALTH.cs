using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CHARACTER_HEALTH
{
	public float	max_health;
	float			current_health;

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
