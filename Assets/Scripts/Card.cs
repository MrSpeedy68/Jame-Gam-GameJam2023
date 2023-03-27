using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum TYPE
    {
        ABILITY,
        BUFF
    }
    
    public enum ABILITY
    {
        NONE,
        FIREBALL,
        ELECTRICBOLT,
        NUKE,
        FLAMEWALL,
        HOMINGSPELL
    }
    
    public enum BUFF
    {
        NONE,
        HEALTH,
        DAMAGE,
        SPEED,
        DEFENCE,
        ATTACKSPEED
    }
    
    [Header("Card Type")]
    public TYPE type;
    public ABILITY ability;
    public BUFF buff;
    public float coolDownTime;
    
    public Sprite cardSprite;
    public Color color = Color.green;
    public String cardName;

    public int cardPrice;

    [HideInInspector]
    public bool isActive = false;

    private float _currentCoolDown;
    
    [Header("Buff Stats")]
    public float buffDuration;
    public float damageBuff;
    public float speedBuff;
    public float defenceBuff;
    public float attackSpeedBuff;
    public float healthBuff;

    //[Header("Ability Stats")]
    
    private void Awake()
    {
        isActive = false;
        _currentCoolDown = coolDownTime;
    }

    private void Update()
    {
        if (isActive)
        {
            Debug.Log("Card is active " + cardName);
            color = Color.red;
            _currentCoolDown -= Time.deltaTime;
        }

        if (IsUsable())
        {
            Debug.Log("Card is usable " + cardName);
            _currentCoolDown = coolDownTime;
            isActive = false;
            color = Color.green;
        }
    }

    public bool IsUsable()
    {
        return _currentCoolDown <= 0;
    }
}
