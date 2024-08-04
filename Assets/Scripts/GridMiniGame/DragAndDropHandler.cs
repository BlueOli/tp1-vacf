using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private Canvas canvas;

    public CardInputManager cardInputManager;

    private void Start()
    {
        // Assuming the script is attached to a UI element and there's a Canvas in the hierarchy
        canvas = GetComponentInParent<Canvas>();
        cardInputManager = GameObject.Find("CardDragInput").GetComponent<CardInputManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out newPosition);
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent, true);

        // Check if the object is dropped on a slot
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = eventData.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("CardSlot"))
                {
                    if(cardInputManager.CheckSingleCardInput(this.GetComponent<CardGenerator>(), result.gameObject.GetComponentInParent<CardSlotHold>()))
                    {
                        result.gameObject.GetComponentInParent<CardSlotHold>().GetCard(this.GetComponent<CardGenerator>());
                        cardInputManager.CheckIfAllNumbers();

                        transform.SetParent(result.gameObject.transform, false);
                        transform.localPosition = Vector3.zero; // Adjust this if you want a specific offset 
                    }
                    else
                    {
                        Debug.Log("Cannot place card");
                        transform.position = originalPosition;
                    }                    

                    return;
                }
            }
        }

        transform.position = originalPosition;
    }
}
