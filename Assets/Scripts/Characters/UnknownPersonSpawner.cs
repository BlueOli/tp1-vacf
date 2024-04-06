using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnknownPersonSpawner : MonoBehaviour
{
    public GameObject unknownPersonPrefab; // Reference to the unknown person prefab to spawn
    public Room room; // Reference to the room
    public int maxUnknownPersons; // Maximum number of unknown persons to spawn
    private int numUnknownPersonsSpawned = 0; // Number of unknown persons already spawned

    public void SpawnUnknownPersons()
    {
        if(maxUnknownPersons == 0)
        {
            maxUnknownPersons = room.CheckCurrentCapacity();
        }        

            for (int i = 0; i < room.numRows; i++)
            {
                for (int j = 0; j < room.numSeatsPerRow; j++)
                {
                    if (numUnknownPersonsSpawned >= maxUnknownPersons)
                    {
                        Debug.Log("Maximum number of unknown persons spawned.");
                        return; // Exit the loop if maximum unknown persons limit reached
                    }

                    Seat currentSeat = room.seats[i, j];
                    if (!room.IsSeatOccupied(currentSeat))
                    {
                        Vector3 spawnPosition = currentSeat.transform.position;
                        Quaternion spawnRotation = currentSeat.transform.rotation;
                        GameObject tempUnknownPerson = Instantiate(unknownPersonPrefab, spawnPosition, spawnRotation);
                        tempUnknownPerson.transform.SetParent(currentSeat.gameObject.transform);
                        room.OccupySeat(currentSeat);
                        numUnknownPersonsSpawned++; // Increment the number of unknown persons spawned
                    }
                }
            }
    }



}
