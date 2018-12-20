using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    public Slider sliderHealth;
    public Text txtScore;
    public Image imgPopUp;
    private Player player;
    private Gun gun;
    private EnemySpawner enemySpawner;
    private ObjectSpawner objectSpawner;

    // Use this for initialization
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 1;
            player = FindObjectOfType<Player>();
            gun = FindObjectOfType<Gun>();
            enemySpawner = FindObjectOfType<EnemySpawner>();
            objectSpawner = FindObjectOfType<ObjectSpawner>();
            sliderHealth.maxValue = player.MaxHealth;
            sliderHealth.value = player.Health;
            txtScore.text = "" + GameManager.instance.Score;
            player.DisableMovement = true;
            gun.enabled = false;
            enemySpawner.enabled = false;
            objectSpawner.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            sliderHealth.maxValue = player.MaxHealth;
            sliderHealth.value = player.Health;
            txtScore.text = "" + GameManager.instance.Score;
        }
    }

    public void TouchToStart()
    {
        player.DisableMovement = false;
        gun.enabled = true;
        enemySpawner.enabled = true;
        objectSpawner.enabled = true;
    }

    public void StartGame()
    {
        GameManager.instance.Seen = false;
        GameManager.instance.IsGameOver = false;
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
        AudioManager.instance.PlayOtherSFX(0);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        FindObjectOfType<Player>().enabled = false;
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.PlayOtherSFX(0);
        SceneManager.LoadScene(0);
    }

    public void ResumeGame(GameObject pauseMenu)
    {
        AudioManager.instance.PlayOtherSFX(0);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<Player>().enabled = true;
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayAudio(0);
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ShowAd()
    {
        GameManager.instance.ShowAd();
    }
}
