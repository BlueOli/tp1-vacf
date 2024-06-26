using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardNumberManager : MonoBehaviour
{
    public int[] securityNumber = new int[3];
    public Text securityText;

    public void Start()
    {
        securityNumber[0] = 0;
        securityNumber[1] = 0;
        securityNumber[2] = 0;

        UpdateNumber();
    }

    public void ChangeNumber(int value)
    {
        if(value == 1 || value == -1)
        {
            securityNumber[0] += value;
            if (securityNumber[0] < 0)
            {
                securityNumber[0] = 9;
            }
            else if (securityNumber[0] > 9)
            {
                securityNumber[0] = 0;
            }
        }
        else if (value == 10 || value == -10)
        {
            value /= 10;
            securityNumber[1] += value;
            if (securityNumber[1] < 0)
            {
                securityNumber[1] = 9;
            }
            else if (securityNumber[1] > 9)
            {
                securityNumber[1] = 0;
            }
        }
        else if (value == 100 || value == -100)
        {
            value /= 100;
            securityNumber[2] += value;
            if (securityNumber[2] < 0)
            {
                securityNumber[2] = 9;
            }
            else if (securityNumber[2] > 9)
            {
                securityNumber[2] = 0;
            }
        }

        UpdateNumber();
    }

    public void UpdateNumber()
    {
        securityText.text = (securityNumber[2] + " " + securityNumber[1] + " " + securityNumber[0]);
    }

    public int GetFullNumber()
    {
        int fullNum = securityNumber[2] * 100 + securityNumber[1] * 10 + securityNumber[0];
        return fullNum;
    }
}
