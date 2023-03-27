using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    public float attackInterval = 5f;
    public float maxSightDistance = 10f;
    public int rayAmount = 7;
    
    private float _attackTimer = 0f;
    private bool _bAttacking = false;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private bool _bisDead = false;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
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

        for (int i = 0; i < rayAmount; i++)
        {
            // calculate the angle for this ray based on the number of rays
            float angle = i * Mathf.PI * 2f / rayAmount;
            // convert angle to a direction vector
            Vector3 direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            // shoot a ray in that direction
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, maxSightDistance))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                if (hit.collider.CompareTag("Player"))
                {
                    _player = hit.collider.GetComponent<Player>();

                    if (!_bisDead)
                    {
                        _navMeshAgent.SetDestination(_player.transform.position);
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * maxSightDistance, Color.green);
            }
        }
       
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        int damage = (int)damageTaken;
        DamagePopup.Create(transform.position + Vector3.up * 2, damage.ToString());
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
        _bisDead = true;
        
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
