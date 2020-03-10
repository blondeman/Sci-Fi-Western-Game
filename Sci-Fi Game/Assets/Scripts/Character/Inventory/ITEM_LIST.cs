using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_LIST : MonoBehaviour
{
	public static ITEM_LIST instance;
	public ITEM_DATA[]		items;

	private void Start()
	{
		if (instance == null)
			instance = this;

		Object[] temp_object_array = Resources.LoadAll("item_data", typeof(ITEM_DATA));
		items = new ITEM_DATA[temp_object_array.Length];

		for(int i = 0; i < temp_object_array.Length; i++)
		{
			items[i] = (ITEM_DATA)temp_object_array[i];	
		}
	}

	public int Get_ID_ITEM_LIST(ITEM_DATA item_data)
	{
		for(int i = 0; i < items.Length; i++)
		{
			if (items[i].name == item_data.name) return i;
		}
		return -1;
	}
}
