using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public void update() {
        if (Input.acceleration.x != 0) {
            move(Input.acceleration.x / 2);
        }
    }

    public void move(float _speed){
        if (_speed > 0.5f)
            _speed = 0.5f;
        if (_speed < -0.5f)
            _speed = -0.5f;

        if (Mathf.Abs(transform.position.x) <= 2f)
            transform.Translate(_speed, 0, 0);
    }
}
