using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public bool eventTriggered = false;
    public bool powerOut = false;
    public KeyCode endEventKey = KeyCode.E;
    public Text eventText;
    public Switch switchy;

    public GameObject emergencyLights;
    public GameObject screenLight;

    public void Start()
    {
        if (switchy != null)
        {
            if (powerOut)
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
            powerOut = true;
            eventText.text = "The power went out!\nTurn the switch back on";
            eventText.gameObject.SetActive(true);
            switchy.SwitchOff();
            emergencyLights.SetActive(true);
            screenLight.SetActive(false);
        }        
    }

    private void Update()
    {
        if (eventTriggered && switchy.isSwitchOn)
        {
            eventTriggered = false;
            powerOut = false;
            eventText.text = "";
            eventText.gameObject.SetActive(false);
            emergencyLights.SetActive(false);
            screenLight.SetActive(true);
        }
    }
}
