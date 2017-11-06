using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragMove : MonoBehaviour
{
    private Vector3 originalDragPosition;
	private Vector3 dragPointerPosition;
    private bool drag = false;

	void LateUpdate()
	{
		if (Input.GetMouseButton(1))
		{
			dragPointerPosition = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
			if (drag == false)
			{
				drag = true;
				originalDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
		else
		{
			drag = false;
		}
		if (drag == true)
		{
			Camera.main.transform.position = originalDragPosition - dragPointerPosition;
		}
		
	}
}
