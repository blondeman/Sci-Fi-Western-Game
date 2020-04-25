using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ITEM_COUNT
{
	public string	name;
	public int		count;
	public int		position;

	public ITEM_COUNT(int count, int position, string name)
	{
		this.name = name;
		this.count = count;
		this.position = position;
	}
}
