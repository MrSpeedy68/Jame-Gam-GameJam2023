using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float defence = 1.0f;
    [SerializeField] private float attackSpeed = 1.0f;

    public BarSlider barSlider;
    public Card[] cardDeck = new Card[4];
    public int score = 0;
    
    [Header("Multipliers")]
    public float damageMultiplier = 1.0f;
    public float speedMultiplier = 1.0f;
    public float defenceMultiplier = 1.0f;
    public float attackSpeedMultiplier = 1.0f;

    public void IncreaseHealth(float healthIncrease)
    {
        health += healthIncrease;
        if (health < 100f)
        {
            health = 100f;
        }
    }
    
    public void TakeDamage(float damageTaken)
    {
        float randVal = Random.value;
        if (randVal < GetDefence())
        {
            Debug.Log("Player blocked the attack");
            DamagePopup.Create(transform.position + Vector3.up * 2, "Blocked");
        }
        else
        {
            health -= damageTaken;
            barSlider.SetFill((int)health);
            int damage = (int)damageTaken;
            DamagePopup.Create(transform.position + Vector3.up * 2, damage.ToString());
            if (health <= 0)
            {
                Die();
            }
        }
    }
    
    private void Die()
    {
        Debug.Log("Player died");
        //Destroy(gameObject);
    }

    public void AddScore(int point)
    {
        score += point;
    }

    public void RemoveScore(int point)
    {
        score -= point;
    }

    public int GetScore()
    {
        return score;
    }

    // Return Player stats
    public float GetDamage()
    {
        return Random.Range(damage-2,damage) * damageMultiplier;
    }
    
    public float GetSpeed()
    {
        return speed * speedMultiplier;
    }
    
    public float GetDefence()
    {
        return (defence * defenceMultiplier) / 100;
    }
    
    public float GetAttackSpeed()
    {
        return attackSpeed * attackSpeedMultiplier;
    }

    public float GetHealth()
    {
        return health;
    }

    public bool IsDeckFull()
    {
        int cardNo = 0;
        for(int i = 0; i < 4; i++)
        {
            if(cardDeck[i] != null)
            {
                cardNo++;
            }
        }
        return cardNo == 4;
    }
}
