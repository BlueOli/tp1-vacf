using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSpawner : MonoBehaviour
{
    public GameObject friendPrefab; // Reference to the friend prefab to spawn
    public Room room; // Reference to the room
    public int maxFriends = 10; // Maximum number of friends to spawn
    private int numFriendsSpawned = 0; // Number of friends already spawned

    public int minGroupSize = 2; // Minimum size of a friend group
    public int maxGroupSize = 3; // Maximum size of a friend group

    private void Start()
    {
        SpawnFriends();
    }

    public void SpawnFriends()
    {
        while (numFriendsSpawned < maxFriends)
        {
            int groupSize = Random.Range(minGroupSize, maxGroupSize + 1); // Randomly determine group size
            
            if(groupSize > maxFriends - numFriendsSpawned) groupSize = maxFriends - numFriendsSpawned;

            int randomRow = Random.Range(0, room.numRows); // Randomly select a row
            int randomStartSeat = Random.Range(0, room.numSeatsPerRow - groupSize + 1); // Randomly select a starting seat

            bool canSpawnGroup = true; // Flag to check if the group can be spawned
            for (int i = 0; i < groupSize; i++)
            {
                int currentSeatIndex = randomStartSeat + i;
                if (currentSeatIndex >= room.numSeatsPerRow || room.IsSeatOccupied(room.seats[randomRow, currentSeatIndex]))
                {
                    canSpawnGroup = false; // If any seat in the group is occupied or out of bounds, group cannot be spawned
                    break;
                }
            }

            if (canSpawnGroup)
            {
                for (int i = 0; i < groupSize; i++)
                {
                    Seat currentSeat = room.seats[randomRow, randomStartSeat + i];
                    Vector3 spawnPosition = currentSeat.transform.position;
                    Quaternion spawnRotation = currentSeat.transform.rotation;
                    Instantiate(friendPrefab, spawnPosition, spawnRotation);
                    room.OccupySeat(currentSeat);
                    numFriendsSpawned++; // Increment the number of friends spawned
                }
            }
        }
    }
}