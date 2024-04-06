using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isSwitchOn;
    public bool canBeSwtiched = false;
    private SwitchColor switchColor;

    public void Start()
    {
        switchColor = this.transform.GetComponentInChildren<SwitchColor>();

        switchColor.UpdateColor(isSwitchOn);
    }

    public void SwitchState()
    {
        if(canBeSwtiched)
        {
            isSwitchOn = !isSwitchOn;
            UpdateColor(isSwitchOn);
        }
    }

    public void SwitchOn()
    {
        if (canBeSwtiched)
        {
            isSwitchOn = true;
            UpdateColor(isSwitchOn);
        }

    }
    public void SwitchOff()
    {
        if (canBeSwtiched)
        {
            isSwitchOn = false;
            UpdateColor(isSwitchOn);
        }
    }

    public void UpdateColor(bool state)
    {
        if (switchColor != null)
        {
            switchColor.UpdateColor(state);
        }
    }
}
