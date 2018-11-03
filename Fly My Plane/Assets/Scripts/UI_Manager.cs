﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Text txtScore;
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        sliderHealth.maxValue = player.Health;
        sliderHealth.value = player.Health;
        txtScore.text = "" + GameManager.instance.Score;
    }

    // Update is called once per frame
    void Update()
    {
        sliderHealth.value = player.Health;
        txtScore.text = "" + GameManager.instance.Score;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddScore(int _score)
    {
        GameManager.instance.Score += _score;
    }

    public void ReduceHealth(float _damage)
    {
        player.Health -= _damage;
    }

    public void PauseGame(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
