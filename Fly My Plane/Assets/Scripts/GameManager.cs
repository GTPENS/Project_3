﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int score;
    private bool isGameOver;
    private Player player;

    string placementId = "rewardedVideo";
#if UNITY_IOS
    private string gameId = "2890725";
#elif UNITY_ANDROID
    private string gameId = "2890724";
#endif

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        PersistentManager.initAsteroidList();
        PersistentManager.initEnemyList();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Score = 0;
        isGameOver = false;
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (player.Health <= 0)
        {
            if (!isGameOver) {
                isGameOver = true;
                ShowAd();
            }
        }
    }

    public IEnumerator TimeDilation()
    {
        Time.timeScale = 0.5f;
        FindObjectOfType<Player>().PlayerSpeed *= 2;
        GameObject.Find("Player_Gun").GetComponent<Gun>().ProjectileSpeed *= 2;
        GameObject.Find("Player_Gun").GetComponent<Gun>().FireRate /= 2;
        yield return new WaitForSeconds(3f);
        FindObjectOfType<Player>().PlayerSpeed /= 2;
        GameObject.Find("Player_Gun").GetComponent<Gun>().ProjectileSpeed /= 2;
        GameObject.Find("Player_Gun").GetComponent<Gun>().FireRate *= 2;
        Time.timeScale = 1f;
    }

    public IEnumerator Invulnerable()
    {
        GameObject.Find("Player").GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        GameObject.Find("Player").GetComponent<Collider2D>().enabled = true;
    }

    public void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(placementId, options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video selesai-tawarkan coin ke pemain");
            player.transform.position = new Vector2(0, -3.5f);
            player.Health = player.MaxHealth;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video dilewati-tidak menawarkan coin ke pemain");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video tidak ditampilkan");
        }
    }
}
