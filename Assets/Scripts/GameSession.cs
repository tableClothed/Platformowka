using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int pointPerCoin = 45;

    int points;

    [SerializeField] Text lifes;
    [SerializeField] Text score;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        points = 0;
    }

    private void Update()
    {
        showText();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
            Destroy(gameObject);
        }
    }
    private int TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        return playerLives;
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
    }

    public int PointCount()
    {
        points = points + pointPerCoin;
        return points;
    }

    private void showText()
    {
        score.text = points.ToString();
        lifes.text = playerLives.ToString();
    }
}