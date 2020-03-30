using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class CUSTOM_BUTTON : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image image;
	Color default_color;
	public Color hover_color = new Color(210, 210, 210, 255);
	public Color pressed_color = new Color(180, 180, 180, 255);
	float lerp_time = 0.1f;

	Coroutine lerp_color;
	bool hovering = false;

	[Header("Events")]
	public UnityEvent left_click;
	public UnityEvent middle_click;
	public UnityEvent right_click;

	void Start()
	{
		default_color = image.color;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Lerp_Color_CUSTOM_BUTTON(pressed_color);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			Left_Click_CUSTOM_BUTTON();
		else if (eventData.button == PointerEventData.InputButton.Middle)
			Middle_Click_CUSTOM_BUTTON();
		else if (eventData.button == PointerEventData.InputButton.Right)
			Right_Click_CUSTOM_BUTTON();

		Lerp_Color_CUSTOM_BUTTON(hover_color);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Lerp_Color_CUSTOM_BUTTON(hover_color);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Lerp_Color_CUSTOM_BUTTON(default_color);
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

	public void Lerp_Color_CUSTOM_BUTTON(Color target_color)
	{
		if (lerp_color != null)
			StopCoroutine(lerp_color);
		if (gameObject.activeInHierarchy)
			lerp_color = StartCoroutine(Lerp_Color(target_color, lerp_time));
		else
			image.color = default_color;
	}

	public IEnumerator Lerp_Color(Color end, float time)
	{
		Color start = image.color;

		for (float t = 0; t < 1; t += Time.deltaTime / time)
		{
			yield return null;
			
			image.color = Color.Lerp(start, end, t);
		}

		image.color = end;
		lerp_color = null;
	}
}
