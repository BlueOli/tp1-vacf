using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathFloor : MonoBehaviour
{
    public GameObject ResurrectFloor;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            player = other.transform.parent.gameObject;
            DeathFall(); // Repositions the player when it touches the trigger
        }
    }

    public void DeathFall()
    {
        float fraction = ResurrectFloor.transform.localScale.x / this.transform.localScale.x;
        Debug.Log(fraction);
        Vector3 pos = new Vector3(player.transform.position.x * -fraction,
                                  ResurrectFloor.transform.position.y,
                                  player.transform.position.z * -fraction);

        player.transform.position = pos;
    }
}
