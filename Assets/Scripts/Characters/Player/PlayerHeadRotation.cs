using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRotation : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 lastDirection;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            direction = Vector3.back;
        }

        if(Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
        }

        if(Input.GetKey(KeyCode.W))
        {
            direction = Vector3.forward;
        }

        if(Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
        }

        if(lastDirection != direction)
        {
            if(direction == Vector3.forward)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
                lastDirection = direction;
            }

            if (direction == Vector3.right)
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                lastDirection = direction;
            }

            if (direction == Vector3.back)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                lastDirection = direction;
            }

            if (direction == Vector3.left)
            {
                transform.localEulerAngles = new Vector3(0, 90, 0);
                lastDirection = direction;
            }
        }
    }
}
