using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotHold : MonoBehaviour
{
    public CardGenerator card;


    // Start is called before the first frame update
    void Start()
    {
        card = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void GetCard(CardGenerator c)
    {
        card = c;
    }
}
