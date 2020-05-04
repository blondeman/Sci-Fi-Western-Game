using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_INVENTORY : MonoBehaviour
{
	public int				max_size = 15;
	public List<ITEM_COUNT> item_array = new List<ITEM_COUNT>();
	[HideInInspector] public INVENTORY_UI		inventory_ui;

	public void Init()
	{

	}

	public void Set_UI_CHARACTER_INVENTORY(INVENTORY_UI ui)
	{
		inventory_ui = ui;
	}

	public ITEM_DATA Get_Item_Data_CHARACTER_INVENTORY(int item_id)
	{
		return ITEM_LIST.instance.items[item_id];
	}

	public int Get_Position_CHARACTER_INVENTORY(int position)
	{
		for(int i = 0; i < item_array.Count; i++)
		{
			if (item_array[i].position == position)
				return i;
		}
		return -1;
	}

	public void Add_Item_CHARACTER_INVENTORY(ITEM_DATA item_data)
	{
		Add_Item_CHARACTER_INVENTORY(item_data, 1);
	}
	public void Add_Item_CHARACTER_INVENTORY(ITEM_DATA item_data, int amount)
	{
		for (int i = 0; i < item_array.Count; i++)
		{
			if (item_array[i].name == item_data.name)
			{
				item_array[i].count += amount;
				Update_Inventory_CHARACTER_INVENTORY();
				return;
			}
		}

		if (item_array.Count >= max_size)
			return;

		item_array.Add(new ITEM_COUNT(amount, Get_Lowest_Position_CHARACTER_INVENTORY(), item_data.name));
		Update_Inventory_CHARACTER_INVENTORY();
	}

	public int Get_Lowest_Position_CHARACTER_INVENTORY()
	{
		for(int i = 0;i<max_size;i++)
		{
			bool found = false;

			for(int j = 0; j < item_array.Count; j++)
			{
				if (item_array[j].position == i)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				return i;
			}
		}

		return -1;
	}

	//RETURNS FALSE IF NOT ENOUGH OF THE ITEM OR DOES NOT HAVE THE ITEM
	public bool Remove_Item_CHARACTER_INVENTORY(int position)
	{
		return Remove_Item_CHARACTER_INVENTORY(position, 1);
	}
	public bool Remove_Item_CHARACTER_INVENTORY(int position, int amount)
	{
		int id = Get_Position_CHARACTER_INVENTORY(position);

		if (item_array[id].count > amount)
		{
			item_array[id].count -= amount;
		}
		else if(item_array[id].count == amount)
		{
			item_array.RemoveAt(id);
		}
		else
		{
			return false;
		}
		Update_Inventory_CHARACTER_INVENTORY();
		return true;
	}

	public void Drop_Item_CHARACTER_INVENTORY(int position, int amount)
	{
		int id = ITEM_LIST.instance.Get_ID_ITEM_LIST(item_array[Get_Position_CHARACTER_INVENTORY(position)].name);

		if (Remove_Item_CHARACTER_INVENTORY(position, amount))
		{
			ITEM_LIST.instance.Drop_Item_ITEM_LIST(id, amount, transform.position);
		}
	}

	public void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag.Equals("Item"))
		{
			ITEM_OBJECT item = collision.GetComponent<ITEM_OBJECT>();
			Add_Item_CHARACTER_INVENTORY(item.item_data, item.count);
			Destroy(collision.gameObject);
		}
	}

	public void Update_Inventory_CHARACTER_INVENTORY()
	{
		Update_Inventory_CHARACTER_INVENTORY(true);
	}
	public void Update_Inventory_CHARACTER_INVENTORY(bool update_ui)
	{
		if (inventory_ui != null && update_ui == true)
		{
			inventory_ui.Update_Items_INVENTORY_UI();
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
			Add_Item_CHARACTER_INVENTORY(Get_Item_Data_CHARACTER_INVENTORY(0));

		if (Input.GetKeyDown(KeyCode.O))
			Add_Item_CHARACTER_INVENTORY(Get_Item_Data_CHARACTER_INVENTORY(1));
	}
}
