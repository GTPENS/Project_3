using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float offsetDamage;
    
    private Asteroid asteroid;
    private Enemy enemy;
    private UI_Manager uiManager;

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

    public float PlayerSpeed
    {
        get
        {
            return playerSpeed;
        }

        set
        {
            playerSpeed = value;
        }
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
    }

    private void Update ()
    {
        
        if (Input.acceleration.x != 0)
        {
            PlayerSpeed = Input.acceleration.x;
            Move(PlayerSpeed / 5);
        }
        
        if (Mathf.Abs(transform.position.x) >= 3f)
        {
            uiManager.ReduceHealth(offsetDamage * Time.deltaTime);
        }

        //development testing
        if (Input.GetKey(KeyCode.D))
        {
            Move(PlayerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-PlayerSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] enemies;
            GameObject[] asteroids;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            Debug.Log(enemies.Length);
            Debug.Log(asteroids.Length);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().Health = 0;
            }
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].GetComponent<Asteroid>().Health = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(GameManager.instance.TimeDilation());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(GameManager.instance.Invulnerable());
        }
    }

    public void Move(float speed)
    {
        if (speed > 0.5f)
        {
            speed = 0.5f;
        }
        
        if (speed < -0.5f)
        {
            speed = -0.5f;
        }
        
        transform.Translate(speed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            asteroid = FindObjectOfType<Asteroid>();
            uiManager.ReduceHealth(asteroid.Damage);
            
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = FindObjectOfType<Enemy>();
            uiManager.ReduceHealth(enemy.Damage);

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            enemy = FindObjectOfType<Enemy>();
            uiManager.ReduceHealth(enemy.Damage);

            Destroy(collision.gameObject);
        }
    }
}
