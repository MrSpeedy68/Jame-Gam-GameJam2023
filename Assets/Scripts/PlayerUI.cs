using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private int currentScore = 0;
    private CardManager _cardManager;
    [SerializeField] private GameObject[] _cardSlots;

    public Text scoretext;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        _cardManager = GameObject.FindWithTag("Player").GetComponent<CardManager>();
    }

    private void Update()
    {
        if (_cardManager)
        {
            UpdateCards();
        }
    }

    private void UpdateCards()
    {
        for (int i = 0; i < +_cardManager.cards.Length; i++)
        {
            _cardSlots[i].GetComponent<Image>().sprite = _cardManager.cards[i].cardSprite;
            _cardSlots[i].GetComponent<Image>().color = _cardManager.cards[i].color;
        }
    }

    private void UpdateScore()
    {
        scoretext.text = "Score: " + currentScore;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScore();
    }
}
