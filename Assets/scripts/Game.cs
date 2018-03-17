using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Deck deck;
    private Desk desk;
    private Areas areas;

    public GameObject deckObject;
    public GameObject deskObject;
    public GameObject areasObject;

    void Awake()
    {
        deck = deckObject.GetComponent<Deck>();
        desk = deskObject.GetComponent<Desk>();
        areas = areasObject.GetComponent<Areas>();
    }    

    void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,40), "Zbuduj talię"))
        {
            if(deck.cardsInDeck.Capacity == 0)
                deck.buildDeck(12);
        }
    }
    
    void Update()
    {
        Vector3 mouseRelativePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseRelativePosition.z = 0;

        //Debug.Log(mouseRelativePosition + " ; " + areas.getDeckColliderBounds().center);
        // if we click on deck collision
        if (areas.getDeckColliderBounds().Contains(mouseRelativePosition))
        {
            Debug.Log("Click on Deck");
             foreach(Card c in deck.getCards())
             {
                 // if we click on card
                 if(c.getBounds().Contains(mouseRelativePosition))
                 {
                     Debug.Log("Click on " + c);
                 }
             }
        }
    }
}
