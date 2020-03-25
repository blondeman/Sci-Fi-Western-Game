using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ITEM_COUNT
{
	public int id;
	public int count;
	public int position;

	public ITEM_COUNT(int id, int count, int position)
	{
		this.id			= id;
		this.count		= count;
		this.position	= position;
	}
}
