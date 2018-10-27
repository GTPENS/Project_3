using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float health;
    [SerializeField] private float damage;
    private float projectileSpeed = 10f;
    private const float radius = 1f;

    private int numberOfProjectiles;

    private float fireRate = 0.5f;
    private float nextFire = 0.0f;
    
    private GameObject bullet;
    private GameObject gun;

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

    public int NumberOfProjectiles
    {
        get
        {
            return numberOfProjectiles;
        }

        set
        {
            numberOfProjectiles = value;
        }
    }

    private void Start()
    {
        numberOfProjectiles = 1;
        bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        gun = GameObject.Find("Gun");
    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(5 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-5 * Time.deltaTime);
        }

        if (Input.acceleration.x != 0)
        {
            Move(Input.acceleration.x / 5);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numberOfProjectiles++;
        }
        Fire();
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

        if (Mathf.Abs(transform.position.x) <= 3f)
        {
            transform.Translate(speed, 0, 0);
            //health ngurang
        }
    }

    public void Fire()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            if(numberOfProjectiles > 1)
            {
                float angleStep = 30 / numberOfProjectiles;
                float angle = -15;

                for (int i = 0; i <= numberOfProjectiles; i++)
                {
                    float projectileDirXPosition = gun.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                    float projectileDirYPosition = gun.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                    Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
                    Vector3 projectileMoveDirection = (projectileVector - gun.transform.position).normalized * projectileSpeed;

                    GameObject tmpObj = Instantiate(bullet, gun.transform.position, Quaternion.identity);
                    tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

                    Destroy(tmpObj, 2f);

                    angle += angleStep;
                }
            }
            else
            {
                GameObject bulletGO = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);
                Destroy(bulletGO, 2f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            Asteroid asteroid;
            asteroid = FindObjectOfType<Asteroid>();
            Health -= asteroid.Damage;
            
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy;
            enemy = FindObjectOfType<Enemy>();
            Health -= enemy.Damage;

            Destroy(collision.gameObject);
        }
    }
}
