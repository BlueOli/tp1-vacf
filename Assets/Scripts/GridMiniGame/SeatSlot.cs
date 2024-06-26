using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeatSlot : MonoBehaviour
{
    public GridManager gridManager;

    [SerializeField]
    public int isOcuppied = 0;

    public bool isLocked = false;

    private Image seatImage;

    private void Start()
    {
        seatImage = GetComponent<Image>();

        EventTrigger trigger = GetComponent<EventTrigger>();
        trigger.triggers.Clear();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnClick(); });
        trigger.triggers.Add(entry);

        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();

        UpdateColor();
    }

    public void OnClick()
    {
        Debug.Log("You clicked " + this.gameObject.name + " on " + this.transform.parent.name);
        Occupy();
    }

    public void Occupy()
    {
        if (isOcuppied == 0)
        {
            isOcuppied = 1;
            gridManager.seatSlotsList.Add(this);
        }
        else if (isOcuppied == 1 && !isLocked)
        {
            isOcuppied = 0; 
            gridManager.seatSlotsList.Remove(this);
        }

        if(gridManager.seatSlotsList.Count > gridManager.maxFriendSeats)
        {
            gridManager.seatSlotsList[0].isOcuppied = 0;
            gridManager.seatSlotsList[0].UpdateColor();
            gridManager.seatSlotsList.Remove(gridManager.seatSlotsList[0]);
        }

        UpdateColor();
    }

    public void BuySeat()
    {
        if (!isLocked)
        {
            isLocked = true;
        }
        UpdateColor();
    }

    public void UpdateColor()
    {
        switch (isOcuppied)
        {
            case 0:
                seatImage.color = Color.white;
                break;
            case 1:
                if (isLocked)
                {
                    seatImage.color = Color.blue;
                }
                else
                {
                    seatImage.color = Color.green;
                }
                break;
            case -1:
                seatImage.color = Color.red;
                break;
        }
    }
}
