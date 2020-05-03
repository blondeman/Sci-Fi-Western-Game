using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EDIT_SLIDER : MonoBehaviour
{
	public string	blend_shape_name;
	public Slider	slider;
	public Text		text;
	[HideInInspector] public EDIT_UI edit_ui;

	public void Init(EDIT_UI edit_ui, string name, bool min_max)
	{
		string[] words = name.Split('_');
		text.text = "";
		for (int i = 0;i<words.Length;i++)
		{
			words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1, words[i].Length - 1);
			text.text = text.text + words[i] + " ";
			
		}
		blend_shape_name = name;
		this.edit_ui = edit_ui;

		if (min_max)
		{
			slider.maxValue = 100;
			slider.minValue = -100;
		}
		else
		{
			slider.maxValue = 100;
			slider.minValue = 0;
		}

		slider.onValueChanged.AddListener(value => edit_ui.Update_UI(blend_shape_name, value));
	}
}
