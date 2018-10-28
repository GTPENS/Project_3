using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    SCOUT = 1,
    FIGHTER = 2,
    BOMBER = 3,
    MERCHANT = 4
}

public class Enemy : MonoBehaviour {

    private string enemyName;
    private float speed;
    private float health;
    private float damage;
    private int enemyId;

    [SerializeField] private EnemyTypes enemyTypes;

    private GameObject explosion;

    private Rigidbody2D rb;

    private UI_Manager uiManager;

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
        uiManager = FindObjectOfType<UI_Manager>();
        enemyId = Convert.ToInt32(enemyTypes);
        enemyName = PersistentManager.getEnemyName(enemyId);
        speed = PersistentManager.getEnemySpeed(enemyId);
        health = PersistentManager.getEnemyHealth(enemyId);
        damage = PersistentManager.getEnemyDamage(enemyId);
        rb = GetComponent<Rigidbody2D>();
        explosion = Resources.Load("Prefabs/Explosion") as GameObject;
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
            uiManager.AddScore(PersistentManager.getEnemyScore(enemyId));
            Destroy(explosionGO, 1.1f);
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
