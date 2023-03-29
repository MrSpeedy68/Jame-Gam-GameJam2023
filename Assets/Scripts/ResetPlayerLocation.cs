using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerLocation : MonoBehaviour
{
    public Transform playerRespawnTransform;
    public AISpawner[] enemySpawners;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerRespawnTransform.position;
            RespawnEnemies();
        }
    }


    private void RespawnEnemies()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.SpawnEnemies(UnityEngine.Random.Range(1, spawner.AIamount));
        }
    }
}
