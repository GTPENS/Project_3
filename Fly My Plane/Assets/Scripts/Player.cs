using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float maxHealth;
    private float health;
    private float damage;
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

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public int GetPrefsHealth()
    {
        return PlayerPrefs.GetInt("Player Health");
    }

    public int GetPrefsDamage()
    {
        return PlayerPrefs.GetInt("Player Damage");
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        //dont forget to delete this line on release
        PlayerPrefs.DeleteAll();
        //-----------------------------------------
        PlayerPrefs.GetInt("Player Health", 0);
        PlayerPrefs.GetInt("Player Damage", 0);
        Health = PersistentManager.getDefensiveLevelUpgrade(GetPrefsHealth());
        Damage = PersistentManager.getOffensiveLevelUpgrade(GetPrefsDamage());
        MaxHealth = PersistentManager.getDefensiveLevelUpgrade(GetPrefsHealth());
    }

    private void Update ()
    {
        Debug.Log("prefs player health = " + GetPrefsHealth());
        Debug.Log("prefs player dmg = " + GetPrefsDamage());

        Debug.Log("MAX health = " + MaxHealth);
        Debug.Log("health = " + Health);
        Debug.Log("damage = " + Damage);
        
        MaxHealth = PersistentManager.getDefensiveLevelUpgrade(GetPrefsHealth());
        Damage = PersistentManager.getOffensiveLevelUpgrade(GetPrefsDamage());
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DefensiveUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OffensiveUpgrade();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = FindObjectOfType<Enemy>();
            uiManager.ReduceHealth(enemy.Damage);

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy Bullet")
        {
            enemy = FindObjectOfType<Enemy>();
            uiManager.ReduceHealth(enemy.Damage);

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Supernova")
        {
            Debug.Log("Supernova");
            GameObject[] enemies;
            GameObject[] asteroids;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().Health = 0;
            }
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].GetComponent<Asteroid>().Health = 0;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            Debug.Log("Shield");

            StartCoroutine(GameManager.instance.Invulnerable());

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Time Dilation")
        {
            Debug.Log("Time Dilation");

            StartCoroutine(GameManager.instance.TimeDilation());

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Offensive")
        {
            Debug.Log("Offensive");
            if (GetPrefsDamage() < 5)
            {
                PlayerPrefs.SetInt("Player Damage", GetPrefsDamage() + 1);
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Defensive")
        {
            Debug.Log("Defensive");
            if (GetPrefsHealth() < 5)
            {
                PlayerPrefs.SetInt("Player Health", GetPrefsHealth() + 1);
                Health = PersistentManager.getDefensiveLevelUpgrade(GetPrefsHealth());
            }
            Destroy(collision.gameObject);
        }
    }
    void DefensiveUpgrade()
    {
        if (GetPrefsHealth() < 5)
        {
            PlayerPrefs.SetInt("Player Health", GetPrefsHealth() + 1);
            Health = PersistentManager.getDefensiveLevelUpgrade(GetPrefsHealth());
        }
    }
    void OffensiveUpgrade()
    {
        if (GetPrefsDamage() < 5)
        {
            PlayerPrefs.SetInt("Player Damage", GetPrefsDamage() + 1);
        }
    }
}
