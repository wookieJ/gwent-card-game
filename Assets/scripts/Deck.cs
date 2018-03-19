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
            clone.setPower(i);
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
            cardsInTrebuchets.Add(card);
            return true;
            // TODO - should I remove deck from deck
            //cardsInDeck.Remove(card);
        }

        return false;
    }

    /// <summary>
    /// Resolve new posiotion of each card in pointet group
    /// </summary>
    /// <param name="group">group of cards that we want to reorganize. Int value</param>
    public void reorganizeGroup(int group)
    {
        if(group == (int)CardGroup.DECK)
        {

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

    /// <summary>
    /// Defined typed of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET };
}
