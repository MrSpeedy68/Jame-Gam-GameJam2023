using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        DamagePopup.Create(transform.position + Vector3.up * 2, (int)damageTaken);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
