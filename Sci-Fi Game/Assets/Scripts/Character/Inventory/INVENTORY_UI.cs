using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INVENTORY_UI : MonoBehaviour
{
	public CHARACTER_INVENTORY inventory;
	public GameObject content;
	public List<GameObject> active_slots = new List<GameObject>();
	public List<GameObject> active_items = new List<GameObject>();

	private void Start()
	{
		Update_Slot_Count_INVENTORY_UI();
	}
	
	public void Update_Slot_Count_INVENTORY_UI()
	{
		foreach (GameObject g in active_slots)
		{
			Destroy(g);
		}
		active_slots.Clear();

		for (int i = 0; i < inventory.max_size; i++)
		{
			GameObject clone = Instantiate(PREFABS.instance.ui_slot, content.transform);
			active_slots.Add(clone);
			clone.transform.name = "slots (" + i + ")";
		}
	}

	public void Update_Items_INVENTORY_UI()
	{
		foreach(GameObject g in active_items)
		{
			Destroy(g);
		}
		active_items.Clear();

		for(int i = 0; i < inventory.item_array.Count; i++)
		{
			GameObject clone = Instantiate(PREFABS.instance.ui_item, active_slots[inventory.item_array[i].position].transform);
			active_items.Add(clone);
		}
	}
}
