using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(Random.Range(1, 6));
    }

    private void SpawnEnemies(int amount)
    {
        // Spawn enemies
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
