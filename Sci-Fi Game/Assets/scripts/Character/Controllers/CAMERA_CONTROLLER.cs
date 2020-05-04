using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CAMERA_CONTROLLER : MonoBehaviour
{
	new Camera			camera;
	public Transform	target;
	public float		smooth;
	Vector3				offset;

	[Header("Zoom Options")]
	public float	min_zoom;
	public float	max_zoom;
	public float	zoom_speed;
	public float	min_position;
	public float	max_position;

	float			zoom;
	float			rotation;
	float			position;
	float			current_value;
	Vector2			direction;

	private void Awake()
	{
		camera = GetComponent<Camera>();
		offset = transform.position - target.position;
	}

	public void Update()
	{
		zoom -= Input.GetAxisRaw("Mouse ScrollWheel") * zoom_speed;
		zoom = Mathf.Clamp(zoom, min_zoom, max_zoom);

		current_value = (zoom - min_zoom) / (max_zoom - min_zoom);
		direction = new Vector2(target.position.z - transform.position.z, transform.position.y - target.position.y);
		rotation = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
		position = Mathf.Lerp(min_position, max_position, current_value);
	}

	private void LateUpdate()
	{
		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, Time.deltaTime*smooth);
		transform.position = Vector3.Lerp(transform.position, target.position + offset + new Vector3(0,0, position), Time.deltaTime * smooth);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation, 0, 0), Time.deltaTime * smooth * 2);
	}
}
