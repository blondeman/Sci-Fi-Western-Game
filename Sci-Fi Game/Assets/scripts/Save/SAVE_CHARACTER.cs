using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class SAVE_CHARACTER
{
	public static void Write_To_File_SAVE_CHARACTER(CHARACTER_HEALTH health_data, CHARACTER_MOVE move_data, int character_id)
	{
		JSONObject node = new JSONObject();
		node.Add("X", CHARACTER_MOVE.Get_World_Pos_CHARACTER_MOVE(move_data.transform).x);
		node.Add("Y", CHARACTER_MOVE.Get_World_Pos_CHARACTER_MOVE(move_data.transform).y);
		node.Add("Current_Health", health_data.current_health);
		node.Add("Max_Health", health_data.max_health);

		string path = Path.Combine(Application.dataPath, "Save_Data/Character_Data/Character" + character_id);

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path = path + "/Stats.json";

		string write = node.ToString(2);
		if (File.Exists(path))
		{
			File.WriteAllText(path, write);
		}
		else
		{
			StreamWriter sr = File.CreateText(path);
			sr.Write(write);
			sr.Close();
		}
	}

	public static bool Read_From_File_SAVE_CHARACTER(CHARACTER_HEALTH health_data, CHARACTER_MOVE move_data, int character_id)
	{
		string path = Path.Combine(Application.dataPath, "Save_Data/Character_Data/Character" + character_id + "/Stats.json");

		if (File.Exists(path))
		{
			JSONObject node = (JSONObject)JSON.Parse(File.ReadAllText(path));

			Vector2 pos = new Vector2(node["X"] * TILE_RENDERER.instance.World_Size_TILE_RENDERER(), node["Y"] * TILE_RENDERER.instance.World_Size_TILE_RENDERER());
			move_data.transform.position = new Vector3(pos.x, 1, pos.y);
			health_data.current_health = node["Current_Health"]; 
			health_data.max_health = node["Max_Health"];

			return true;
		}
		return false;
	}
}
