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
		inventory_ui.Set_Description_INVENTORY_UI(slot_id, true);
	}
}
