using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER : ENTITY
{
	public int					character_id;
	public CHARACTER_MOVE		move;
	public CHARACTER_HEALTH		health;
	public CHARACTER_INVENTORY	inventory;

	private void Start()
	{
		//Load_Character_CHARACTER();

		move.Init();
		health.Init();
		inventory.Init();
	}

	public void Save_Character_CHARACTER()
	{
		SAVE_CHARACTER.Write_To_File_SAVE_CHARACTER(health, move, character_id);
		SAVE_INVENTORY.Write_To_File_SAVE_INVENTORY(inventory, character_id);
	}

	public void Load_Character_CHARACTER()
	{
		SAVE_CHARACTER.Read_From_File_SAVE_CHARACTER(health, move, character_id);
		SAVE_INVENTORY.Read_From_File_SAVE_INVENTORY(inventory, character_id);
		inventory.Update_Inventory_CHARACTER_INVENTORY();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
			Save_Character_CHARACTER();
		if (Input.GetKeyDown(KeyCode.L))
			Load_Character_CHARACTER();
	}
}
