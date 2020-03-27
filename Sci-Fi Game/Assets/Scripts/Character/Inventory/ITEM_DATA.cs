using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_item_data", menuName = "Create/Item")]
public class ITEM_DATA : ScriptableObject
{
	public string		description;
	public Sprite		ui_sprite;
	public Sprite		equip_sprite;
	public ITEM_TYPE[]	types;
	public ITEM_RARITY	rarity;

	public string Get_Type()
	{
		if (types.Length == 0)
			return "";

		switch (types[0])
		{
			case (ITEM_TYPE)0: return "Consumable";
			case (ITEM_TYPE)1: return "Weapon";
			case (ITEM_TYPE)2: return "Quest Item";
			case (ITEM_TYPE)3: return "Currency";
			case (ITEM_TYPE)4: return "Equipment";
			case (ITEM_TYPE)5: return "Placeable";
			default:return "No Type";
		}
	}

	public string Get_Rarity()
	{
		switch (rarity)
		{
			case (ITEM_RARITY)0: return "Common";
			case (ITEM_RARITY)1: return "Rare";
			case (ITEM_RARITY)2: return "Epic";
			default: return "No Rarity";
		}
	}
}

public enum ITEM_TYPE
{
	consumable,
	weapon,
	quest,
	currency,
	wearable,
	placeable
}

public enum ITEM_RARITY
{
	common,
	rare,
	epic
}