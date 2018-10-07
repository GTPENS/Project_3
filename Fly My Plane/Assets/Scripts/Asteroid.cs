using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidTypes
{
    NORMAL,
    SMALL,
    ARMORED,
    SPLITTING,
    FAST
}

public enum AsteroidSpeed
{
    SLOW,
    FAST
}

public class Asteroid : MonoBehaviour {

    private float speed;
    private float health;
    private float damage;

    private GameObject explosion;

    private Rigidbody2D rb;

    public AsteroidTypes asteroidTypes;
    public AsteroidSpeed asteroidSpeed;
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
        rb = GetComponent<Rigidbody2D>();
        explosion = Resources.Load("Prefabs/Explosion") as GameObject;

        switch (asteroidSpeed)
        {
            case AsteroidSpeed.FAST:
                Speed = 3f;
                break;
            case AsteroidSpeed.SLOW:
                Speed = 2f;
                break;
        }

        switch (asteroidTypes)
        {
            case AsteroidTypes.NORMAL:
                Health = 3;
                Damage = 4;
                break;
            case AsteroidTypes.SMALL:
                Health = 1;
                Damage = 2;
                break;
            case AsteroidTypes.FAST:
                Health = 1;
                Damage = 2;
                break;
            case AsteroidTypes.ARMORED:
                Health = 6;
                Damage = 4;
                break;
            case AsteroidTypes.SPLITTING:
                Health = 3;
                Damage = 4;
                break;
        }
    }
	
	private void Update () {
        Debug.Log("Asteroid Type = " + asteroidTypes + " Health = " + Health);
        
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
            Destroy(explosionGO, 1.7f);
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
            Destroy(collision.gameObject);
        }
    }
}
