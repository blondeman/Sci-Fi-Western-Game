using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_building_data", menuName = "Create/Building")]
public class BUILDING_DATA : ScriptableObject
{
	public int id;
	[Range(0f,1f)]
	public float richness;
	public int[,] data_array;
}
