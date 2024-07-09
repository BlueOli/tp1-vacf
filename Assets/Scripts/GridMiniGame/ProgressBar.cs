using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject nextScene;

    public GridManager gridManager;
    public Image progressBarRight;
    public Image progressBarLeft;// Reference to the UI Image
    public float fillTime = 10f; // Time in seconds to fill the bar

    private float elapsedTime = 0f;

    public bool stop = false;

    void Start()
    {
        progressBarRight.fillAmount = 1f; // Start with a full bar
        progressBarRight.color = Color.green; // Start with green color

        progressBarLeft.fillAmount = 1f; // Start with a full bar
        progressBarLeft.color = Color.green; // Start with green color
    }

    void Update()
    {
        if (!stop)
        {
            if (elapsedTime < fillTime)
            {
                elapsedTime += Time.deltaTime;
                float fillAmount = 1f - (elapsedTime / fillTime);
                progressBarRight.fillAmount = fillAmount;
                progressBarLeft.fillAmount = fillAmount;

                // Change color from green to red
                progressBarRight.color = Color.Lerp(Color.green, Color.red, elapsedTime / fillTime);
                progressBarLeft.color = Color.Lerp(Color.green, Color.red, elapsedTime / fillTime);
            }
            else
            {
                progressBarRight.fillAmount = 0f;
                progressBarRight.color = Color.red;
                progressBarLeft.fillAmount = 0f;
                progressBarLeft.color = Color.red;

                elapsedTime = 0f;
                if (gridManager != null)
                {
                    if (!gridManager.OcuppyRandomSeat())
                    {
                        stop = true;
                        gridManager.LoadGridSeats();
                        nextScene.SetActive(true);
                    }
                }
            }
        }        
    }
}
