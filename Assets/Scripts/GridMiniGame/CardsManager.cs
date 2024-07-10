using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public int[] randomSecurityNums = new int[3];
    public CardGenerator[] cards = new CardGenerator[9];

    public void Start()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i] = transform.GetChild(i).GetComponent<CardGenerator>();
        }

        GenerateRandomSecurityNumber();
        AssignNumbersToCards();
    }

    public void GenerateRandomSecurityNumber()
    {
        for(int i = 0;i < randomSecurityNums.Length; i++)
        {
            randomSecurityNums[i] = Random.Range(0, 1000);
            Debug.Log(randomSecurityNums[i]);
        }
    }

    public void AssignNumbersToCards()
    {
        List<CardGenerator> cardsNoNum = new List<CardGenerator>();
        foreach(CardGenerator card in cards)
        {
            cardsNoNum.Add(card);
        }

        for (int i = 0; i < randomSecurityNums.Length; i++)
        {
            int[] tempArray = new int[3];
            tempArray[0] = randomSecurityNums[i] % 10;
            tempArray[1] = (randomSecurityNums[i] / 10)  % 10;
            tempArray[2] = (randomSecurityNums[i] / 100) % 10;
            for (int j = 0; j<3; j++)
            {
                int rand = Random.Range(0, cardsNoNum.Count);
                CardGenerator tempCard = cardsNoNum[rand];
                tempCard.SetAndDisplayColor(i);
                tempCard.SetAndDisplayPosition(2 - j);
                tempCard.SetAndDisplayNumber(tempArray[j]);
                tempCard.numGroup = randomSecurityNums[i];
                cardsNoNum.Remove(tempCard);
            }         
        }
    }

    public void RemoveNumber(int pos)
    {
        foreach(CardGenerator card in cards)
        {
            if(card.numGroup == randomSecurityNums[pos])
            {
                HideCard(card);
            }
        }
        randomSecurityNums[pos] = -1;
    }

    public void HideCard(CardGenerator card)
    {
        card.SetAndDisplayColor(Color.grey);
    }

}
