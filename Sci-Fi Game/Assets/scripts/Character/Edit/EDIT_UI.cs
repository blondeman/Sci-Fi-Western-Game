using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDIT_UI : MonoBehaviour
{
	public CHARACTER_EDIT character_edit;
	public Transform content;

	private void Start()
	{
		Create_Sliders_EDIT_UI();
	}

	public void Create_Sliders_EDIT_UI()
	{
		string[] array = character_edit.Get_Blend_Shape_Names_CHARACTER_EDIT();
		for (int i = 0;i< array.Length;i++)
		{			
			EDIT_SLIDER slider = Instantiate(PREFABS.instance.ui_edit_slider, content).GetComponent<EDIT_SLIDER>();
			slider.transform.name = "Slider (" + array[i] + ")";
			if (array[i].Equals(array[i + 1]))
			{
				slider.Init(this, array[i], true);
				i++;
			}
			else
			{
				slider.Init(this, array[i], false);
			}
		}
	}

	public void Update_UI(string blend_shape_name, float value)
	{
		if(character_edit)
			character_edit.Set_Blend_Shape_CHARACTER_EDIT(blend_shape_name, value);
	}
}
