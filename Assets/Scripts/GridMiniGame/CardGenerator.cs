using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    public int number;
    public bool isLeft;
    public bool isRight;
    public bool isMiddle;
    public Color color;

    public Image backgtound;
    public Text numText;
    public GameObject left;
    public GameObject right;
    public GameObject middle;

    public void Awake()
    {
        backgtound = transform.GetChild(0).GetComponent<Image>();
        left = transform.GetChild(1).gameObject;
        middle = transform.GetChild(2).gameObject;
        right = transform.GetChild(3).gameObject;
        numText = transform.GetChild(4).GetComponent<Text>();
    }

    public void SetNumber(int num)
    {
        number = num;
    }

    public void DisplayNumber()
    {
        numText.text = number.ToString();
    }

    public void SetAndDisplayNumber(int num)
    {
        SetNumber(num);
        DisplayNumber();
    }

    public void SetColor(Color col)
    {
        color = col;
    }

    public void DisplayColor()
    {
        backgtound.color = color;
    }

    public void SetAndDisplayColor(Color col)
    {
        SetColor(col);
        DisplayColor();
    }

    public void SetAndDisplayColor(int col)
    {
        Color c = Color.white;

        switch (col)
        {
            case 0:
                c = Color.cyan;
                break;
            case 1:
                c = Color.magenta;
                break;
            case 2:
                c = Color.yellow;
                break;
        }

        SetColor(c);
        DisplayColor();
    }

    public void SetPosition(int pos)
    {
        switch (pos)
        {
            case 0:
                isLeft = true;
                isMiddle = false;
                isRight = false;
                break;
            case 1:
                isLeft = false;
                isMiddle = true;
                isRight = false;
                break;
            case 2:
                isLeft = false;
                isMiddle = false;
                isRight = true;
                break;
        }
    }

    public void DisplayPosition()
    {
        if(isLeft)
        {
            left.SetActive(true);
            middle.SetActive(false);
            right.SetActive(false);
        }
        else if(isMiddle)
        {
            middle.SetActive(true);
            right.SetActive(false);
            left.SetActive(false);
        }
        else if (isRight)
        {
            middle.SetActive(false);
            right.SetActive(true);
            left.SetActive(false);
        }
    }

    public void SetAndDisplayPosition(int pos)
    {
        SetPosition(pos);
        DisplayPosition();
    }
}
