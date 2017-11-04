using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class BuildItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private RectTransform dragObject;
    [SerializeField]
    private RectTransform dragArea;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalLocalPosition;

    Vector3 loca;
    private void Start()
    {
        loca = dragObject.localPosition;
    }

    public void OnBeginDrag(PointerEventData data)
	{
		originalLocalPosition = dragObject.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }

	public void OnDrag(PointerEventData data)
	{
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, data.position, data.pressEventCamera, out localPointerPosition))
		{
			Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
			dragObject.localPosition = originalLocalPosition + offsetToOriginal;
		}
	}

    public void OnEndDrag(PointerEventData eventData)
    {
		Vector3 worldPoiterPosition;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(dragArea, dragObject.position, Camera.main, out worldPoiterPosition);
        Instantiate(ItemPrefab, worldPoiterPosition, Quaternion.identity);
        dragObject.localPosition = loca;
    }

	
}
