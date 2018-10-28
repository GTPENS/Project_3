using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField] private bool isEnemy;
    private const float radius = 1f;

    private GameObject bullet;
    private int numberOfProjectiles;

    private float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private float projectileSpeed = 15f;

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

    public float FireRate
    {
        get
        {
            return fireRate;
        }

        set
        {
            fireRate = value;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }

        set
        {
            projectileSpeed = value;
        }
    }

    // Use this for initialization
    void Start () {
        bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        numberOfProjectiles = 1;
    }
	
	// Update is called once per frame
	void Update () {
        Fire();
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            if (numberOfProjectiles > 1)
            {
                float angleStep = 30 / numberOfProjectiles;
                float angle = -15;

                for (int i = 0; i <= numberOfProjectiles; i++)
                {
                    float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                    float projectileDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                    Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
                    Vector3 projectileMoveDirection = (projectileVector - transform.position).normalized * ProjectileSpeed;

                    GameObject tmpObj = Instantiate(bullet, transform.position, Quaternion.identity);
                    tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

                    Destroy(tmpObj, 2f);

                    angle += angleStep;
                }
            }
            else
            {
                GameObject bulletGO = Instantiate(bullet, transform.position, transform.rotation);
                if(!isEnemy)
                {
                    bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
                }
                else
                {
                    bulletGO.gameObject.tag = "Enemy Bullet";
                    bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
                }
                    
                Destroy(bulletGO, 2f);
            }
        }
    }
}
