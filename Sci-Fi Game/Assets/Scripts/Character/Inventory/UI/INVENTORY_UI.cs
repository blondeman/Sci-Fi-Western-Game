using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INVENTORY_UI : MonoBehaviour
{
	public RectTransform rect_transform;
	public CHARACTER_INVENTORY inventory;
	public GameObject content;
	public List<ITEM_UI> active_slots = new List<ITEM_UI>();
	public List<GameObject> active_items = new List<GameObject>();

	public float hidden_position = -100;
	public float shown_position = 100;
	public float transition_time = 1.5f;
	bool is_hidden;

	[Header("Description View")]
	public Text text_name;
	public Text text_description;
	public Text text_stats;
	int			current_slot;

	private void Start()
	{
		is_hidden = true;
		rect_transform.anchoredPosition = new Vector2(hidden_position, rect_transform.anchoredPosition.y);

		Update_Slot_Count_INVENTORY_UI();
		Clear_Description_INVENTORY_UI();
	}
	
	public void Update_Slot_Count_INVENTORY_UI()
	{
		foreach (ITEM_UI g in active_slots)
		{
			Destroy(g);
		}
		active_slots.Clear();

		for (int i = 0; i < inventory.max_size; i++)
		{
			ITEM_UI slot = Instantiate(PREFABS.instance.ui_slot, content.transform).GetComponent<ITEM_UI>();
			active_slots.Add(slot);
			slot.item_count = null;
			slot.inventory_ui = this;
			slot.slot_id = i;
			slot.transform.name = "slots (" + i + ")";
		}
	}

	public void Update_Items_INVENTORY_UI()
	{
		foreach(ITEM_UI i in active_slots)
		{
			i.item_count = null;
		}

		foreach(GameObject g in active_items)
		{
			Destroy(g);
		}
		active_items.Clear();

		for(int i = 0; i < inventory.item_array.Count; i++)
		{
			ITEM_UI slot = active_slots[inventory.item_array[i].position];
			GameObject clone = Instantiate(PREFABS.instance.ui_item, slot.transform);
			slot.item_count = inventory.item_array[i];
			active_items.Add(clone);
		}

		Set_Description_INVENTORY_UI(current_slot, false);
	}

	public void Clear_Description_INVENTORY_UI()
	{
		current_slot = -1;
		text_name.text = "";
		text_description.text = "";
		text_stats.text = "";
	}

	public void Set_Description_INVENTORY_UI(int slot_id, bool can_disable)
	{
		if (slot_id < 0 || (current_slot == slot_id && can_disable))
		{
			Clear_Description_INVENTORY_UI();
			return;
		}

		current_slot = slot_id;
		ITEM_COUNT item_count = active_slots[slot_id].item_count;

		if (item_count == null)
		{
			Clear_Description_INVENTORY_UI();
		}
		else
		{
			ITEM_DATA item_data = inventory.Get_Item_Data_CHARACTER_INVENTORY(item_count.id);

			text_name.text = item_data.name;
			text_description.text = item_data.description;
			text_stats.text =
			"Count:" + item_count.count + "\n" +
			"Rarity:" + item_data.Get_Rarity() + "\n" +
			"Type:" + item_data.Get_Type();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
			Tab_INVENTORY_UI();
	}

	public void Tab_INVENTORY_UI()
	{
		is_hidden = !is_hidden;

		StartCoroutine(Tab());
	}

	public IEnumerator Tab()
	{
		float start;
		float end;

		if (!is_hidden)
		{
			start = hidden_position;
			end = shown_position;
		}
		else
		{
			start = shown_position;
			end = hidden_position;
		}

		for(float t = 0; t < 1; t += Time.deltaTime / transition_time)
		{
			yield return null;
			rect_transform.anchoredPosition = new Vector2(Mathf.Lerp(start,end,t), rect_transform.anchoredPosition.y);
		}

		rect_transform.anchoredPosition = new Vector2(end, rect_transform.anchoredPosition.y);
	}
}
