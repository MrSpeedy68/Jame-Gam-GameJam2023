using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private CardManager _cardManager;
    [SerializeField] private GameObject[] _cardSlots;
    
    // Start is called before the first frame update
    void Start()
    {
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
            _cardSlots[i].GetComponent<Image>().sprite = _cardManager.cards[i].cardImage.sprite;
            _cardSlots[i].GetComponent<Image>().color = _cardManager.cards[i].color;
        }
    }
}
