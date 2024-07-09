using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Text maxFriendsText;

    [SerializeField]
    private LoadGridLayout gridSO;

    [SerializeField]
    private GameObject gridBox;

    [SerializeField]
    private CardNumberManager cardNumberManager;

    [SerializeField]
    private CardsManager cardsManager;

    [SerializeField]
    private TetrisPieceGenerator pieceGenerator;

    public int maxFriendSeatsTotal;
    public int maxFriendSeats = 6;
    public List<SeatSlot> seatSlotsList = new List<SeatSlot>();

    public void Start()
    {
        if (gridSO.maxFriends == 0 || gridSO.maxFriends < maxFriendSeatsTotal) 
        {
            gridSO.maxFriends = maxFriendSeatsTotal; 
        }
        else
        {
            maxFriendSeatsTotal = gridSO.maxFriends;
        }

        CheckMaxFriendSeatsTotal();
    }

    public void CheckMaxFriendSeatsTotal()
    {
        if (maxFriendSeats > maxFriendSeatsTotal)
        {
            maxFriendSeats = maxFriendSeatsTotal;
        }
        else
        {
            maxFriendSeats = 6;
        }

        maxFriendsText.text = "Friends without seats: " + maxFriendSeatsTotal;
    }

    public void LoadGridSeats()
    {
        int i = 0;
        int j = 0;
        foreach(Transform t in gridBox.transform)
        {
            foreach (Transform trans in t)
            {
                SeatSlot seat = trans.GetComponent<SeatSlot>();
                gridSO.grid[i, j] = seat.isOcuppied;
                j++;
            }
            i++;
            j = 0;
        }
        //ShowGridSeats();
    }

    public void ShowGridSeats()
    {
        if (gridSO != null)
        {
            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    switch (gridSO.grid[i,j])
                    {
                        case 0:
                            Debug.Log("Free Seat");
                            break;
                        case 1:
                            Debug.Log("Friend Seat");
                            break;
                        case -1:
                            Debug.Log("Occupied Seat");
                            break;
                    }
                }
            }
        }
    }

    public void BuySeats()
    {
        if (CheckIfNumberIsValid(GetInputNumber()))
        {
            int i = 0;
            int j = 0;
            foreach (Transform t in gridBox.transform)
            {
                foreach (Transform trans in t)
                {
                    SeatSlot seat = trans.GetComponent<SeatSlot>();
                    if (seat.isOcuppied == 1 && !seat.isLocked)
                    {
                        seat.BuySeat();
                        maxFriendSeatsTotal--;
                        CheckMaxFriendSeatsTotal();
                    }
                    j++;
                }
                i++;
                j = 0;
            }
            seatSlotsList.Clear();
        }
        else
        {
            Debug.Log("The number is invalid");
        }

        LoadGridSeats();
    }

    public bool CheckIfNumberIsValid(int number)
    {
        bool isValid = false;

        for(int i = 0; i < cardsManager.randomSecurityNums.Length; i++)
        {
            if(number == cardsManager.randomSecurityNums[i])
            {
                isValid = true;
                Debug.Log("The number is valid");
                cardsManager.RemoveNumber(i);
                break;
            }
        }

        return isValid;
    }

    public int GetInputNumber()
    {
        int num = cardNumberManager.GetFullNumber();
        return num;
    }

    public bool OcuppyRandomSeat()
    {
        bool freeSeats = false;

        if(maxFriendSeatsTotal <= 0)
        {
            if (gridSO != null)
            {
                for (int i = 0; i < gridSO.grid.GetLength(0); i++)
                {
                    for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                    {
                        switch (gridSO.grid[i, j])
                        {
                            case 0:
                                Debug.Log("Autofill seat: " + i + " " + j);
                                SeatSlot teampSeat = gridBox.transform.GetChild(i).GetChild(j).GetComponent<SeatSlot>();
                                if (seatSlotsList.Contains(teampSeat))
                                {
                                    seatSlotsList.Remove(teampSeat);
                                }
                                teampSeat.isOcuppied = -1;
                                teampSeat.UpdateColor();
                                LoadGridSeats();
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            if (CheckFreeSeats())
            {
                bool foundFreeSeat = false;
                while (!foundFreeSeat)
                {
                    int x = UnityEngine.Random.Range(0, gridSO.grid.GetLength(0));
                    int y = UnityEngine.Random.Range(0, gridSO.grid.GetLength(1));
                    //Debug.Log(x + " " + y);
                    if (gridSO.grid[x, y] == 0)
                    {
                        foundFreeSeat = true;
                        SeatSlot teampSeat = gridBox.transform.GetChild(x).GetChild(y).GetComponent<SeatSlot>();
                        if (seatSlotsList.Contains(teampSeat))
                        {
                            seatSlotsList.Remove(teampSeat);
                        }
                        teampSeat.isOcuppied = -1;
                        teampSeat.UpdateColor();
                        freeSeats = true;
                        LoadGridSeats();
                    }
                }
            }
            else
            {
                BuyRemainingSeats();
            }
        }        

        return freeSeats;
    }

    public bool CheckFreeSeats()
    {
        bool stillFreeSeats = false;

        int freeSeats = 0;

        if (gridSO != null)
        {
            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    switch (gridSO.grid[i, j])
                    {
                        case 0:
                            freeSeats++;
                            break;
                        case 1:
                            if(!gridBox.transform.GetChild(i).GetChild(j).GetComponent<SeatSlot>().isLocked)
                            {
                                freeSeats++;
                            }
                            break;
                        case -1:
                            //Debug.Log("Occupied Seat");
                            break;
                    }
                }
            }
        }

        if(freeSeats > maxFriendSeatsTotal)
        {
            stillFreeSeats = true;
        }

        return stillFreeSeats;
    }

    public void BuyRemainingSeats()
    {
        maxFriendSeats = maxFriendSeatsTotal;
        if (gridSO != null)
        {
            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    switch (gridSO.grid[i, j])
                    {
                        case 0:
                            Debug.Log("Autobuying seat: " + i + " " + j);
                            gridBox.transform.GetChild(i).GetChild(j).GetComponent<SeatSlot>().Occupy();
                            break;
                    }
                }
            }
        }

        foreach (Transform t in gridBox.transform)
        {
            foreach (Transform trans in t)
            {
                SeatSlot seat = trans.GetComponent<SeatSlot>();
                if (seat.isOcuppied == 1 && !seat.isLocked)
                {
                    seat.BuySeat();
                    maxFriendSeatsTotal--;
                    CheckMaxFriendSeatsTotal();
                }
            }
        }
        seatSlotsList.Clear();

        LoadGridSeats();
    }

    public void BuySeatsByButton()
    {
        Vector2Int[] buyPositions = { };

        int x = 0;
        int y = 4;
        foreach (Transform t in gridBox.transform)
        {
            foreach (Transform trans in t)
            {
                SeatSlot seat = trans.GetComponent<SeatSlot>();
                if (seat.isOcuppied == 1 && !seat.isLocked)
                {
                    Array.Resize(ref buyPositions, buyPositions.Length + 1);
                    buyPositions[buyPositions.Length - 1] = new Vector2Int(x, y);
                }
                x++;
            }
            y--;
            x = 0;
        }

        CheckFriendTetris(buyPositions);

        if (CheckIfNumberIsValid(GetInputNumber()))
        {
            int i = 0;
            int j = 0;
            foreach (Transform t in gridBox.transform)
            {
                foreach (Transform trans in t)
                {
                    SeatSlot seat = trans.GetComponent<SeatSlot>();
                    if (seat.isOcuppied == 1 && !seat.isLocked)
                    {
                        seat.BuySeat();
                        maxFriendSeatsTotal--;
                        //Debug.Log("Max friends total: " + maxFriendSeatsTotal);
                        CheckMaxFriendSeatsTotal();
                    }
                    j++;
                }
                i++;
                j = 0;
            }
            seatSlotsList.Clear();
        }
        else
        {
            Debug.Log("The number is invalid");
        }

        LoadGridSeats();
    }

    public bool CheckFriendTetris(Vector2Int[] buyPositions)
    {
        bool foundTetris = false;

        foreach (TetrisPieceGenerator.TetrisPiece piece in pieceGenerator.pieces)
        {
            TetrisPieceGenerator.TetrisPiece tempPiece = new()
            {
                blocks = buyPositions,
                size = buyPositions.Length
            };

            if (piece.IsEqualShape(tempPiece))
            {
                Debug.Log("Shapes are equal");
                foundTetris = true;
                pieceGenerator.UseTetrisPiece(piece);
                break;
            }
        }

        return foundTetris;
    }

}
