using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private GameObject cardGameObject;
    private Card baseCard;

    // TODO - move initialization to Awake() or Start()
    public List<Card> cardsInDeck = new List<Card>(); // list of cards represented by each index;
    public List<Card> cardsInSwords = new List<Card>(); // list of cards in sword group
    public List<Card> cardsInBows = new List<Card>(); // list of cards in bow group
    public List<Card> cardsInTrebuchets = new List<Card>(); // list of cards in catapulte group
    public List<Card> cardsInDeaths = new List<Card>(); // burned or dead cards

    // TODO - dynamic layout cards system
    public float startX = -6.53f;
    public float startY = -6.27f;
    public float startZ = -0.1f;
    public float stepX = 1.05f;

    private static int FRONTS_NUMBER = 2;
    private static int MAX_NUMBER_OF_CARDS_IN_GROUP = 10;

    void Awake()
    {
        cardGameObject = GameObject.Find("Card");
        baseCard = cardGameObject.GetComponent<Card>();
    }

    /// <summary>
    /// method for buildiing new deck - adding cards to player's deck
    /// </summary>
    /// <param name="numberOfCards">how many cards have to be added to player's deck</param>
    public void buildDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            int j = i;

            if (i > FRONTS_NUMBER - 1)
                j = i % 2;

            Card clone = Instantiate(baseCard) as Card;
            clone.tag = "CloneCard";
            clone.setFront(j);
            clone.setPower(baseCard.getCardModel().getPower(j));
            // TODO - Improve !!!!!!!!!!!!!
            clone.setGroup(1);
            clone.setIndex(i);
            cardsInDeck.Add(clone);
        }
    }

    public IEnumerable<Card> getCards()
    {
        foreach(Card c in cardsInDeck)
        {
            yield return c;
        }
    }

    public IEnumerable<Card> getSwordCards()
    {
        foreach(Card c in cardsInSwords)
        {
            yield return c;
        }
    }

    public IEnumerable<Card> getBowCards()
    {
        foreach (Card c in cardsInBows)
        {
            yield return c;
        }
    }

    public IEnumerable<Card> getTrebuchetCards()
    {
        foreach (Card c in cardsInTrebuchets)
        {
            yield return c;
        }
    }

    public IEnumerable<Card> getDeathCards()
    {
        foreach (Card c in cardsInDeaths)
        {
            yield return c;
        }
    }

    /// <summary>
    /// adding card to sword group
    /// </summary>
    /// <param name="card">card we want to add to sword group</param>
    /// <returns>true if operation succeeded</returns>
    public bool addCardToSwords(Card card)
    {
        if(cardsInSwords.Count < MAX_NUMBER_OF_CARDS_IN_GROUP && cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.53f + cardsInSwords.Count * 1.05f, -0.19f, -0.1f);
            card.transform.position = newVector;

            cardsInSwords.Add(card);
            // TODO - should I remove deck from deck
            cardsInDeck.Remove(card);
            return true;
        }

        return false;
    }

    /// <summary>
    /// adding card to bow group
    /// </summary>
    /// <param name="card">card we want to add to bow group</param>
    /// <returns>true if operation succeeded</returns>
    public bool addCardToBows(Card card)
    {
        if (cardsInBows.Count < MAX_NUMBER_OF_CARDS_IN_GROUP && cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.53f + cardsInBows.Count * 1.05f, -1.91f, -0.1f);
            card.transform.position = newVector;

            cardsInBows.Add(card);
            // TODO - should I remove deck from deck
            cardsInDeck.Remove(card);
            return true;
        }

        return false;
    }

    /// <summary>
    /// adding card to trebuchets group and 
    /// </summary>
    /// <param name="card">card we want to add to trebuchet group</param>
    /// <returns>true if operation succeeded</returns>
    public bool addCardToTrebuchets(Card card)
    {
        if (cardsInTrebuchets.Count < MAX_NUMBER_OF_CARDS_IN_GROUP && cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.53f + cardsInTrebuchets.Count * 1.05f, -3.66f, -0.1f);
            card.transform.position = newVector;

            cardsInTrebuchets.Add(card);
            // TODO - should I remove deck from deck
            cardsInDeck.Remove(card);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Send cards from desk to death deck
    /// </summary>
    /// <returns>true if succeeded</returns>
    public bool sendCardsToDeathList()
    {
        bool ifSucceeded = false;

        for (int i = cardsInSwords.Count -1; i >=0; i--)
        {
            cardsInDeaths.Add(cardsInSwords[i]);
            ifSucceeded = cardsInSwords.Remove(cardsInSwords[i]);
        }
        for (int i = cardsInBows.Count - 1; i >= 0; i--)
        {
            cardsInDeaths.Add(cardsInBows[i]);
            ifSucceeded = cardsInBows.Remove(cardsInBows[i]);
        }
        for (int i = cardsInTrebuchets.Count - 1; i >= 0; i--)
        {
            cardsInDeaths.Add(cardsInTrebuchets[i]);
            ifSucceeded = cardsInTrebuchets.Remove(cardsInTrebuchets[i]);
        }

        return ifSucceeded;
    }

    /// <summary>
    /// disactivating cards in deck
    /// </summary>
    public void disactiveAllInDeck()
    {
        if (cardsInDeck.Count > 0)
        {
            foreach (Card c in getCards())
            {
                if (c.isActive())
                {
                    c.setActive(false);
                    c.transform.position += new Vector3(0, -0.15f, 0);
                }
            }
        }
    }

    /// <summary>
    /// Get sum of the card's powers from group or all caards
    /// </summary>
    /// <param name="group">number of group (0 - all, 1 - sword, 2 - bow, 3 - trebuchet)</param>
    /// <returns>sum of powers of cards in group(s)</returns>
    public int getPowerSum(int group)
    {
        int result = 0;

        if (group == 0 || group == 3)
        {
            foreach (Card card in getTrebuchetCards())
            {
                result += card.getPower();
            }
        }
        if (group == 0 || group == 2)
        { 
            foreach (Card card in getBowCards())
            {
                result += card.getPower();
            }
        }
        if(group == 0 || group == 1)
        { 
            foreach (Card card in getSwordCards())
            {
                result += card.getPower();
            }
        }

        return result;
    }

    /// <summary>
    /// Flip player cards
    /// </summary>
    public void flipGroupCards()
    {
        foreach(Card card in getSwordCards())
        {
            card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getBowCards())
        {
            card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getTrebuchetCards())
        {
            card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getDeathCards())
        {
            card.flip(false, true);

            float x = card.transform.position.x;
            float y = card.transform.position.y;
            float z = card.transform.position.z;

            card.transform.position = new Vector3(x, y * -1f, z);
        }
    }

    /// <summary>
    /// Defined type of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET };
}
