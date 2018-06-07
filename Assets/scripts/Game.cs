using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

// TODO - check if SetActive method of GameObject objects works - yes? Correct system of transparent object
// TODO - areas size matched to deck size so player can disactivate card clicking into deck area at the edges

// TODO - Drag and drop system
// TODO - Allign cards with value - increasing. Sorting method to replace cards.
// TODO - Switching players canvas - image like in real gwent
// TODO - Hide active card after second click on it
// TODO - BUG when players put all cards in first tour

public class Game : MonoBehaviour
{
    private Card activeCard;
    private Card activeShowingCard;
    private static int activePlayerNumber;
    private static int state = (int)Status.FREE;
    private static int gameStatus = (int)GameStatus.TOUR1;

    private GameObject deckObject;
    private GameObject deskObject;
    private GameObject areasObject;

    private Deck activeDeck;
    private Desk desk;
    private Areas areas;

    private GameObject playerDownNameTextObject;
    private Text playerDownNameText;

    private GameObject playerUpNameTextObject;
    private Text playerUpNameText;

    private GameObject player1Object;
    private GameObject player2Object;
    private Player player1;
    private Player player2;

    private GameObject score1Object;
    private GameObject score2Object;
    private GameObject score3Object;
    private GameObject score4Object;
    private GameObject score5Object;
    private GameObject score6Object;
    private GameObject score7Object;
    private GameObject score8Object;
    private GameObject cardNumber1Object;
    private GameObject cardNumber2Object;
    private Text score1Text;
    private Text score2Text;
    private Text score3Text;
    private Text score4Text;
    private Text score5Text;
    private Text score6Text;
    private Text score7Text;
    private Text score8Text;
    private Text cardNumber1;
    private Text cardNumber2;

    private GameObject buttonObject;
    private Button button;

    public GameObject endPanelObject;
    public GameObject endTextObject;
    public Text endText;

    private GameObject giveUpButtonObject;

    void Awake()
    {
        player1Object = GameObject.Find("Player1");
        player2Object = GameObject.Find("Player2");
        player1 = player1Object.GetComponent<Player>();
        player2 = player2Object.GetComponent<Player>();

        deskObject = GameObject.Find("Desk");
        desk = deskObject.GetComponent<Desk>();

        areasObject = GameObject.Find("Areas");
        areas = areasObject.GetComponent<Areas>();

        playerDownNameTextObject = GameObject.Find("DownPlayerName");
        playerDownNameText = playerDownNameTextObject.GetComponent<Text>();

        playerUpNameTextObject = GameObject.Find("UpPlayerName");
        playerUpNameText = playerUpNameTextObject.GetComponent<Text>();

        score1Object = GameObject.Find("Score1");
        score2Object = GameObject.Find("Score2");
        score3Object = GameObject.Find("Score3");
        score4Object = GameObject.Find("Score4");
        score5Object = GameObject.Find("Score5");
        score6Object = GameObject.Find("Score6");
        score7Object = GameObject.Find("Score7");
        score8Object = GameObject.Find("Score8");
        cardNumber1Object = GameObject.Find("cardNumber1");
        cardNumber2Object = GameObject.Find("cardNumber2");
        score1Text = score1Object.GetComponent<Text>();
        score2Text = score2Object.GetComponent<Text>();
        score3Text = score3Object.GetComponent<Text>();
        score4Text = score4Object.GetComponent<Text>();
        score5Text = score5Object.GetComponent<Text>();
        score6Text = score6Object.GetComponent<Text>();
        score7Text = score7Object.GetComponent<Text>();
        score8Text = score8Object.GetComponent<Text>();
        cardNumber1 = cardNumber1Object.GetComponent<Text>();
        cardNumber2 = cardNumber2Object.GetComponent<Text>();

        buttonObject = GameObject.Find("Button");
        button = buttonObject.GetComponent<Button>();

        endPanelObject = GameObject.FindGameObjectWithTag("EndPanel");
        endPanelObject.transform.position = new Vector3(0,0,-1.8f);
        endTextObject = GameObject.FindGameObjectWithTag("EndText");
        endText = endTextObject.GetComponent<Text>();

        giveUpButtonObject = GameObject.FindGameObjectWithTag("giveUpButton");
        giveUpButtonObject.SetActive(true);

        endPanelObject.SetActive(false);

        activePlayerNumber = (int)PlayerNumber.PLAYER1;
        gameStatus = (int)GameStatus.TOUR1;
    }    

