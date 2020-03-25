using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_OBJECT : MonoBehaviour
{
	public SpriteRenderer sprite_renderer;
	ITEM_DATA item_data;
	int count;

	public void Initialize_ITEM_OBJECT(ITEM_DATA item_data, int count)
	{
		this.item_data = item_data;
		this.count = count;
		sprite_renderer.sprite = item_data.equip_sprite;
	}
}
