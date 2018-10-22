using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    SCOUT,
    FIGHTER,
    BOMBER,
    MERCHANT
}

public enum EnemySpeed
{
    SLOW,
    NORMAL
}

public class Enemy : MonoBehaviour {

    private float speed;
    private float health;
    private float damage;

    private GameObject explosion;

    private Rigidbody2D rb;

    public EnemyTypes enemyTypes;
    public EnemySpeed enemySpeed;

    public float Speed {
        get {
            return speed;
        }

        set {
            speed = value;
        }
    }

    public float Health {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public float Damage {
        get {
            return damage;
        }

        set {
            damage = value;
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        explosion = Resources.Load("Prefabs/Explosion") as GameObject;

        switch (enemySpeed)
        {
            case EnemySpeed.NORMAL:
                Speed = 4f;
                break;
            case EnemySpeed.SLOW:
                Speed = 3f;
                break;
        }

        switch (enemyTypes)
        {
            case EnemyTypes.SCOUT:
                Health = 5;
                Damage = 3;
                break;
            case EnemyTypes.BOMBER:
                Health = 15;
                Damage = 10;
                break;
            case EnemyTypes.FIGHTER:
                Health = 10;
                Damage = 5;
                break;
            case EnemyTypes.MERCHANT:
                Health = 15;
                Damage = 5;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        EnemyMove();
        CheckHealth();
	}

    private void EnemyMove()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Player player;
            player = FindObjectOfType<Player>();
            Health -= player.Damage;
            Destroy(collision.gameObject);
        }
    }
}
