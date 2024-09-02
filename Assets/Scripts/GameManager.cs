/*****************************************************************************
// File Name : GameManager.cs
// Author : Jimmy D.
// Creation Date : 8/28/2025
//
// Brief Description : Script for GameManager stuff
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int Lives;

    [SerializeField] private EnemyController EMS;

    [SerializeField] private EnemySpawner ES;

    [SerializeField] private int Score;

    [SerializeField] private TMP_Text ScoreText;

    [SerializeField] private TMP_Text HighScoreText;

    public int Lives1 { get => Lives; set => Lives = value; }


    void Start()
    {
        UpdateHighScore();

    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// update score by 100
    /// </summary>
    public void UpdateScore()
    {
        Score += 100;
        ScoreText.text = " Score: " + Score.ToString();
        CheckHighScore();


    }
    /// <summary>
    /// checks the old high score against the new one 
    /// </summary>
    public void CheckHighScore()
    {
        if (Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            UpdateHighScore();
        }
    }
    /// <summary>
    /// updates current highscore to new one 
    /// </summary>
    public void UpdateHighScore()
    {
        HighScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }


}
