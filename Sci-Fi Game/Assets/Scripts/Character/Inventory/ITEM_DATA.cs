using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_item_data", menuName = "Create/Item")]
public class ITEM_DATA : ScriptableObject
{
	public Sprite		ui_sprite;
	public Sprite		equip_sprite;
	public ITEM_TYPE[]	types;
}

public enum ITEM_TYPE
{
	consumable,
	weapon,
	quest,
	currency,
	wearable,

}