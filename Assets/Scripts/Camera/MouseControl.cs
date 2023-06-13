using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl :MonoBehaviour 
{
	float anglex = 0.0f;
	float angley = 0.0f;
	float anglexMin = -90.0f;
	float anglexMax = 90.0f;
	float scale = 1.0f;
	float scaleMin = 0.5f;
	float scaleMax = 2.0f;
	float transitionScale = 1.0f;
	Vector3 startPosition;
	Quaternion startRotation;
	float startDistance;
	Vector3 lastPosition;
	public GameObject model;

	void Start () 
	{
		startPosition = new Vector3(0.0f, 0.0f, -5.0f);
		startRotation = Quaternion.Euler (Vector3.zero);
	}

	void Update ()
	{
		#if UNITY_EDITOR||UNITY_WEBGL
		if (Input.GetMouseButton (0)) 
		{
			anglex += Input.GetAxis ("Mouse Y") * 10.0f;
			angley += Input.GetAxis ("Mouse X") * 10.0f;
		}
		scale += Input.GetAxis ("Mouse ScrollWheel") / 5.0f;
		#else
		if (Input.touches.Length != 0)
		{
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Began)
			{
				lastPosition = touch.position;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				anglex += (touch.position.y - lastPosition.y) / 5.0f;
				angley += (touch.position.x - lastPosition.x) / 2.5f;
				lastPosition = touch.position;
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				if (Input.touchCount > 1)
				{
					lastPosition = Input.GetTouch (1).position;
				}
			}
		}
		if (Input.touches.Length == 2) 
		{
			Touch touch1 = Input.touches[0];
			Touch touch2 = Input.touches[1];
			float disx = touch1.position.x - touch2.position.x;
			float disy = touch1.position.y - touch2.position.y;
			float distance = Mathf.Sqrt(disx * disx + disy * disy);
			if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began) 
			{
				startDistance = distance;
			} 
			else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) 
			{
				float change = (distance - startDistance) / 100.0f;
				if (!float.IsNaN (change)) 
				{
					scale += change;
					startDistance = distance;
				}
			}
		}
		#endif

		if (anglex < anglexMin) 
		{
			anglex = anglexMin;
		}
		if (anglex > anglexMax)
		{
			anglex = anglexMax;
		}
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.RotateAround (Vector3.zero, Vector3.left, anglex);
		transform.RotateAround (Vector3.zero, Vector3.up, angley);

		if (scale < scaleMin) 
		{
			scale = scaleMin;
		}
		if (scale > scaleMax) 
		{
			scale = scaleMax;
		}
		transitionScale += (scale - transitionScale) / 5.0f;
		if (model != null) 
		{
			model.transform.localScale = new Vector3 (transitionScale, transitionScale, transitionScale);
		}
	}

	public void Reset()
	{
		anglex = 0.0f;
		angley = 0.0f;
		scale = 1.0f;
		transitionScale = 1.0f;
	}
}