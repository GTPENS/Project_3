using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float health;
    [SerializeField] private float damage;

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

    private void Start()
    {
        bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        gun = GameObject.Find("Gun");
    }

    private void Update () {
        Debug.Log("Player Health = " + Health);
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

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
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

        if (Mathf.Abs(transform.position.x) <= 3f)
        {
            transform.Translate(speed, 0, 0);
            //health ngurang
        }
    }

    public void Fire()
    {
        GameObject bulletGO = Instantiate(bullet, gun.transform.position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);
        Destroy(bulletGO, 2f);
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
    }
}
