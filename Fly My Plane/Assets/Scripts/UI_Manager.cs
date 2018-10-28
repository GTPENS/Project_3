using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    [SerializeField]private Slider sliderHealth;
    [SerializeField]private Text txtScore;
    private Player player;

	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<Player>();
        sliderHealth.maxValue = player.Health;
        sliderHealth.value = player.Health;
        txtScore.text = "Score = " + GameManager.instance.Score; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        sliderHealth.value = player.Health;
        txtScore.text = "Score = " + GameManager.instance.Score;
    }

    public void AddScore(int _score)
    {
        GameManager.instance.Score += _score;
    }

    public void ReduceHealth(float _damage)
    {
        player.Health -= _damage;
    }
}
