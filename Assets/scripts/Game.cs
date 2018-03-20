using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Deck deck;
    private Desk desk;
    private Areas areas;
    private Card activeCard;

    public GameObject deckObject;
    public GameObject deskObject;
    public GameObject areasObject;

    private int state = 0;

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

    private enum Status{
        FREE,
        ACTIVE_CARD
    };

    void Update()
    {
        // vector of actual mouse position
        Vector3 mouseRelativePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseRelativePosition.z = -0.1f;

        if (Input.GetMouseButtonDown(0))
        {
            // if we click on deck collision
            if (areas.getDeckColliderBounds().Contains(mouseRelativePosition) && deck.cardsInDeck.Count > 0)
            {
                foreach (Card c in deck.getCards())
                {
                    // if we click on card
                    if (c.getBounds().Contains(mouseRelativePosition))
                    {
                        deck.disactiveAllInDeck();
                        activeCard = c;
                        c.setActive(true);
                        activeCard.transform.position += new Vector3(0, 0.2f, 0);
                        state = (int)Status.ACTIVE_CARD;
                    }
                }
            }
            // click on card sword group
            else if (areas.getSwordColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD)
                {
                    // TODO - dodawanie do listy z podziałem na grupy zasięgu (sword, bow, ...), na podstawie tego system rozmieszczania kart w grupie
                    // TODO - sprawzić czy activeCard ma wpływ na karty w decku
                    activeCard.setActive(false);
                    if(deck.cardsInSwords.Count < 6)
                        deck.addCardToSwords(activeCard);
                    deck.disactiveAllInDeck();
                    state = (int)Status.FREE;
                }
            }
            else if (areas.getBowColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD)
                {
                    // TODO - dodawanie do listy z podziałem na grupy zasięgu (sword, bow, ...), na podstawie tego system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    if (deck.cardsInBows.Count < 6)
                        deck.addCardToBows(activeCard);
                    deck.disactiveAllInDeck();
                    state = (int)Status.FREE;
                }
            }
            else if (areas.getTrebuchetColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD)
                {
                    // TODO - dodawanie do listy z podziałem na grupy zasięgu (sword, bow, ...), na podstawie tego system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    // TODO - is it enough to have controll under card in list? Position controll
                    if (deck.cardsInTrebuchets.Count < 6)
                        deck.addCardToTrebuchets(activeCard);
                    deck.disactiveAllInDeck();
                    state = (int)Status.FREE;
                }
            }
            else
            {
                deck.disactiveAllInDeck();
                activeCard = null;
                state = (int)Status.FREE;
            }
        }
    }

    /// <summary>
    /// Resolve new posiotion of each card in pointet group
    /// </summary>
    /// <param name="group">group of cards that we want to reorganize. Int value</param>
    public void reorganizeGroup(int group)
    {
        if (group == (int)CardGroup.DECK)
        {
            // odd number of cards
            /*if (deck.cardsInDeck.Count % 2 == 1)
            {
                deck.cardsInDeck[0].transform.position = areas.getDeckCenterVector();
            }*/
        }
        else if (group == (int)CardGroup.SWORD)
        {
        }
        else if (group == (int)CardGroup.BOW)
        {

        }
        else if (group == (int)CardGroup.TREBUCHET)
        {

        }
    }

    /// <summary>
    /// Defined typed of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET };
}