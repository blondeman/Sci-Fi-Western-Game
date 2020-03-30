using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ITEM_UI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public INVENTORY_UI inventory_ui;
	public ITEM_COUNT	item_count;
	public int			slot_id;
	public Transform	child_item;
	public Transform	drag_parent;

	public void Set_Description_ITEM_UI()
	{
		inventory_ui.Description_Menu_INVENTORY_UI(slot_id);
	}

	public void Drop_Menu_ITEM_UI()
	{
		inventory_ui.Drop_Menu_INVENTORY_UI(slot_id);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (child_item == null)
			return;

		child_item.SetParent(drag_parent);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (child_item == null)
			return;

		child_item.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (child_item == null)
			return;

		inventory_ui.Set_Drag_INVENTORY_UI(this, child_item);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		inventory_ui.pointer_hover = this;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		inventory_ui.pointer_hover = null;
	}
}