    void Start()
    {
        player1.getDeck().buildDeck(10);
        player2.getDeck().buildDeck(10);

        activePlayerNumber = (int)PlayerNumber.PLAYER1;

        initializePlayersDecks();
    }

    void initializePlayersDecks()
    {
        player1.getDeck().sendCardsToDeathList();
        player2.getDeck().sendCardsToDeathList();

        player1.moveCardsFromDeskToDeathArea(activePlayerNumber);
        player2.moveCardsFromDeskToDeathArea(activePlayerNumber);

        //Debug.Log("Deleted " + deleteAllCardClones() + " cards");

        // player1.reloadDeck();
        //player2.reloadDeck();

        Debug.Log("P1 amount of cards: " + player1.getDeck().cardsInDeck.Count);
        Debug.Log("P2 amount of cards: " + player2.getDeck().cardsInDeck.Count);

        player2.setDeckVisibility(false);
        activeDeck = player1.getDeck();

        if (player1.getDeck().cardsInDeck.Count > 0)
            activeCard = player1.getDeck().cardsInDeck[0];

        activeShowingCard = Instantiate(activeCard) as Card;
        activeShowingCard.transform.position = new Vector3(8.96f, 0, -0.1f);
        showActiveCard(false);

        reorganizeGroup();

        //player1.getDeck().cardsInDeck[0].setPower(10);
        //player2.getDeck().cardsInDeck[0].setPower(15);
    }

    private enum Status{
        FREE,
        ACTIVE_CARD,
        BLOCKED
    };

    /// <summary>
    /// Delete all clone cards
    /// </summary>
    /// <returns>Number of deleted cards</returns>
    /*public int deleteAllCardClones()
    {
        int cloneNumber = 0;
        GameObject[] cloneCards = GameObject.FindGameObjectsWithTag("CloneCard");
        cloneNumber = cloneCards.Length;

        foreach (GameObject go in cloneCards)
            GameObject.DestroyObject(go);

        return cloneNumber;
    }*/

