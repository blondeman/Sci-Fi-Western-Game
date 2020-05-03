using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TOON_LIGHT : MonoBehaviour
{
	new Transform light = null;
	new Renderer renderer;

	private void Update()
	{
		if (!light)
		{
			light = GameObject.FindGameObjectWithTag("MainLight").transform;
			return; 
		}
		if (!renderer)
		{
			renderer = this.GetComponent<Renderer>();
			return;
		}

		for (int i = 0; i < renderer.sharedMaterials.Length; i++)
		{
			renderer.sharedMaterials[i].SetVector($"_LightDir", -light.transform.forward.normalized);
		}
	}
}
