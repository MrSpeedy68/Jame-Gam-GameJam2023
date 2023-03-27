using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    public float attackInterval = 5f;
    
    private float _attackTimer = 0f;
    private bool _bAttacking = false;
    private Player _player;

    private void Start()
    {
        _attackTimer = attackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
        
        if (_bAttacking)
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0f)
            {
                DealDamage();
                _bAttacking = false;
                _attackTimer = attackInterval;
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        DamagePopup.Create(transform.position + Vector3.up * 2, (int)damageTaken);
    }

    public void DealDamage()
    {
        if (_player)
        {
            _player.TakeDamage(Random.Range(damage-2,damage));
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            DealDamage();
            _bAttacking = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _bAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _bAttacking = false;
        }
    }

}
