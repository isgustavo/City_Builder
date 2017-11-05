using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private BuildingItem item;
    [SerializeField]
    private GameObject priceGroup;
    [SerializeField]
    private Image itemSprite;
    [SerializeField]
    private Text itemPrice;
    [SerializeField]
    private RectTransform dragObject;
    [SerializeField]
    private RectTransform dragArea;

    private Vector2 originalDragPointerPosition;
    private Vector3 originalDragPosition;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = dragObject.localPosition;
        itemSprite.sprite = item.sprite;
        itemPrice.text = item.price.ToString("000");
    }

    public void OnBeginDrag(PointerEventData data)
	{
        originalDragPosition = dragObject.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, data.position, data.pressEventCamera, out originalDragPointerPosition);
        priceGroup.SetActive(false);
    }

	public void OnDrag(PointerEventData data)
	{
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, data.position, data.pressEventCamera, out localPointerPosition))
		{
			Vector3 offsetToOriginal = localPointerPosition - originalDragPointerPosition;
			dragObject.localPosition = originalDragPosition + offsetToOriginal;
		}
	}

    public void OnEndDrag(PointerEventData eventData)
    {
		Vector3 worldPoiterPosition;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(dragArea, dragObject.position, Camera.main, out worldPoiterPosition);
        GameObject building = Instantiate(item.buildItemPrefab, worldPoiterPosition, Quaternion.identity);
        building.GetComponentInChildren<BuildItem>().SetValues(item.sprite, item.contructionTime, item.profitValue, item.profitTime);
        dragObject.localPosition = originalPosition;
        priceGroup.SetActive(true);
    }

	
}
