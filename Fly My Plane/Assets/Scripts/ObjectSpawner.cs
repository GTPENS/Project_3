using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] private float spawnRate = 7.5f;

    public GameObject[] asteroid;
    
	// Use this for initialization
	void Start () {
        Invoke("SpawnAsteroid", spawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}
	
    void SpawnAsteroid()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Instantiate(asteroid[Random.Range(0, 3)], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        NextAsteroidSpawn();
    }

    void NextAsteroidSpawn()
    {
        float spawnInSeconds;
        if (spawnRate > 1f)
            spawnInSeconds = Random.Range(1f, spawnRate);
        else
            spawnInSeconds = 1f;
        Invoke("SpawnAsteroid", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (spawnRate > 1f)
            spawnRate--;
        else
            CancelInvoke("IncreaseSpawnRate");
    }
}
