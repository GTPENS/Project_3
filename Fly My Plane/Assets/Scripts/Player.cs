using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
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

        if (Mathf.Abs(transform.position.x) <= 2f)
            Debug.Log("MWATEK");
            //transform.Translate(speed, 0, 0);
    }
}
