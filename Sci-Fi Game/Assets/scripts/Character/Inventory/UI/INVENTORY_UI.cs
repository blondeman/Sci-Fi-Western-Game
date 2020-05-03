using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INVENTORY_UI : MonoBehaviour
{
	[HideInInspector] public RectTransform rect_transform;
	public CHARACTER_INVENTORY inventory;
	public GameObject content;
	[HideInInspector] public List<ITEM_UI> active_slots = new List<ITEM_UI>();
	[HideInInspector] public List<GameObject> active_items = new List<GameObject>();

	[Header("Drag")]
	public Transform drag_parent;
	[HideInInspector] public ITEM_UI pointer_hover;

	[Header("Tab")]
	public float hidden_position = -100;
	public float shown_position = 100;
	public float transition_time = 1.5f;
	bool is_hidden;

	[Header("Description View")]
	public GameObject	description_view;
	public Text			text_name;
	public Text			text_description;
	public Text			text_stats;

	[Header("Drop View")]
	public GameObject	drop_view;
	public Text			drop_name;
	public InputField	input_field;
	int					drop_count;

	int current_slot;

	private void Start()
	{
		inventory.Set_UI_CHARACTER_INVENTORY(this);
		rect_transform = GetComponent<RectTransform>();

		is_hidden = true;
		rect_transform.anchoredPosition = new Vector2(hidden_position, rect_transform.anchoredPosition.y);

		Clear_Menu_INVENTORY_UI();
		Update_Slot_Count_INVENTORY_UI();
		inventory.Update_Inventory_CHARACTER_INVENTORY();
	}

	public void Set_Drag_INVENTORY_UI(ITEM_UI parent, Transform child)
	{
		if (pointer_hover == null)
		{
			child.SetParent(parent.transform);
			child.position = parent.transform.position;
		}
		else
		{
			child.SetParent(pointer_hover.transform);
			child.position = pointer_hover.transform.position;
			pointer_hover.child_item = child;

			parent.child_item = null;

			int item_count_id = inventory.Get_Position_CHARACTER_INVENTORY(parent.slot_id);
			inventory.item_array[item_count_id].position = pointer_hover.slot_id;

			inventory.Update_Inventory_CHARACTER_INVENTORY();
		}
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
			slot.drag_parent = drag_parent;
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
			slot.child_item = clone.transform;
			active_items.Add(clone);
		}

		Description_Menu_INVENTORY_UI(current_slot);
	}

	public void Clear_Menu_INVENTORY_UI()
	{
		drop_count = 1;
		current_slot = -1;
		description_view.SetActive(false);
		drop_view.SetActive(false);
	}

	public void Drop_Menu_INVENTORY_UI(int slot_id)
	{
		if (slot_id < 0)
		{
			Clear_Menu_INVENTORY_UI();
			return;
		}

		Clear_Menu_INVENTORY_UI();
		current_slot = slot_id;
		ITEM_COUNT item_count = active_slots[slot_id].item_count;
		
		if (item_count == null)
		{
			Clear_Menu_INVENTORY_UI();
		}
		else
		{
			drop_view.SetActive(true);
			ITEM_DATA item_data = inventory.Get_Item_Data_CHARACTER_INVENTORY(ITEM_LIST.instance.Get_ID_ITEM_LIST(item_count.name));

			drop_name.text = item_data.name;

			drop_count = item_count.count;
			input_field.text = drop_count.ToString();
		}
	}

	public void Description_Menu_INVENTORY_UI(int slot_id)
	{
		if (slot_id < 0)
		{
			Clear_Menu_INVENTORY_UI();
			return;
		}

		Clear_Menu_INVENTORY_UI();
		current_slot = slot_id;
		ITEM_COUNT item_count = active_slots[slot_id].item_count;

		if (item_count == null)
		{
			Clear_Menu_INVENTORY_UI();
		}
		else
		{
			description_view.SetActive(true);
			ITEM_DATA item_data = inventory.Get_Item_Data_CHARACTER_INVENTORY(ITEM_LIST.instance.Get_ID_ITEM_LIST(item_count.name));

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

	public void Change_Drop_Value_INVENTORY_UI(int amount)
	{
		drop_count += amount;
		if (drop_count <= 0)
			drop_count = 1;
		else if (drop_count > active_slots[current_slot].item_count.count)
			drop_count = active_slots[current_slot].item_count.count;

		input_field.text = drop_count.ToString();
	}

	public void Set_Drop_Value_INVENTORY_UI()
	{
		drop_count = int.Parse(input_field.text);
		Change_Drop_Value_INVENTORY_UI(0);
	}

	public void Drop_Item_INVENTORY_UI()
	{
		inventory.Drop_Item_CHARACTER_INVENTORY(current_slot, drop_count);
	}
}
