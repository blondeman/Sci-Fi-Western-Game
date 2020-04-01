using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_OBJECT : ENTITY
{
	public SpriteRenderer sprite_renderer;
	public float pickup_countdown;
	[HideInInspector] public ITEM_DATA item_data;
	[HideInInspector] public int count;

	public void Initialize_ITEM_OBJECT(ITEM_DATA item_data, int count)
	{
		StartCoroutine(Countdown());
		this.item_data = item_data;
		this.count = count;
		sprite_renderer.sprite = item_data.equip_sprite;
	}

	private new void OnDestroy()
	{
		ITEM_LIST.instance.item_objects.Remove(this);
		base.OnDestroy();
	}

	public IEnumerator Countdown()
	{
		transform.tag = "Item_Dropped";
		yield return new WaitForSeconds(pickup_countdown);
		transform.tag = "Item";
	}
}
