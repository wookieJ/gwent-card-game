using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Deck activeDeck;
    private Desk desk;
    private Areas areas;
    public Card activeCard;
    public int activePlayerNumber;

    public GameObject deckObject;
    public GameObject deskObject;
    public GameObject areasObject;

    public GameObject playerNameTextObject;
    public Text playerNameText;

    public GameObject player1Object;
    public GameObject player2Object;
    public Player player1;
    public Player player2;

    public GameObject score1Object;
    public GameObject score2Object;
    public Text score1Text;
    public Text score2Text;

    private int state = 0;

    void Awake()
    {
        activeDeck = deckObject.GetComponent<Deck>();
        desk = deskObject.GetComponent<Desk>();
        areas = areasObject.GetComponent<Areas>();

        playerNameText = playerNameTextObject.GetComponent<Text>();
        score1Text = score1Object.GetComponent<Text>();
        score2Text = score2Object.GetComponent<Text>();

        player1 = player1Object.GetComponent<Player>();
        player2 = player2Object.GetComponent<Player>();

        activePlayerNumber = (int)PlayerNumber.PLAYER1;
    }    

    void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 45;

        if (GUI.Button(new Rect(55,30,330,120), "Zbuduj talię", buttonStyle))
        {
            if (activeDeck.cardsInDeck.Count == 0 && activeDeck.cardsInSwords.Count == 0 && activeDeck.cardsInBows.Count == 0 && activeDeck.cardsInTrebuchets.Count == 0)
            {
                player1.getDeck().buildDeck(12);
                player2.getDeck().buildDeck(10);
                player2.setDeckVisibility(false);
                activeDeck = player1.getDeck();
            }
        }
        if (GUI.Button(new Rect(55, 180, 330, 120), "Zmień gracza", buttonStyle))
        {
            switchPlayer();
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
            if (areas.getDeckColliderBounds().Contains(mouseRelativePosition) && activeDeck.cardsInDeck.Count > 0)
            {
                foreach (Card c in activeDeck.getCards())
                {
                    // if we click on card
                    if (c.getBounds().Contains(mouseRelativePosition))
                    {
                        activeDeck.disactiveAllInDeck();
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
                    activeCard.setActive(false);
                    if (activeDeck.addCardToSwords(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                    }
                }
            }
            else if (areas.getBowColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD)
                {
                    // TODO - dodawanie do listy z podziałem na grupy zasięgu (sword, bow, ...), na podstawie tego system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    if (activeDeck.addCardToBows(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                    }
                }
            }
            else if (areas.getTrebuchetColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD)
                {
                    // TODO - dodawanie do listy z podziałem na grupy zasięgu (sword, bow, ...), na podstawie tego system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    // TODO - is it enough to have controll under card in list? Position controll
                    if (activeDeck.addCardToTrebuchets(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                    }
                }
            }
            else
            {
                activeDeck.disactiveAllInDeck();
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
    /// Defined type of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET };

    /// <summary>
    /// Defined name of players
    /// </summary>
    private enum PlayerNumber { PLAYER1 = 1, PLAYER2 };

    /// <summary>
    /// Switch player - update active deck
    /// </summary>
    private void switchPlayer()
    {
        //desk.flipDesk();
        player1.getDeck().flipGroupCards();
        player2.getDeck().flipGroupCards();
        Vector3 tempVector = score1Text.transform.position;
        score1Text.transform.position = score2Text.transform.position;
        score2Text.transform.position = tempVector;

        if (activePlayerNumber == (int)PlayerNumber.PLAYER1)
        {
            this.activeDeck = player2.getDeck();
            player1.setDeckVisibility(false);
            player2.setDeckVisibility(true);            
            activePlayerNumber = (int)PlayerNumber.PLAYER2;
        }
        else if (activePlayerNumber == (int)PlayerNumber.PLAYER2)
        {
            this.activeDeck = player1.getDeck();
            player1.setDeckVisibility(true);
            player2.setDeckVisibility(false);
            activePlayerNumber = (int)PlayerNumber.PLAYER1;
        }
        
        playerNameText.text = "Player " + activePlayerNumber.ToString();
    }
}