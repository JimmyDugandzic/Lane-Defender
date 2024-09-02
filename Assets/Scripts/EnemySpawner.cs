/*****************************************************************************
// File Name : EnemySpawner.cs
// Author : Jimmy D.
// Creation Date : 8/30/2025
//
// Brief Description : Script for Enemy Spawning
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  [SerializeField] private Transform[] SpawnPoints;

  [SerializeField] private GameObject[] Enemy;

  [SerializeField] private int _speed;

  [SerializeField] private EnemyController EMS;

  [SerializeField] private int LastNumber;

  public float Timer;
  public float timerMax;
  public bool GameStarted;

  public GameObject[] Enemy_ { get => Enemy; set => Enemy = value; }

  void Start()
  {
      Time.timeScale = 1;
      GameStarted = true;
  }

    /// <summary>
    /// timer to spawn enemies
    /// </summary>
  void Update()
  {

      if (GameStarted == true)
      {
          if (Timer > 0)
          {
              Timer -= Time.deltaTime;
          }
          else if (Timer <= 0)
          {
              SpawnEnemy();
              Timer = timerMax;
          }

      }


    }


    
    /// <summary>
    /// spawns enemies at random spawnpoints
    /// </summary>
  public void SpawnEnemy()
  {
      Transform CurrentSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
      int randomNum = Random.Range(0, Enemy_.Length);
      while (randomNum == LastNumber)
      {
          randomNum = Random.Range(0, Enemy_.Length);
      }

      GameObject ES2Spawn = Enemy_[randomNum];
      Instantiate(ES2Spawn, CurrentSpawnPoint.position, Quaternion.identity);


  }


}
