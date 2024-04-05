using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // Range within which the player can interact with friends
    public float cooldownDuration = 5f; // Duration of the cooldown period
    public float reduceLoudnessEffect = 2f;

    public GameManager manager;

    public GameObject interactionFeedback; // Visual feedback for interaction

    private float cooldownTimer = 0f; // Timer for the cooldown period
    private Image cooldownUI; // Reference to the UI element for cooldown feedback

    private void Start()
    {
        
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (interactionFeedback != null)
        {
            cooldownUI = interactionFeedback.GetComponent<Image>();
            if (cooldownUI != null)
            {
                cooldownUI.fillAmount = 0f; // Set initial fill amount to 0%
                cooldownUI.gameObject.SetActive(false); // Hide the UI element initially
            }
            else
            {
                Debug.LogWarning("Interaction feedback object does not contain Image component.");
            }
        }
    }

    private void Update()
    {
        if(!manager.gameEnded)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;

                // Update UI element to reflect cooldown progress
                UpdateCooldownUI();

                if (cooldownTimer <= 0)
                {
                    cooldownUI.gameObject.SetActive(false); // Hide the UI element when cooldown ends
                }
            }
            else
            {
                // Check for player input to interact with friends
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractWithFriends();
                    cooldownTimer = cooldownDuration; // Start the cooldown timer
                    cooldownUI.gameObject.SetActive(true); // Show the UI element when cooldown starts
                }
            }
        }
    }

    private void InteractWithFriends()
    {
        // Get all friends within the interaction range of the player
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider col in hitColliders)
        {
            Friend friend = col.GetComponentInParent<Friend>();
            if (friend != null)
            {
                // Reduce the loudness of the friend
                friend.ReduceLoudness(reduceLoudnessEffect);
            }
        }
    }

    private void UpdateCooldownUI()
    {
        if (!manager.gameEnded)
        {
            if (cooldownUI != null)
            {
                cooldownUI.fillAmount = 1 - (cooldownTimer / cooldownDuration); // Update fill amount based on cooldown progress
            }
        }
    }
}