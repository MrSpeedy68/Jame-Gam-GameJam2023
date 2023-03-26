using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float damage = 5.0f;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float defence = 1.0f;
    [SerializeField] private float attackSpeed = 1.0f;
    
    public Card[] cardDeck = new Card[4];
    public int score = 0;
    
    [Header("Multipliers")]
    public float damageMultiplier = 1.0f;
    public float speedMultiplier = 1.0f;
    public float defenceMultiplier = 1.0f;
    public float attackSpeedMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
        health -= damageTaken;
        if (health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Player died");
        //Destroy(gameObject);
    }

    // Return Player stats
    public float GetDamage()
    {
        return damage * damageMultiplier;
    }
    
    public float GetSpeed()
    {
        return speed * speedMultiplier;
    }
    
    public float GetDefence()
    {
        return defence * defenceMultiplier;
    }
    
    public float GetAttackSpeed()
    {
        return attackSpeed * attackSpeedMultiplier;
    }
}
