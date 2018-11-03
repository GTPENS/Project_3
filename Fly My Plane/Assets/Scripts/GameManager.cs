using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int score;

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
    }

    private void Update()
    {

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
}
