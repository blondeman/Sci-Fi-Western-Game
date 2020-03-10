using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ITEM_COUNT
{
	public int						id;
	public int						count;
	[HideInInspector] public int	pos_x;
	[HideInInspector] public int	pos_y;

	public ITEM_COUNT(int id, int count)
	{
		this.id =		id;
		this.count =	count;
	}
}
