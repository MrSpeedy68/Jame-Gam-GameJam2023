using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    private CardManager _cardManager;
    [SerializeField] private GameObject[] _cardSlots;

    public TMP_Text scoretext;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _cardManager = player.GetComponent<CardManager>();
    }

    private void Update()
    {
        if (_cardManager)
        {
            UpdateCards();
        }
        if (player)
        {
            UpdateScore();
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
        scoretext.text = "Score: " + player.GetScore();
    }
}
