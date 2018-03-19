using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Card baseCard;
    public List<Card> cardsInDeck = new List<Card>(); // list of cards represented by each index;
    public List<Card> cardsInSwords = new List<Card>(); // list of cards in sword group
    public List<Card> cardsInBows = new List<Card>(); // list of cards in bow group
    public List<Card> cardsInCatapultes = new List<Card>(); // list of cards in catapulte group
    public GameObject cardGameObject;

    public float startX = -6.53f;
    public float startY = -6.27f;
    public float startZ = -0.1f;
    public float stepX = 1.05f;
    
    void Awake()
    {
        baseCard = cardGameObject.GetComponent<Card>();
    }

    public void buildDeck(int numberOfCards)
    {
        for (int i = 0; i <= numberOfCards; i++)
        {
            int j = i;

            if (i > 7)
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

    public IEnumerable<Card> getCatapultCards()
    {
        foreach (Card c in cardsInCatapultes)
        {
            yield return c;
        }
    }

    public void disactiveAllInDeck()
    {
        foreach (Card c in getCards())
        {
            //Debug.Log(c.getIndex() + " : " + c.isActive());

            if (c.isActive())
            {
                c.setActive(false);
                c.transform.position += new Vector3(0, -0.2f, 0);
            }
        }
    }

    /*
      if (areas.getDeckColliderBounds().Contains(this.transform.position))
        {
            Vector3 up = new Vector3(0, 0.2f, 0);
            // TODO - zabezpieczyć aby wysuwało się tylko raz
            this.transform.position += up;
        }
    */
}
