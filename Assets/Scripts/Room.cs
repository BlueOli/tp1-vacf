using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] rowObjects; // Array of row GameObjects
    public int numSeatsPerRow = 5; // Number of seats per row
    public float seatSpacing = 1.5f; // Spacing between seats
    public float rowHeightIncrement = 1f; // Increment in row height per row
    public int numRows = 0;
    public Seat[,] seats; // 2D array to store seats
    public List<Seat> occupiedSeats = new List<Seat>(); // List to store occupied seats
    public int maxCapacity;
    public int currentCapacity;

    public float temperature = 10f;

    private void Awake()
    {
        numRows = rowObjects.Length;
        maxCapacity = numRows * numSeatsPerRow;
        CreateSeats();
    }

    public void Update()
    {
        foreach (Transform t in transform)
        {
            if(t.gameObject.transform.childCount > 0)
            {
                if (t.gameObject.transform.GetChild(0).CompareTag("Unknown Person"))
                {
                    t.gameObject.GetComponentInChildren<UnknownPerson>().temperature = temperature;
                }
            }
                
        }      
    }

    private void CreateSeats()
    {
        seats = new Seat[rowObjects.Length, numSeatsPerRow];

        for (int i = 0; i < rowObjects.Length; i++)
        {
            GameObject rowObject = rowObjects[i];
            float rowHeight = i * rowHeightIncrement; // Calculate the height of the row
            for (int j = 0; j < numSeatsPerRow; j++)
            {
                Vector3 seatPosition = rowObject.transform.position + new Vector3(j * seatSpacing - rowObject.transform.localScale.x / 2 + 1f, 0.5f, 0.75f);
                Quaternion seatRotation = rowObject.transform.rotation;
                GameObject seatObject = new GameObject("Seat_" + i + "_" + j);
                seatObject.transform.SetParent(transform);
                seatObject.transform.SetLocalPositionAndRotation(seatPosition, seatRotation);
                seats[i, j] = seatObject.AddComponent<Seat>();
                seats[i, j].row = i;
                seats[i, j].seatNumber = j;
                seats[i, j].rowHeight = rowHeight; // Assign the height of the row to each seat
            }
        }
    }

    public void OccupySeat(Seat seat)
    {
        occupiedSeats.Add(seat);
    }

    public bool IsSeatOccupied(Seat seat)
    {
        return occupiedSeats.Contains(seat);
    }

    public bool CanAddFriend()
    {
        return occupiedSeats.Count < maxCapacity;
    }

    public int CheckCurrentCapacity()
    {
        return maxCapacity - occupiedSeats.Count;
    }

    public bool IsRoomFull()
    {
        if(occupiedSeats.Count == maxCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
