using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidTypes
{
    NORMAL = 1,
    SMALL = 2,
    ARMORED = 3,
    SPLITTING = 4,
    FAST = 5
}

public class Asteroid : MonoBehaviour {

    private string asteroidName;
    private float speed;
    private float health;
    private float damage;
    private int asteroidId;

    [SerializeField] private AsteroidTypes asteroidTypes;

    private GameObject explosion;
    private GameObject splittingObject;

    private Rigidbody2D rb;

    private UI_Manager uiManager;
    
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    private void Start () {
        uiManager = FindObjectOfType<UI_Manager>();
        asteroidId = Convert.ToInt32(asteroidTypes);
        asteroidName = PersistentManager.getAsteroidName(asteroidId);
        speed = PersistentManager.getAsteroidSpeed(asteroidId);
        health = PersistentManager.getAsteroidHealth(asteroidId);
        damage = PersistentManager.getAsteroidDamage(asteroidId);
        rb = GetComponent<Rigidbody2D>();
        explosion = Resources.Load("Prefabs/Exploding_Asteroid") as GameObject;
        splittingObject = Resources.Load("Prefabs/Asteroid_Small") as GameObject;
    }
	
	private void Update () {
        CheckHealth();
        AsteroidMove();
	}

    private void AsteroidMove()
    {
        rb.velocity = new Vector2(0, -Speed);
    }
    
    private void CheckHealth()
    {
        if (Health <= 0)
        {
            GameObject explosionGO = Instantiate(explosion, transform.position, Quaternion.identity);
            explosionGO.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            uiManager.AddScore(PersistentManager.getAsteroidScore(asteroidId));
            Destroy(explosionGO, 1.1f);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Player player;
            player = FindObjectOfType<Player>();
            Health -= player.Damage;
            AudioManager.instance.PlayOtherSFX(1);
            if (asteroidName == "Splitting")
            {
                GameObject splitting1 = Instantiate(splittingObject, transform.position, Quaternion.identity);
                GameObject splitting2 = Instantiate(splittingObject, transform.position, Quaternion.identity);
                splitting1.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
                splitting2.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            }
            Destroy(collision.gameObject);
        }
    }
}
