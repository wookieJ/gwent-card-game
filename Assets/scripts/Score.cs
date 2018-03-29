using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    public int score;
    public int group;
    
    private GameObject deckObject;
    private Deck deck;
    private Text _MyText;

    void Awake()
    {
        string currentGameObjectName = this.gameObject.name;
        int currentPlayer = Int32.Parse(currentGameObjectName[currentGameObjectName.Length - 1].ToString());

        if (currentPlayer == 1 || currentPlayer == 6 || currentPlayer == 7 || currentPlayer == 8)
            currentPlayer = 1;
        else
            currentPlayer = 2;

        string playerDeckName = "Player" + currentPlayer + "Deck";
        deckObject = GameObject.Find(playerDeckName);
        deck = deckObject.GetComponent<Deck>();

        _MyText = GameObject.Find(currentGameObjectName).GetComponent<Text>();
    }

    void Update()
    {
        _MyText.text = deck.getPowerSum(group).ToString();
    }
}
