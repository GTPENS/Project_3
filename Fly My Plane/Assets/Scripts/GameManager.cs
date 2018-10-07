using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<GameObject> listOfGameObjects =  new List<GameObject>();
    private GameObject player,asteroidNormal,asteroidFast,asteroidSmall,asteroidArmored;

    private void Start()
    {
        player = Resources.Load("Assets/Resources/Prefab/Player") as GameObject;
        asteroidNormal = Resources.Load("Assets/Resources/Prefab/Asteroid_Normal") as GameObject;
        asteroidFast = Resources.Load("Assets/Resources/Prefab/Asteroid_Fast") as GameObject;
        asteroidSmall = Resources.Load("Assets/Resources/Prefab/Asteroid_Small") as GameObject;
        asteroidArmored = Resources.Load("Assets/Resources/Prefab/Asteroid_Armored") as GameObject;
    }

    private void attachAsteroid(Vector2 _position, int _id)
    {
        
    }
}
