using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int score;
    private bool isGameOver;
    private bool seen;
    private int gameState;
    private int tempScore;
    private Player player;

    private const int ENUM_DANGER_LEVEL_UP_TRESHOLD = 200;
    private const int ENUM_DANGER_LEVEL_DOWN_TRESHOLD = 100;

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

    public int TempScore {
        get {
            return tempScore;
        }

        set {
            tempScore = value;
        }
    }

    public bool Seen
    {
        get
        {
            return seen;
        }

        set
        {
            seen = value;
        }
    }

    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }

        set
        {
            isGameOver = value;
        }
    }

    public int GameState
    {
        get
        {
            return gameState;
        }

        set
        {
            gameState = value;
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
        TempScore = 0;
        IsGameOver = false;
        player = FindObjectOfType<Player>();
        GameState = 0;
        StartCoroutine(StateCheck());
    }

    private void Update()
    {
        Debug.Log("GAME STATE = " + GameState);
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        CheckGameOver();
        //ChangeState();
    }

    //tambahno nang kene nek bengong Line
    private IEnumerator StateCheck()
    {
        if (TempScore < ENUM_DANGER_LEVEL_DOWN_TRESHOLD)
            ChangeState(false);
        else if (TempScore > ENUM_DANGER_LEVEL_UP_TRESHOLD)
            ChangeState(true);

        TempScore = 0;
        yield return new WaitForSecondsRealtime(60f);
        StartCoroutine(StateCheck());
    }

    private void ChangeState(bool up)
    {
        if (up && GameState < 4)
            GameState++;

        if (!up && GameState > 0)
            GameState--;

    }

    private void CheckGameOver()
    {
        if(player != null)
        {
            if (player.Health <= 0)
            {
                if (!IsGameOver)
                {
                    IsGameOver = true;
                    if (!Seen)
                    {
                        UI_Manager ui = FindObjectOfType<UI_Manager>();
                        ui.imgPopUp.gameObject.SetActive(true);
                        FindObjectOfType<Player>().enabled = false;
                        FindObjectOfType<EnemySpawner>().Canceling();
                        FindObjectOfType<ObjectSpawner>().Canceling();
                        FindObjectOfType<Gun>().enabled = false;
                        //ShowAd();
                    }
                }
                else if (Seen)
                {
                    FindObjectOfType<UI_Manager>().BackToMainMenu();
                }
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
        AudioManager.instance.GetComponent<AudioSource>().mute = true;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(placementId, options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            player.transform.position = new Vector2(0, -3.5f);
            player.Health = player.MaxHealth;
            Seen = true;
            AudioManager.instance.GetComponent<AudioSource>().mute = false;
            FindObjectOfType<Player>().enabled = true;
            FindObjectOfType<EnemySpawner>().NextEnemySpawn();
            FindObjectOfType<ObjectSpawner>().NextAsteroidSpawn();
            FindObjectOfType<Gun>().enabled = true;
        }
        else if (result == ShowResult.Skipped)
        {
            player.transform.position = new Vector2(0, -3.5f);
            player.Health = player.MaxHealth;
            Seen = true;
            AudioManager.instance.GetComponent<AudioSource>().mute = false;
            FindObjectOfType<Player>().enabled = true;
            FindObjectOfType<EnemySpawner>().NextEnemySpawn();
            FindObjectOfType<ObjectSpawner>().NextAsteroidSpawn();
            FindObjectOfType<Gun>().enabled = true;
        }
        else if (result == ShowResult.Failed)
        {
            UI_Manager ui = FindObjectOfType<UI_Manager>();
            ui.BackToMainMenu();
        }
    }
}