    void Update()
    {
        // Updating numberOfCards
        // ---------------------------------------------------------------------------------------------------------------
        cardNumber1.text = player1.getDeck().cardsInDeck.Count.ToString();
        cardNumber2.text = player2.getDeck().cardsInDeck.Count.ToString();

        // Picking card
        // ---------------------------------------------------------------------------------------------------------------
        // vector of actual mouse position
        Vector3 mouseRelativePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseRelativePosition.z = -0.1f;
        if (Input.GetMouseButtonDown(0) && state != ((int)Status.BLOCKED))
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

                        showActiveCard(true);

                        activeCard.transform.position += new Vector3(0, 0.15f, 0);
                        state = (int)Status.ACTIVE_CARD;
                    }
                }
            }
            // manekin card on swords
            if (areas.getSwordColliderBounds().Contains(mouseRelativePosition) && state == (int)Status.ACTIVE_CARD && activeCard.getIsSpecial() == (int)TypeOfCard.MANEKIN)
            {
                foreach (Card c in activeDeck.getSwordCards())
                {
                    if (c.getBounds().Contains(mouseRelativePosition) && c.getIsSpecial() != (int)TypeOfCard.GOLD && c.getIsSpecial() != (int)TypeOfCard.GOLD_SPY && c.getIsSpecial() != (int)TypeOfCard.MANEKIN)
                    {
                        Debug.Log("Manekin target!");
                        activeCard.setActive(false);
                        activeDeck.moveCardToDeckFromSwords(c);
                        if (activeDeck.addCardToSwords(activeCard) == true)
                        {
                            activeDeck.disactiveAllInDeck();
                            state = (int)Status.FREE;

                            if (player1.isPlaying && player2.isPlaying)
                            {
                                Debug.Log("switchPlayer()");
                                switchPlayer();
                            }
                            else
                            {
                                reorganizeGroup();
                                state = (int)Status.FREE;
                                showActiveCard(false);
                            }
                        }
                        break;
                    }
                }
            }
            // manekin card on bows
            else if (areas.getBowColliderBounds().Contains(mouseRelativePosition) && state == (int)Status.ACTIVE_CARD && activeCard.getIsSpecial() == (int)TypeOfCard.MANEKIN)
            {
                foreach (Card c in activeDeck.getBowCards())
                {
                    if (c.getBounds().Contains(mouseRelativePosition) && c.getIsSpecial() != (int)TypeOfCard.GOLD && c.getIsSpecial() != (int)TypeOfCard.GOLD_SPY && c.getIsSpecial() != (int)TypeOfCard.MANEKIN)
                    {
                        activeCard.setActive(false);
                        activeDeck.moveCardToDeckFromBows(c);
                        if (activeDeck.addCardToBows(activeCard) == true)
                        {
                            activeDeck.disactiveAllInDeck();
                            state = (int)Status.FREE;

                            if (player1.isPlaying && player2.isPlaying)
                            {
                                Debug.Log("switchPlayer()");
                                switchPlayer();
                            }
                            else
                            {
                                reorganizeGroup();
                                state = (int)Status.FREE;
                                showActiveCard(false);
                            }
                        }
                        break;
                    }
                }
            }
            // manekin card on trebuchets
            else if (areas.getTrebuchetColliderBounds().Contains(mouseRelativePosition) && state == (int)Status.ACTIVE_CARD && activeCard.getIsSpecial() == (int)TypeOfCard.MANEKIN)
            {
                foreach (Card c in activeDeck.getTrebuchetCards())
                {
                    if (c.getBounds().Contains(mouseRelativePosition) && c.getIsSpecial() != (int)TypeOfCard.GOLD && c.getIsSpecial() != (int)TypeOfCard.GOLD_SPY && c.getIsSpecial() != (int)TypeOfCard.MANEKIN)
                    {
                        activeCard.setActive(false);
                        activeDeck.moveCardToDeckFromTrebuchets(c);
                        if (activeDeck.addCardToTrebuchets(activeCard) == true)
                        {
                            activeDeck.disactiveAllInDeck();
                            state = (int)Status.FREE;

                            if (player1.isPlaying && player2.isPlaying)
                            {
                                Debug.Log("switchPlayer()");
                                switchPlayer();
                            }
                            else
                            {
                                reorganizeGroup();
                                state = (int)Status.FREE;
                                showActiveCard(false);
                            }
                        }
                        break;
                    }
                }
            }
            // click on card sword group
            else if (areas.getSwordColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD && activeCard.getGroup() == (int)CardGroup.SWORD && activeCard.getIsSpecial() != (int)TypeOfCard.SPY  && activeCard.getIsSpecial() != (int)TypeOfCard.GOLD_SPY)
                {
                    // TODO - system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    if (activeDeck.addCardToSwords(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;

                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                }
            }
            else if (areas.getBowColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD && activeCard.getGroup() == (int)CardGroup.BOW)
                {
                    // TODO - system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    if (activeDeck.addCardToBows(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                }
            }
            else if (areas.getTrebuchetColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD && activeCard.getGroup() == (int)CardGroup.TREBUCHET)
                {
                    // TODO - system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    // TODO - is it enough to have controll under card in list? Position controll
                    if (activeDeck.addCardToTrebuchets(activeCard) == true)
                    {
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                }
            }
            // Putting Spy cards
            else if (areas.getSword2ColliderBounds().Contains(mouseRelativePosition))
            {
                // For spy card
                if (state == (int)Status.ACTIVE_CARD && (activeCard.getIsSpecial() == (int)TypeOfCard.SPY || activeCard.getIsSpecial() == (int)TypeOfCard.GOLD_SPY) && activeCard.getGroup() == (int)CardGroup.SWORD)
                {
                    // TODO - system rozmieszczania kart w grupie
                    activeCard.setActive(false);
                    if (activePlayerNumber == (int)PlayerNumber.PLAYER1)
                    {
                        player2.getDeck().addSpy(activeCard);
                        player1.getDeck().cardsInDeck.Remove(activeCard);
                        player1.getDeck().addTwoRandomCards();
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                    else
                    {
                        player1.getDeck().addSpy(activeCard);
                        player2.getDeck().cardsInDeck.Remove(activeCard);
                        player2.getDeck().addTwoRandomCards();
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                }
                else
                {
                    activeDeck.disactiveAllInDeck();
                    activeCard = null;
                    showActiveCard(false);
                    state = (int)Status.FREE;
                }
            }
            else if (areas.getSpecial1ColliderBounds().Contains(mouseRelativePosition))
            {
                if (state == (int)Status.ACTIVE_CARD && (activeCard.getIsSpecial() == (int)TypeOfCard.DESTROY || activeCard.getIsSpecial() == (int)TypeOfCard.WEATHER))
                {
                    activeCard.setActive(false);
                    activeCard.transform.position = areas.getSpecial1CenterVector();
                    activeDeck.addToSpecial(activeCard);

                    // destroy
                    if(activeCard.getIsSpecial() == 4)
                    {
                        if(activePlayerNumber == (int)PlayerNumber.PLAYER1)
                        {
                            player2.getDeck().removeMaxPowerCard();
                        }
                        else
                            player1.getDeck().removeMaxPowerCard();
                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                    // weather
                    else if(activeCard.getIsSpecial() == 5)
                    {
                        int weatherCardRange = activeCard.getPower() * -1;
                        player1.getDeck().applyWeatherEffect(weatherCardRange);
                        player2.getDeck().applyWeatherEffect(weatherCardRange);

                        if(weatherCardRange == 4)
                        {
                            player1.getDeck().deleteFromSpecial();
                            player2.getDeck().deleteFromSpecial();
                        }

                        activeDeck.disactiveAllInDeck();
                        state = (int)Status.FREE;
                        if (player1.isPlaying && player2.isPlaying)
                            switchPlayer();
                        else
                        {
                            reorganizeGroup();
                            state = (int)Status.FREE;
                            showActiveCard(false);
                        }
                    }
                }
            }
            // Ending player time, tour
            // TODO - Change gameStatus
            // ---------------------------------------------------------------------------------------------------------------
            if (player1.getDeck().cardsInDeck.Count == 0 && player1.isPlaying)
            {
                Debug.Log("Player1 has no cards");
                player1.isPlaying = false;
            }
            if (player2.getDeck().cardsInDeck.Count == 0 && player2.isPlaying)
            {
                Debug.Log("Player2 has no cards");
                player2.isPlaying = false;
            }
            if (player1.isPlaying == false && player2.isPlaying == false && gameStatus != (int)GameStatus.END)
            {
                player1.updateScore();
                player2.updateScore();
                Debug.Log("Both players have no cards");
                Debug.Log("P1: " + player1.score + ", P2: " + player2.score);
                // End of tour - check who won, subtract health, set new tour
                if (player1.score > player2.score)
                {
                    Debug.Log("player1.score > player2.score");
                    if (player2.health > 0)
                    {
                        Debug.Log("player2.health > 0");
                        // Player 1 won the tour
                        endText.text = "Gracz 1 wygrał!";
                        player2.health--;
                        player2.updateHealthDiamonds();
                    }
                    if (player2.health == 0)
                    {
                        // Player 1 won the game
                        player2.health = -1;
                    }
                }
                else if (player1.score < player2.score)
                {
                    Debug.Log("player1.score <= player2.score");
                    if (player1.health > 0)
                    {
                        Debug.Log("player1.health > 0");
                        // Player 2 won the tour
                        endText.text = "Gracz 2 wygrał!";
                        player1.health--;
                        player1.updateHealthDiamonds();
                    }
                    if (player1.health == 0)
                    {
                        // Player 2 won the game
                        player1.health = -1;
                    }
                }
                else
                {
                    if (player1.health > 0)
                        player1.health--;
                    if (player2.health > 0)
                        player2.health--;
                    endText.text = "Remis!";
                    if (player1.health == 0)
                        player1.health = -1;
                    if (player2.health == 0)
                        player2.health = -1;

                    player1.updateHealthDiamonds();
                    player2.updateHealthDiamonds();
                }

                // game over
                if (player1.health == -1 && player2.health == -1)
                {
                    Debug.Log("REMIS!");
                    gameStatus = (int)GameStatus.END;

                    // TODO - zawsze przy remisie PLAYER1 - może losować?
                    //activePlayerNumber = (int)PlayerNumber.PLAYER1;
                }
                else if (player1.health == -1)
                {
                    Debug.Log("P1 WON!");
                    gameStatus = (int)GameStatus.END;
                    // activePlayerNumber = (int)PlayerNumber.PLAYER1;
                }
                else if (player2.health == -1)
                {
                    Debug.Log("P2 WON!");
                    gameStatus = (int)GameStatus.END;
                    //  activePlayerNumber = (int)PlayerNumber.PLAYER2;
                }

                if (gameStatus != (int)GameStatus.END)
                {
                    Debug.Log("End tour()");
                    endTour();
                }
                else
                {
                    gameOver();

                    //Debug.Log("Deleting!");
                    // deleteAllCardClones();
                }
            }
        }
    }

    private void gameOver()
    {
        endPanelObject.SetActive(true);
        giveUpButtonObject.SetActive(false);
        StartCoroutine(GameOverScreen(2f));
    }

    IEnumerator GameOverScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        // after
        endText.text = "Game Over\n";
        if(player1.health == -1 && player2.health == -1)
            endText.text += "\nRemis";
        else if(player2.health == -1)
            endText.text += "\nGracz 1 wygrał";
        else if(player1.health == -1)
            endText.text += "\nGracz 2 wygrał";
    }

    /// <summary>
    /// Resolve new posiotion of each card in each group
    /// </summary>
    public void reorganizeGroup()
    {
        
            if (activeDeck.cardsInDeck.Count > 0)
            {
                Vector3 centerVector = areas.getDeckCenterVector() + new Vector3(0, -0.1456f, -0.1f);

                // For odd number of cards
                if (activeDeck.cardsInDeck.Count % 2 == 1)
                {
                    int j = 1;
                    activeDeck.cardsInDeck[0].transform.position = centerVector;

                    for (int i = 1; i < activeDeck.cardsInDeck.Count; i++)
                    {
                        // TODO - Expand range of max 8 cards in group and dynamically change offset between ech card in groups. Add functionallity of schowing one card after another (changing z position).
                        activeDeck.cardsInDeck[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                        j *= -1;
                        if (i % 2 == 0)
                            j++;
                    }
                }
                else
                {
                    int j = 1;
                    activeDeck.cardsInDeck[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                    activeDeck.cardsInDeck[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                    for (int i = 2; i < activeDeck.cardsInDeck.Count; i++)
                    {
                        activeDeck.cardsInDeck[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                        j *= -1;
                        if (i % 2 == 1)
                            j++;
                    }
                }
            }
            if (activeDeck.cardsInSwords.Count > 0)
            {
                Vector3 centerVector = areas.getSwordsCenterVector() + new Vector3(0, -0.1456f, -0.1f);

                // For odd number of cards
                if (activeDeck.cardsInSwords.Count % 2 == 1)
                {
                    int j = 1;
                    activeDeck.cardsInSwords[0].transform.position = centerVector;

                    for(int i = 1; i<activeDeck.cardsInSwords.Count; i++)
                    {
                        activeDeck.cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                        j *= -1;
                        if (i % 2 == 0)
                            j++;
                    }
                }
                else
                {
                    int j = 1;
                    activeDeck.cardsInSwords[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                    activeDeck.cardsInSwords[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                    for (int i = 2; i < activeDeck.cardsInSwords.Count; i++)
                    {
                        activeDeck.cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                        j *= -1;
                        if (i % 2 == 1)
                            j++;
                    }
                }
            }
        if (activeDeck.cardsInBows.Count > 0)
        {
            Vector3 centerVector = areas.getBowsCenterVector() + new Vector3(0, -0.1456f, -0.1f);

            // For odd number of cards
            if (activeDeck.cardsInBows.Count % 2 == 1)
            {
                int j = 1;
                activeDeck.cardsInBows[0].transform.position = centerVector;

                for (int i = 1; i < activeDeck.cardsInBows.Count; i++)
                {
                    activeDeck.cardsInBows[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 0)
                        j++;
                }
            }
            else
            {
                int j = 1;
                activeDeck.cardsInBows[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                activeDeck.cardsInBows[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                for (int i = 2; i < activeDeck.cardsInBows.Count; i++)
                {
                    activeDeck.cardsInBows[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 1)
                        j++;
                }
            }
        }
        if (activeDeck.cardsInTrebuchets.Count > 0)
        {
            Vector3 centerVector = areas.getTrebuchetsCenterVector() + new Vector3(0, -0.1456f, -0.1f);

            // For odd number of cards
            if (activeDeck.cardsInTrebuchets.Count % 2 == 1)
            {
                int j = 1;
                activeDeck.cardsInTrebuchets[0].transform.position = centerVector;

                for (int i = 1; i < activeDeck.cardsInTrebuchets.Count; i++)
                {
                    activeDeck.cardsInTrebuchets[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 0)
                        j++;
                }
            }
            else
            {
                int j = 1;
                activeDeck.cardsInTrebuchets[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                activeDeck.cardsInTrebuchets[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                for (int i = 2; i < activeDeck.cardsInTrebuchets.Count; i++)
                {
                    activeDeck.cardsInTrebuchets[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 1)
                        j++;
                }
            }
        }
        // For spy cards - PLAYER 1
        if (activePlayerNumber == (int)PlayerNumber.PLAYER1 && player2.getDeck().cardsInSwords.Count > 0)
        {
            Vector3 centerVector = areas.getSword2CenterVector() + new Vector3(0, -0.1456f, -0.1f);

            // For odd number of cards
            if (player2.getDeck().cardsInSwords.Count % 2 == 1)
            {
                int j = 1;
                player2.getDeck().cardsInSwords[0].transform.position = centerVector;

                for (int i = 1; i < player2.getDeck().cardsInSwords.Count; i++)
                {
                    player2.getDeck().cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 0)
                        j++;
                }
            }
            else
            {
                int j = 1;
                player2.getDeck().cardsInSwords[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                player2.getDeck().cardsInSwords[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                for (int i = 2; i < player2.getDeck().cardsInSwords.Count; i++)
                {
                    player2.getDeck().cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 1)
                        j++;
                }
            }
        }
        // For spy cards - PLAYER 2
        if (activePlayerNumber == (int)PlayerNumber.PLAYER2 && player1.getDeck().cardsInSwords.Count > 0)
        {
            Vector3 centerVector = areas.getSword2CenterVector() + new Vector3(0, -0.1456f, -0.1f);

            // For odd number of cards
            if (player1.getDeck().cardsInSwords.Count % 2 == 1)
            {
                int j = 1;
                player1.getDeck().cardsInSwords[0].transform.position = centerVector;

                for (int i = 1; i < player1.getDeck().cardsInSwords.Count; i++)
                {
                    player1.getDeck().cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 0)
                        j++;
                }
            }
            else
            {
                int j = 1;
                player1.getDeck().cardsInSwords[0].transform.position = centerVector + new Vector3(0.525f, 0, 0);
                player1.getDeck().cardsInSwords[1].transform.position = centerVector + new Vector3(-0.525f, 0, 0);

                for (int i = 2; i < player1.getDeck().cardsInSwords.Count; i++)
                {
                    player1.getDeck().cardsInSwords[i].transform.position = new Vector3(centerVector.x + j * 1.05f + Math.Sign(j) * 0.525f, centerVector.y, centerVector.z);

                    j *= -1;
                    if (i % 2 == 1)
                        j++;
                }
            }
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
    /// Defined game status
    /// </summary>
    private enum GameStatus { END, TOUR1, TOUR2, TOUR3 };
    
    /// <summary>
    /// Defined type of card
    /// </summary>
    private enum TypeOfCard { NORMAL, GOLD, SPY, MANEKIN, DESTROY, WEATHER , GOLD_SPY};

    /// <summary>
    /// Switch player - update active deck
    /// </summary>
    private void switchPlayer()
    {
        reorganizeGroup();
        state = (int)Status.BLOCKED;
        showActiveCard(false);
        
        StartCoroutine(Wait(0.75f));
    }

    /// <summary>
    /// End tour - show who won and start new game
    /// </summary>
    private void endTour()
    {
        // before
        endPanelObject.SetActive(true);
        giveUpButtonObject.SetActive(false);

        player1.isPlaying = true;
        player2.isPlaying = true;

        initializePlayersDecks();
        // TODO - Player who has won - started
        Debug.Log("WaitEndTour() has started");
        StartCoroutine(WaitEndTour(3f));
    }

    IEnumerator WaitEndTour(float duration)
    {
        yield return new WaitForSeconds(duration);

        Debug.Log("WaitEndTour() has ended");
        // after
        endPanelObject.SetActive(false);
        giveUpButtonObject.SetActive(true);
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);

        //giveUpButtonObject.SetActive(false);
        //playerDownNameTextObject.SetActive(false);
        // playerUpNameTextObject.SetActive(false);

        button.transform.position = new Vector3(0, 0, -1f);

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

        button.GetComponentInChildren<Text>().text = "Gracz " + activePlayerNumber + ",\nDotknij aby kontynuować";
        //playerNameText.text = "Gracz " + activePlayerNumber.ToString();

        Vector3 upPlayerNamePosition = playerUpNameTextObject.transform.position;
        playerUpNameTextObject.transform.position = playerDownNameTextObject.transform.position;
        playerDownNameTextObject.transform.position = upPlayerNamePosition;

        // changing position of players health diamonds
        Vector3 playerOneHealthOneVector = player1.getHealthDiamond(1).getPosition();
        Vector3 playerOneHealthSecondVector = player1.getHealthDiamond(2).getPosition();
        Vector3 playerTwoHealthOneVector = player2.getHealthDiamond(1).getPosition();
        Vector3 playerTwoHealthSecondVector = player2.getHealthDiamond(2).getPosition();
        player1.getHealthDiamond(1).setPosition(playerTwoHealthOneVector);
        player1.getHealthDiamond(2).setPosition(playerTwoHealthSecondVector);
        player2.getHealthDiamond(1).setPosition(playerOneHealthOneVector);
        player2.getHealthDiamond(2).setPosition(playerOneHealthSecondVector);

        // number of cards posiotion replacing
        Vector3 playerOneNumberOfCardsPosition = cardNumber1.transform.position;
        cardNumber1.transform.position = cardNumber2.transform.position;
        cardNumber2.transform.position = playerOneNumberOfCardsPosition;

        // fliping cards
        player1.getDeck().flipGroupCards();
        player2.getDeck().flipGroupCards();

        // score position replacing
        Vector3 tempVector = score1Text.transform.position;
        score1Text.transform.position = score2Text.transform.position;
        score2Text.transform.position = tempVector;

        tempVector = score3Text.transform.position;
        score3Text.transform.position = score6Text.transform.position;
        score6Text.transform.position = tempVector;

        tempVector = score4Text.transform.position;
        score4Text.transform.position = score7Text.transform.position;
        score7Text.transform.position = tempVector;

        tempVector = score5Text.transform.position;
        score5Text.transform.position = score8Text.transform.position;
        score8Text.transform.position = tempVector;

        reorganizeGroup();

        state = (int)Status.FREE;
    }

    private void showActiveCard(bool ifShow)
    {
        // TODO - usunąć operację modulo!
        if(ifShow)
            activeShowingCard.setBigFront(activeCard.getIndex() + 1); // +1 because 0 means null
        else
            activeShowingCard.setBigFront(0);
    }

    public void giveUp()
    {
        Debug.Log("Button position: " + button.transform.position);
        if (button.transform.position.y > 5f)
        {
            Debug.Log("Give up!");
            switchPlayer();

            if (activePlayerNumber == (int)PlayerNumber.PLAYER1)
                player1.isPlaying = false;
            else if (activePlayerNumber == (int)PlayerNumber.PLAYER2)
                player2.isPlaying = false;
        }
        else
            Debug.Log("Blocked!");
    }
}