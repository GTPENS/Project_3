using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] private float spawnRate = 7.5f;

    public GameObject[] asteroid;
    
	// Use this for initialization
	private void Start () {
        Invoke("SpawnAsteroid", spawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}
	
    public void SpawnAsteroid()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.2f, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.8f, 1));
        Instantiate(asteroid[Random.Range(0, 4)], new Vector2(Random.Range(min.x, max.x), max.y), Quaternion.identity);
        NextAsteroidSpawn();
    }

    private void NextAsteroidSpawn()
    {
        float spawnInSeconds;
        if (spawnRate > 1f)
            spawnInSeconds = Random.Range(1f, spawnRate);
        else
            spawnInSeconds = 1f;
        Invoke("SpawnAsteroid", spawnInSeconds);
    }

    private void IncreaseSpawnRate()
    {
        if (spawnRate > 1f)
            spawnRate--;
        else
            CancelInvoke("IncreaseSpawnRate");
    }
}
