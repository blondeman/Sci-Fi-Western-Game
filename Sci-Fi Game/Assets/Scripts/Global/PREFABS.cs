using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PREFABS : MonoBehaviour
{
	public static PREFABS	instance;
	public GameObject		item;
	public GameObject		tile;
	public GameObject		ui_slot;
	public GameObject		ui_item;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
}
