using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_UI : MonoBehaviour
{
	public INVENTORY_UI inventory_ui;
	public ITEM_COUNT	item_count;
	public int			slot_id;

	public void Set_Description_ITEM_UI()
	{
		inventory_ui.Description_Menu_INVENTORY_UI(slot_id);
	}

	public void Drop_Menu_ITEM_UI()
	{
		inventory_ui.Drop_Menu_INVENTORY_UI(slot_id);
	}
}
