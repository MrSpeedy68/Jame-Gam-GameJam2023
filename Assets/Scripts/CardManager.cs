using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private Player _player;
    public Card[] cards;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        cards = _player.cardDeck;

        foreach (var card in cards)
        {
            card.color = Color.white;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Card1"))
        {
            UseCard(0);
        }
        
        if (Input.GetButtonDown("Card2"))
        {
            UseCard(1);
        }
        
        if (Input.GetButtonDown("Card3"))
        {
            UseCard(2);
        }
        
        if (Input.GetButtonDown("Card4"))
        {
            UseCard(3);
        }
    }

    private void UseCard(int index)
    {
        if (cards[index] != null && cards[index].IsUsable())
        {
            if(cards[index].type == Card.TYPE.ABILITY)
                ActivateAbility(index);
            else if (cards[index].type == Card.TYPE.BUFF)
                ActivateBuff(index);
        }
    }

    private void ActivateBuff(int index)
    {
        cards[index].isActive = true;
        
        if (!(cards[index].buff == Card.BUFF.HEALTH))
        {
            _player.damageMultiplier += cards[index].damageBuff;
            _player.speedMultiplier += cards[index].speedBuff;
            _player.defenceMultiplier += cards[index].defenceBuff;
            _player.attackSpeedMultiplier += cards[index].attackSpeedBuff;
            
            Debug.Log("Damage: " + _player.damageMultiplier);
            Debug.Log("Speed: " + _player.speedMultiplier);
            Debug.Log("Defence: " + _player.defenceMultiplier);
            Debug.Log("Attack Speed: " + _player.attackSpeedMultiplier);

            StartCoroutine("CardBuffCooldown", index);
            StartCoroutine("CardCoolDown", index);
        }
        else
        {
            _player.IncreaseHealth(cards[index].healthBuff);
            StartCoroutine("CardCoolDown", index);
        }
    }

    private void DeactivateBuff(int index)
    {
        if (!(cards[index].buff == Card.BUFF.HEALTH))
        {
            _player.damageMultiplier -= cards[index].damageBuff;
            _player.speedMultiplier -= cards[index].speedBuff;
            _player.defenceMultiplier -= cards[index].defenceBuff;
            _player.attackSpeedMultiplier -= cards[index].attackSpeedBuff;
            
            Debug.Log("Damage After Deactivate: " + _player.damageMultiplier);
            Debug.Log("Speed After Deactivate: " + _player.speedMultiplier);
            Debug.Log("Defence After Deactivate: " + _player.defenceMultiplier);
            Debug.Log("Attack Speed After Deactivate: " + _player.attackSpeedMultiplier);
        }
    }

    private IEnumerator CardBuffCooldown(int index)
    {
        float time = cards[index].buffDuration;
        
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        DeactivateBuff(index);
    }

    private IEnumerator CardCoolDown(int index)
    {
        cards[index].color = Color.red;
        
        float time = cards[index].coolDownTime;
        
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        cards[index].color = Color.white;
    }
    
    private void ActivateAbility(int index)
    {
        cards[index].isActive = true;
        
        switch (cards[index].ability)
        {
            case Card.ABILITY.FIREBALL:
                break;
            case Card.ABILITY.ELECTRICBOLT:
                break;
            case Card.ABILITY.NUKE:
                break;
            case Card.ABILITY.FLAMEWALL:
                break;
            case Card.ABILITY.HOMINGSPELL:
                break;
            default:
                break;
        }
    }
    
}
