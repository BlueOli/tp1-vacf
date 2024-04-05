using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player

    private Rigidbody rb; // Reference to the Rigidbody component

    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    private void Update()
    {
        if (!gameManager.gameEnded)
        {
            // Get horizontal and vertical input
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calculate movement direction
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

            // Normalize movement vector to ensure consistent speed regardless of direction
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            // Move the player
            rb.MovePosition(rb.position + movement);
        }        
    }
}
