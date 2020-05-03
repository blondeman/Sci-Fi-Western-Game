using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_EDIT : MonoBehaviour
{
	const int NULL = -1;

	public new SkinnedMeshRenderer renderer;

	public void Set_Blend_Shape_CHARACTER_EDIT(string name, float value)
	{
		int id_min = NULL;
		int id_max = NULL;
		for(int i = 0; i < renderer.sharedMesh.blendShapeCount; i++)
		{
			string iterator_name = renderer.sharedMesh.GetBlendShapeName(i);
			string suffix = iterator_name.Substring(iterator_name.Length - 3, 3);

			if (suffix.Equals("min") || suffix.Equals("max"))
			{
				iterator_name = iterator_name.Substring(0, iterator_name.Length - 4);
			}

			if (iterator_name.Equals(name))
			{
				if (suffix.Equals("min"))
				{
					id_min = i;
					continue;
				}
				if (suffix.Equals("max"))
				{
					id_max = i;
					continue;
				}

				id_min = i;
			}
		}

		if (id_min != NULL)
		{
			if (id_max != NULL)
			{
				value = Mathf.Clamp(value, -100, 100);

				if (value >= 0)
				{
					renderer.SetBlendShapeWeight(id_max, value);
					renderer.SetBlendShapeWeight(id_min, 0);
				}
				else
				{
					renderer.SetBlendShapeWeight(id_min, -value);
					renderer.SetBlendShapeWeight(id_max, 0);
				}
			}
			else
			{
				value = Mathf.Clamp(value, 0, 100);
				renderer.SetBlendShapeWeight(id_min, value);
			}
		}
	}

	public string[] Get_Blend_Shape_Names_CHARACTER_EDIT()
	{
		List<string> values = new List<string>();
		for (int i = 0; i < renderer.sharedMesh.blendShapeCount; i++)
		{
			string iterator_name = renderer.sharedMesh.GetBlendShapeName(i);
			string suffix = iterator_name.Substring(iterator_name.Length - 3, 3);

			if (suffix.Equals("min") || suffix.Equals("max"))
			{
				iterator_name = iterator_name.Substring(0, iterator_name.Length - 4);
			}

			values.Add(iterator_name);
		}
		return values.ToArray();
	}
}
