using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Card baseCard;
    public List<Card> cardsInDeck = new List<Card>(); // list of cards represented by each index;
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
}
