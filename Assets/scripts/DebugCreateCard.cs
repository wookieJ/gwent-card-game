using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCreateCard : MonoBehaviour
{
    public Deck deck;

    public GameObject deckObject;
    
    void Awake()
    {
        deck = deckObject.GetComponent<Deck>(); 
    }    

    void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,40), "Zbuduj talię"))
        {
            if(deck.cardsInDeck.Capacity == 0)
                deck.buildDeck(12);
        }
        /*
        if (GUI.Button(new Rect(10, 100, 100, 40), "transform"))
        {
            
        }*/
    }
}
