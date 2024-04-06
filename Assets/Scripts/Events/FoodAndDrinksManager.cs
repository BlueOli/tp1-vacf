using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodAndDrinksManager : MonoBehaviour
{
    public bool eventTriggered = false;
    public bool foodAndDrinksReady = false;
    public KeyCode endEventKey = KeyCode.E;
    public Text eventText;

    // Start is called before the first frame update
    public void EventTrigger()
    {
        if (!eventTriggered)
        {
            eventTriggered = true;
            foodAndDrinksReady = true;
            eventText.text = "Look up!\nFood and Drinks are here!";
            eventText.gameObject.SetActive(true);
            eventTriggered = false;
        }
    }

    private void Update()
    {
        if (foodAndDrinksReady)
        {
            if (Input.GetKeyDown(endEventKey))
            {
                foodAndDrinksReady = false;
                Debug.Log("You grabbed the items");
                eventText.text = "";
            }
        }
    }
}
