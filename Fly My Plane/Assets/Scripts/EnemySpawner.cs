using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float spawnRate = 25f;
    private int spawnChance;

    public GameObject[] enemy;

    // Use this for initialization
    private void Start()
    {
        Invoke("SpawnEnemy", spawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 60f);
    }

    public void SpawnEnemy()
    {
        spawnChance = Random.Range(0, 100);
        Debug.Log("chance = " + spawnChance);
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.2f, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.8f, 1));
        if(spawnChance > 75)
            Instantiate(enemy[0], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        else if(spawnChance > 50)
            Instantiate(enemy[1], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        else if(spawnChance > 25)
            Instantiate(enemy[2], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        else if(spawnChance > 10)
            Instantiate(enemy[3], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        NextEnemySpawn();
    }

    private void NextEnemySpawn()
    {
        float spawnInSeconds;
        if (spawnRate > 1f)
            spawnInSeconds = Random.Range(1f, spawnRate);
        else
            spawnInSeconds = 1f;
        Invoke("SpawnEnemy", spawnInSeconds);
    }

    private void IncreaseSpawnRate()
    {
        if (spawnRate > 1f)
            spawnRate--;
        else
            CancelInvoke("IncreaseSpawnRate");
    }
}
