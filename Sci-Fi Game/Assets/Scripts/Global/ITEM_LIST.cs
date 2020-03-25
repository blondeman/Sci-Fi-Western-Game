using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_LIST : MonoBehaviour
{
	public static ITEM_LIST instance;
	public ITEM_DATA[]		items;
	public List<ITEM_OBJECT> item_objects = new List<ITEM_OBJECT>();

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
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

	public void Drop_Item_ITEM_LIST(ITEM_DATA item_data, int amount, Vector2 position)
	{
		ITEM_OBJECT clone = Instantiate(PREFABS.instance.item, position, Quaternion.identity).GetComponent<ITEM_OBJECT>();
		clone.Initialize_ITEM_OBJECT(item_data, amount);
		item_objects.Add(clone);
	}
}
