using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public Vector3 position; // Position of the friend in the cinema
    public float loudness; // Level of loudness of the friend
    public float startingLoudness = 0f; // Initial loudness of the friend
    public float maxLoudness = 1.0f; // Maximum loudness value
    public float loudnessIncreaseRate = 0.05f; // Rate at which loudness increases over time 

    public float currentLoudness; // Current loudness of the friend

    public GameObject loudnessSphere; // Reference to the sphere representing loudness
    public float maxSphereScale = 1f; // Maximum scale of the loudness sphere
    public float loudnessMultiplier = 1f; // Multiplier to adjust loudness scale

    public GameManager manager;

    private void Start()
    {
        currentLoudness = startingLoudness; // Initialize loudness
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!manager.gameEnded)
        {
            // Increase loudness over time
            currentLoudness += loudnessIncreaseRate * Time.deltaTime;

            // Clamp loudness to stay within the range
            currentLoudness = Mathf.Clamp(currentLoudness, 0f, maxLoudness);

            ScaleLoudnessSphere();
        }        
    }

    // Method to reduce the loudness of the friend
    public void ReduceLoudness(float amount)
    {
        currentLoudness -= amount;
        if (currentLoudness < 0)
        {
            currentLoudness = 0;
        }
    }

    // Scale the loudness sphere based on current loudness level
    private void ScaleLoudnessSphere()
    {
        float scaleFactor = currentLoudness * loudnessMultiplier;
        scaleFactor = Mathf.Clamp(scaleFactor, 0f, maxSphereScale);
        loudnessSphere.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}

