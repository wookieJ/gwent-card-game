using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Card baseCard;
    public List<Card> cardsInDeck = new List<Card>(); // list of cards represented by each index;
    public List<Card> cardsInSwords = new List<Card>(); // list of cards in sword group
    public List<Card> cardsInBows = new List<Card>(); // list of cards in bow group
    public List<Card> cardsInTrebuchets = new List<Card>(); // list of cards in catapulte group
    public GameObject cardGameObject;

    // TODO - dynamic layout cards system
    public float startX = -6.53f;
    public float startY = -6.27f;
    public float startZ = -0.1f;
    public float stepX = 1.05f;

    private static int FRONTS_NUMBER = 8;

    void Awake()
    {
        baseCard = cardGameObject.GetComponent<Card>();
    }

    /// <summary>
    /// method for buildiing new deck - adding cards to player's deck
    /// </summary>
    /// <param name="numberOfCards">how many cards have to be added to player's deck</param>
    public void buildDeck(int numberOfCards)
    {
        for (int i = 0; i <= numberOfCards; i++)
        {
            int j = i;

            if (i > FRONTS_NUMBER - 1)
                j = 1;

            Card clone = Instantiate(baseCard) as Card;
            clone.transform.position = new Vector3(startX + i * stepX, startY, startZ);
            clone.setFront(j);
            clone.setPower(baseCard.getCardModel().getPower(j));
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

    /// <summary>
    /// adding card to sword group
    /// </summary>
    /// <param name="card">card we want to add to sword group</param>
    /// <returns>true if operation succeeded</returns>
    public bool addCardToSwords(Card card)
    {
        if(cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.5f + cardsInSwords.Count * 1.05f, -0.97f, -0.1f);
            card.transform.position = newVector;

            cardsInSwords.Add(card);
            return true;
            // TODO - should I remove deck from deck
            //cardsInDeck.Remove(card);
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
        if (cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.5f + cardsInBows.Count * 1.05f, -2.66f, -0.1f);
            card.transform.position = newVector;

            cardsInBows.Add(card);
            return true;
            // TODO - should I remove deck from deck
            //cardsInDeck.Remove(card);
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
        if (cardsInDeck.Contains(card))
        {
            Vector3 newVector = new Vector3(-2.5f + cardsInTrebuchets.Count * 1.05f, -4.31f, -0.1f);
            card.transform.position = newVector;

            cardsInTrebuchets.Add(card);
            return true;
            // TODO - should I remove deck from deck
            //cardsInDeck.Remove(card);
        }

        return false;
    }

    /// <summary>
    /// disactivating cards in deck
    /// </summary>
    public void disactiveAllInDeck()
    {
        foreach (Card c in getCards())
        {
            if (c.isActive())
            {
                c.setActive(false);
                c.transform.position += new Vector3(0, -0.2f, 0);
            }
        }
    }

    public int getPowerSum()
    {
        int result = 0;

        foreach (Card card in getSwordCards())
        {
            result += card.getPower();
        }
        foreach (Card card in getBowCards())
        {
            result += card.getPower();
        }
        foreach (Card card in getTrebuchetCards())
        {
            result += card.getPower();
        }

        return result;
    }
}
