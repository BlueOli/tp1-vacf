using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInputManager : MonoBehaviour
{
    public List<CardSlotHold> slots = new List<CardSlotHold>();
    public Color currentColor;
    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        buyButton.GetComponent<Button>().interactable = false;
        GetCardSlots();
    }

    public void GetCardSlots()
    {
        slots.Clear();
        foreach (Transform transform in transform)
        {
            slots.Add(transform.GetComponent<CardSlotHold>());
        }
    }

    public bool CheckSingleCardInput(CardGenerator card, CardSlotHold cardSlot)
    {
        bool canInputCard = false;

        GetCardSlots();
        bool allNull = true;
        int index = 0;
        int cardIndex = 0;
        foreach (CardSlotHold slot in slots)
        {
            if(slot.card != null) allNull = false;
            if (cardSlot == slot) cardIndex = index;
            index++;
        }

        if(cardSlot.card != null)
        {
            return canInputCard;
        }

        if(allNull || currentColor == card.color)
        {
            switch (cardIndex)
            {
                case 0:
                    if (card.isLeft) canInputCard = true;
                    Debug.Log("Found left pos");
                    break;
                case 1:
                    if (card.isMiddle) canInputCard = true;
                    Debug.Log("Found mid pos");
                    break;
                case 2:
                    if (card.isRight) canInputCard = true;
                    Debug.Log("Found right pos");
                    break;
            }
        }

        if (canInputCard && currentColor != null)
        {
            currentColor = card.color;
        }

        return canInputCard;
    }

    public bool CheckIfAllNumbers()
    {
        bool allNumbersIn = true;

        foreach (CardSlotHold slot in slots)
        {
            if (slot.card == null) allNumbersIn = false;
        }

        if (allNumbersIn)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

        return allNumbersIn;
    }

    public void ClearInput()
    {
        foreach (CardSlotHold slot in slots)
        {
            GameObject.Destroy(slot.card.gameObject);
        }
    }
}
