using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {
    private Deck deck;
    private int score;

    public Text _MyText;
    public GameObject deckObject;

    void Awake()
    {
        _MyText = GetComponent<Text>();
        deck = deckObject.GetComponent<Deck>();
    }

    void Update()
    {
        _MyText.text = deck.getPowerSum().ToString();
    }
}
