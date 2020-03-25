using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_INVENTORY : MonoBehaviour
{
	public List<ITEM_COUNT> item_array = new List<ITEM_COUNT>();

	public ITEM_DATA Get_Item_Data_CHARACTER_INVENTORY(int item_id)
	{
		return ITEM_LIST.instance.items[item_id];
	}

	public void Add_Item_CHARACTER_INVENTORY(ITEM_DATA item_data)
	{
		Add_Item_CHARACTER_INVENTORY(item_data, 1);
	}
	public void Add_Item_CHARACTER_INVENTORY(ITEM_DATA item_data, int amount)
	{
		int id = ITEM_LIST.instance.Get_ID_ITEM_LIST(item_data);
		bool found = false;

		for (int i = 0; i < item_array.Count; i++)
		{
			if (item_array[i].id == id)
			{
				item_array[i].count += amount;
				found = true;
				break;
			}
		}

		if (!found)
		{
			item_array.Add(new ITEM_COUNT(id, amount));
		}
	}

	//RETURNS FALSE IF NOT ENOUGH OF THE ITEM OR DOES NOT HAVE THE ITEM
	public bool Remove_Item_CHARACTER_INVENTORY(ITEM_DATA item_data)
	{
		return Remove_Item_CHARACTER_INVENTORY(item_data, 1);
	}
	public bool Remove_Item_CHARACTER_INVENTORY(ITEM_DATA item_data, int amount)
	{
		int id = ITEM_LIST.instance.Get_ID_ITEM_LIST(item_data);

		for (int i = 0; i < item_array.Count; i++)
		{
			if (item_array[i].id == id)
			{
				if (item_array[i].count > amount)
				{
					item_array[i].count -= amount;
				}
				else if(item_array[i].count == amount)
				{
					item_array.RemoveAt(i);
				}
				else
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
			Add_Item_CHARACTER_INVENTORY(ITEM_LIST.instance.items[0], 3);

		if (Input.GetKeyDown(KeyCode.U))
			Drop_Item_CHARACTER_INVENTORY(ITEM_LIST.instance.items[0], 10);
	}

	public void Drop_Item_CHARACTER_INVENTORY(ITEM_DATA item_data, int amount)
	{
		if(Remove_Item_CHARACTER_INVENTORY(item_data, amount))
		{
			ITEM_LIST.instance.Drop_Item_ITEM_LIST(item_data, amount, transform.position);
		}
	}
}
