using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private Vector3 movement{
        set { movement = value; }
        get { return movement; }
    }

	void Start () {
		
	}

    public void update(){
        move();
    }

    private void move(){
        this.transform.Translate(movement);
    }
}
