using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private Player _player;
    public Card[] cards;
    private ParticleSystem[] _particleSystems;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        cards = _player.cardDeck;

        if (cards.Length > 0)
        {
            foreach (var card in cards)
            {
                card.color = Color.white;
            }
        }
    }

    private void Update()
    {
        //cards = _player.cardDeck;
        
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
        if (cards.Length > 0 && cards[index] != null)
        {
            if (cards[index].IsUsable())
            {
                if(cards[index].type == Card.TYPE.ABILITY)
                    ActivateAbility(index);
                else if (cards[index].type == Card.TYPE.BUFF)
                    ActivateBuff(index);
            }
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

            StartCoroutine("CardBuffCooldown", index);
            StartCoroutine("CardCoolDown", index);
            
            CardParticles(index);
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
        }
    }

    private void CardParticles(int index)
    {
        Card.BUFF buff = cards[index].buff;
        switch (buff)
        {
            case Card.BUFF.DAMAGE:
                var particle = Instantiate(GameAssets.i.DamageParticle, _player.transform.position, Quaternion.identity);
                particle.transform.parent = _player.transform;
                Destroy(particle, 20f);
                break;
            case Card.BUFF.SPEED:
                var particle1 = Instantiate(GameAssets.i.SpeedParticle, _player.transform.position, Quaternion.identity);
                particle1.transform.parent = _player.transform;
                Destroy(particle1, 20f);
                break;
            case Card.BUFF.DEFENCE:
                var particle2 = Instantiate(GameAssets.i.ShieldParticle, _player.transform.position, Quaternion.identity);
                particle2.transform.parent = _player.transform;
                Destroy(particle2, 20f);
                break;
            case Card.BUFF.ATTACKSPEED:
                break;
            case Card.BUFF.HEALTH:
                var particle3 = Instantiate(GameAssets.i.HealParticle, _player.transform.position, Quaternion.identity);
                particle3.transform.parent = _player.transform;
                Destroy(particle3, 20f);
                break;
            default:
                break;
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

    public void UpdateCards()
    {
        cards = _player.cardDeck;
    }
    
}
