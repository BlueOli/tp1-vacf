using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnknownPerson : MonoBehaviour
{
    public Vector3 position; // Position of the unknown person in the cinema
    public float patience; // Level of patience of the unknown person
    public float maxPatience = 100f; // Maximum patience of the unknown person
    public float patienceDecreaseRate = 1f; // Rate at which patience decreases per second when friends are loud
    public float loudnessThreshold = 10f; // Threshold for friend loudness to start affecting patience
    public float loudnessMultiplier = 0.25f; // Multiplier for loudness affecting patience
    public Renderer rendererComponent; // Reference to the renderer component of the unknown person
    private Gradient patienceColorGradient; // Gradient for mapping patience values to colors
    public GameObject hotIcon;

    public float temperature;
    public float temperatureMultiplier = 0.1f;

    public GameManager manager;

    public void Start()
    {
        patience = maxPatience;


        // Create a gradient for mapping patience values to colors
        patienceColorGradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0].color = Color.green; // Green color for high patience
        colorKeys[0].time = 1.0f;
        colorKeys[1].color = Color.yellow; // Red color for low patience
        colorKeys[1].time = 0.5f;
        colorKeys[2].color = Color.red; // Red color for low patience
        colorKeys[2].time = 0.0f;
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[1].time = 0.5f;
        alphaKeys[2].alpha = 1.0f;
        alphaKeys[2].time = 1.0f;
        patienceColorGradient.SetKeys(colorKeys, alphaKeys);
        rendererComponent = this.GetComponentInChildren<Renderer>();

        // Set initial color based on starting patience value
        UpdateColor();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Method to reduce the patience of the unknown person
    public void ReducePatience(float amount)
    {
        patience -= amount;
        if (patience <= 0)
        {
            // Kick out the group if patience reaches 0
            GameManager.Instance.KickOut();
        }
        else
        {
            // Check if patience drops below the threshold for kicking out the group
            GameManager.Instance.CheckPatienceThreshold(patience);
        }
    }

    public float loudnessEffect;
    public float temperatureEffect;
    public float decreaseAmount;

    private void Update()
    {
        if (!manager.gameEnded)
        {
            // Check for nearby friends and reduce patience based on their loudness
            Friend[] friends = FindObjectsOfType<Friend>();
            foreach (Friend friend in friends)
            {
                float distance = Vector3.Distance(transform.position, friend.transform.position);
                if (distance < loudnessThreshold)
                {
                    loudnessEffect = Mathf.Clamp(friend.currentLoudness * loudnessMultiplier, 0f, maxPatience);
                    temperatureEffect = Mathf.Clamp(temperature * temperatureMultiplier, 0f, maxPatience);
                    decreaseAmount = patienceDecreaseRate * Time.deltaTime * loudnessEffect * temperatureEffect;
                    ReducePatience(decreaseAmount);
                }
            }

            UpdateColor();
        }        

        if(temperature > 10f)
        {
            if (!hotIcon.activeSelf)
            {
                hotIcon.SetActive(true);
            }
        }
        else
        {
            if (hotIcon.activeSelf)
            {
                hotIcon.SetActive(false);
            }
        }
    }


    private void UpdateColor()
    {
        // Map current patience value to a color using the gradient
        Color color = patienceColorGradient.Evaluate(patience / maxPatience);

        // Update the color of the renderer component
        rendererComponent.material.color = color;
    }
}

