using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_tile_data", menuName = "Create/Tile")]
public class TILE_DATA : ScriptableObject
{
	public int			id;
	public Sprite		sprite;
	public int			weight;
	public TILE_TYPE[]	types;
}

public enum TILE_TYPE
{
	floor,
	roofed,
	solid,
	transparent
}
