using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CUSTOM_BUTTON : MonoBehaviour, IPointerClickHandler
{
	public UnityEvent left_click;
	public UnityEvent middle_click;
	public UnityEvent right_click;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			Left_Click_CUSTOM_BUTTON();
		else if (eventData.button == PointerEventData.InputButton.Middle)
			Middle_Click_CUSTOM_BUTTON();
		else if (eventData.button == PointerEventData.InputButton.Right)
			Right_Click_CUSTOM_BUTTON();
	}

	public void Left_Click_CUSTOM_BUTTON()
	{
		left_click.Invoke();
	}

	public void Middle_Click_CUSTOM_BUTTON()
	{
		middle_click.Invoke();
	}

	public void Right_Click_CUSTOM_BUTTON()
	{
		right_click.Invoke();
	}
}
