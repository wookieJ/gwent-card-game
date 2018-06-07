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
    public List<Card> cardsInSpecial = new List<Card>(); // special cards

    // TODO - dynamic layout cards system
    public float startX = -6.53f;
    public float startY = -6.27f;
    public float startZ = -0.1f;
    public float stepX = 1.05f;

    private static int FRONTS_NUMBER = 35;
    // TODO - remove max amount of cards in each range group
    private static int MAX_NUMBER_OF_CARDS_IN_GROUP = 7;
    private static int SWORD_GROUP_AMOUNT = 7;
    private static int SWORD_GOLD_GROUP_AMOUNT = 5;
    private static int BOW_GROUP_AMOUNT = 5;
    private static int BOW_GOLD_GROUP_AMOUNT = 1;
    private static int TREBUCHET_GROUP_AMOUNT = 3;
    private static int TREBUCHET_GOLD_GROUP_AMOUNT = 0;

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
        List<int> uniqueValues = new List<int>();

        for (int cardIndex = 0; cardIndex < numberOfCards; cardIndex++)
        {
            // For unique cards set
            int cardId;
            do
            {
                cardId = Random.Range(0, FRONTS_NUMBER);
            } while (uniqueValues.Contains(cardId));
            uniqueValues.Add(cardId);
            
            Card clone = Instantiate(baseCard) as Card;
            clone.tag = "CloneCard";
            clone.setFront(cardId);
            clone.setPower(baseCard.getCardModel().getPower(cardId));
            clone.setIndex(cardId);
            clone.setIsSpecial(clone.getCardModel().getIsSpecial(cardId));
            cardsInDeck.Add(clone);
        }
    }

    /// <summary>
    /// Adding 2 random cards to player's deck
    /// </summary>
    /// <param name="whichPlayer"></param>
    public void addTwoRandomCards()
    {
        // TODO - Cards aren't unique!!!!!!!!!!!!!!!!!!!!!
        for (int i = 0; i < 2; i++)
        {
            int cardId = Random.Range(0, FRONTS_NUMBER);
            Card clone = Instantiate(baseCard) as Card;
            clone.tag = "CloneCard";
            clone.setFront(cardId);
            clone.setPower(baseCard.getCardModel().getPower(cardId));
            clone.setIndex(cardId);
            clone.setIsSpecial(clone.getCardModel().getIsSpecial(cardId));
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

    public IEnumerable<Card> getSpecialCards()
    {
        foreach (Card c in cardsInSpecial)
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
    /// adding card from swords to deck
    /// </summary>
    /// <param name="card">card we want to move</param>
    /// <returns>true if operation succeeded</returns>
    public bool moveCardToDeckFromSwords(Card card)
    {
        cardsInDeck.Add(card);
        return cardsInSwords.Remove(card);
    }

    /// <summary>
    /// adding card from bows to deck
    /// </summary>
    /// <param name="card">card we want to move</param>
    /// <returns>true if operation succeeded</returns>
    public bool moveCardToDeckFromBows(Card card)
    {
        cardsInDeck.Add(card);
        return cardsInBows.Remove(card);
    }

    /// <summary>
    /// adding card from trebuchets to deck
    /// </summary>
    /// <param name="card">card we want to move</param>
    /// <returns>true if operation succeeded</returns>
    public bool moveCardToDeckFromTrebuchets(Card card)
    {
        cardsInDeck.Add(card);
        return cardsInTrebuchets.Remove(card);
    }

    /// <summary>
    /// Adding spy card to opponent sword deck
    /// </summary>
    /// <param name="card">spy card we want to add</param>
    public void addSpy(Card card)
    {
        Vector3 newVector = new Vector3(-2.53f + cardsInSwords.Count * 1.05f, 1.66495f, -0.1f);
        card.transform.position = newVector;

        cardsInSwords.Add(card);
    }

    /// <summary>
    /// Adding weather and destroy cards to special box
    /// </summary>
    /// <param name="card">Crd we wnt to add</param>
    public void addToSpecial(Card card)
    {        
        cardsInSpecial.Add(card);
        cardsInDeck.Remove(card);
    }

    /// <summary>
    /// Deleting weather from special box
    /// </summary>
    public void deleteFromSpecial()
    {
        foreach(Card c in getSpecialCards())
        {
            if(c.isSpecial == 5)
                sendCardToDeathList(c);
        }
        cardsInSpecial.Clear();
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
    /// Sending card to death list
    /// </summary>
    /// <param name="card">card we want to send</param>
    /// <returns>true if succeeded</returns>
    public bool sendCardToDeathList(Card card)
    {
        bool ifSucceeded = false;

        cardsInDeaths.Add(card);
        if(card.getGroup() == (int)CardGroup.SWORD)
            ifSucceeded = cardsInSwords.Remove(card);
        if (card.getGroup() == (int)CardGroup.BOW)
            ifSucceeded = cardsInBows.Remove(card);
        if (card.getGroup() == (int)CardGroup.TREBUCHET)
            ifSucceeded = cardsInTrebuchets.Remove(card);

        Vector3 player1DeathAreaVector = new Vector3(8.51f, -4.6f, -0.1f);
        card.transform.position = player1DeathAreaVector;
        
        float x = card.transform.position.x;
        float y = card.transform.position.y;
        float z = card.transform.position.z;

        card.transform.position = new Vector3(x, y * -1f, z);
        
        

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
    /// Removing card with highest power value
    /// </summary>
    public void removeMaxPowerCard()
    {
        int maxPower = 0;
        Card maxCard = null;
        foreach (Card card in cardsInSwords)
        {
            // checing if card is not a gold one and has no weather effect
            if ((card.weatherEffect == false && card.getPower() > maxPower && card.getIsSpecial() != 1) || (card.weatherEffect == true && 1 > maxPower && card.getIsSpecial() != 1))
            {
                maxPower = card.getPower();
                maxCard = card;
            }
        }
        foreach (Card card in cardsInBows)
        {
            // checing if card is not a gold one
            if ((card.weatherEffect == false && card.getPower() > maxPower && card.getIsSpecial() != 1) || (card.weatherEffect == true && 1 > maxPower && card.getIsSpecial() != 1))
            {
                maxPower = card.getPower();
                maxCard = card;
            }
        }
        foreach (Card card in cardsInTrebuchets)
        {
            // checing if card is not a gold one
            if ((card.weatherEffect == false && card.getPower() > maxPower && card.getIsSpecial() != 1) || (card.weatherEffect == true && 1 > maxPower && card.getIsSpecial() != 1))
            {
                maxPower = card.getPower();
                maxCard = card;
            }
        }
        if (maxCard != null)
            sendCardToDeathList(maxCard);
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
                if (card.weatherEffect == false)
                    result += card.getPower();
                else
                    result++;
            }
        }
        if (group == 0 || group == 2)
        { 
            foreach (Card card in getBowCards())
            {
                if (card.weatherEffect == false)
                    result += card.getPower();
                else
                    result++;
            }
        }
        if(group == 0 || group == 1)
        { 
            foreach (Card card in getSwordCards())
            {
                if (card.weatherEffect == false)
                    result += card.getPower();
                else
                    result++;
            }
        }

        return result;
    }

    /// <summary>
    /// Tagging cards touched by weather card
    /// </summary>
    /// <param name="cardGroup">range of card</param>
    public void applyWeatherEffect(int cardGroup)
    {
        switch(cardGroup)
        {
            case 1:
                foreach (Card card in getSwordCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = true;
                }
                break;
            case 2:
                foreach (Card card in getBowCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = true;
                }
                break;
            case 3:
                foreach (Card card in getTrebuchetCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = true;
                }
                break;
            case 4:
                foreach (Card card in getSwordCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = false;
                }
                foreach (Card card in getBowCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = false;
                }
                foreach (Card card in getTrebuchetCards())
                {
                    if (card.getIsSpecial() == 0)
                        card.weatherEffect = false;
                }
                break;
        }
    }

    /// <summary>
    /// Flip player cards
    /// </summary>
    public void flipGroupCards()
    {
        foreach(Card card in getSwordCards())
        {
            //card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getBowCards())
        {
            //card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getTrebuchetCards())
        {
            //card.flip(true, true);
            card.mirrorTransform();
        }
        foreach (Card card in getDeathCards())
        {
            //card.flip(false, true);

            float x = card.transform.position.x;
            float y = card.transform.position.y;
            float z = card.transform.position.z;

            card.transform.position = new Vector3(x, y * -1f, z);
        }
        foreach(Card card in getSpecialCards())
        {
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
