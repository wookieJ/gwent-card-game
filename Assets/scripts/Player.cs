using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int score;
    public int health;
    public Deck deck;
    public GameObject deckObject;

    void Awake()
    {
        deck = deckObject.GetComponent<Deck>();
        health = 2;
        score = 0;
    }

    /// <summary>
    /// Get player's name
    /// </summary>
    /// <returns>plyer's name</returns>
    public string getPlayerName()
    {
        return this.playerName;
    }

    /// <summary>
    /// Set player's name
    /// </summary>
    /// <param name="name">player's new name</param>
    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    /// <summary>
    /// Get player's score
    /// </summary>
    /// <returns>player's score</returns>
    public int getScore()
    {
        return this.score;
    }

    /// <summary>
    /// Set plyer's score
    /// </summary>
    /// <param name="score">player's new score</param>
    public void setScore(int score)
    {
        this.score = score;
    }

    /// <summary>
    /// Get player's health
    /// </summary>
    /// <returns></returns>
    public int getHealth()
    {
        return this.health;
    }

    /// <summary>
    /// Set player's health
    /// </summary>
    /// <param name="health">player's new health</param>
    public void setHealth(int health)
    {
        this.health = health;
    }

    /// <summary>
    /// Get player's deck
    /// </summary>
    /// <returns>player's deck</returns>
    public Deck getDeck()
    {
        return this.deck;
    }

    /// <summary>
    /// Set visibility of cards in deck
    /// </summary>
    /// <param name="visibility">true if we want to see cards and false to disapear them</param>
    public void setDeckVisibility(bool visibility)
    {
        float value = -10;

        if (visibility == true)
            value = -5.35f;
                
        foreach (Card card in deck.getCards())
        {
            card.transform.position = new Vector3(card.transform.position.x, value, card.transform.position.z);
        }
    }
}
