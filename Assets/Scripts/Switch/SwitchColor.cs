using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    public Renderer rendererComponent;

    public void UpdateColor(bool state)
    {
        rendererComponent = GetComponentInChildren<Renderer>();

        UnityEngine.Color color = new();

        if(state)
        {
            color = UnityEngine.Color.green;
        }
        else
        {
            color = UnityEngine.Color.red;
        }

        rendererComponent.material.color = color;
    }
}
