using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public GameObject explosion;

	void Update () {
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
    }

    void Move(float speed)
    {
        if (speed > 0.5f)
        {
            speed = 0.5f;
        }
        
        if (speed < -0.5f)
        {
            speed = -0.5f;
        }

        if (Mathf.Abs(transform.position.x) <= 2.5f)
        {
            transform.Translate(speed, 0, 0);
            //health ngurang
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            Destroy(collision.gameObject);
            GameObject explosionGO = Instantiate(explosion, transform.position = new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
        }
    }
}
