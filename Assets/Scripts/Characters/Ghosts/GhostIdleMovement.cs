using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIdleMovement : MonoBehaviour
{
    public float amplitude = 0.075f; // The height of the up and down movement
    public float frequency = 1f; // The speed of the movement

    private Vector3 startPosition;
    private float randomOffset; // To make each ghost move differently

    void Start()
    {
        // Store the initial position of the ghost
        startPosition = transform.position;
        // Generate a random offset to make each ghost move differently
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculate the new Y position using a sinusoidal function
        float newY = startPosition.y + amplitude * Mathf.Sin((Time.time * frequency) + randomOffset);

        // Apply the new position to the ghost
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
