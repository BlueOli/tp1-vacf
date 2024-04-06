using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACManager : MonoBehaviour
{
    public bool eventTriggered = false;
    public bool ACOut = false;
    public KeyCode endEventKey = KeyCode.E;
    public Text eventText;
    public Switch switchy;

    public void Start()
    {
        if(switchy != null)
        {
            if (ACOut)
            {
                switchy.SwitchOff();
            }
            else
            {
                switchy.SwitchOn();
            }
        }        
    }

    public void EventTrigger()
    {
        if (!eventTriggered)
        {
            eventTriggered = true;
            ACOut = true;
            eventText.text = "AC is not working!\nFix the thermostat";
            eventText.gameObject.SetActive(true);
            switchy.SwitchOff();
        }
    }

    private void Update()
    {
        if (eventTriggered && switchy.isSwitchOn)
        {
            eventTriggered = false;
            ACOut = false;
            eventText.text = "";
            eventText.gameObject.SetActive(false);
        }
    }
}
