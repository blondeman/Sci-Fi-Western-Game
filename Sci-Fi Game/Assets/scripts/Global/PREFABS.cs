using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PREFABS : MonoBehaviour
{
	public static PREFABS	instance;
	public GameObject		item;
	public GameObject		ui_slot;
	public GameObject		ui_item;

	public GameObject		ui_edit_slider;

	public TILE_CHUNK		chunk;
	public TILE_OBJECT		tile;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
}
