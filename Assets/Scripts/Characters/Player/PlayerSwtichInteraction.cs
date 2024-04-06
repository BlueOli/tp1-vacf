using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwtichInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // Range within which the player can interact with friends
    public KeyCode interactionKey = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactionKey))
        {
            InteractWithSwitch();
        }
    }

    private void InteractWithSwitch()
    {
        // Get all friends within the interaction range of the player
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider col in hitColliders)
        {
            Switch switchy = col.GetComponentInParent<Switch>();
            if (switchy != null)
            {
                switchy.SwitchState();
            }
        }
    }
}
