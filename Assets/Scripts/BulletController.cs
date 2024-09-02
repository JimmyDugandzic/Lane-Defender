/*****************************************************************************
// File Name : BulletController.cs
// Author : Jimmy D.
// Creation Date : 8/26/2025
//
// Brief Description : Script for Bullet stuff
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
   [SerializeField] private Rigidbody2D BulletRB2D;
   [SerializeField] private GameManager GM;

   [SerializeField] private EnemyController EC;

    [SerializeField] Animator Anim;

    /// <summary>
    /// bullet goes to the right 
    /// </summary>
    void Start()
    {
        
        BulletRB2D.velocity = Vector2.right * 100;
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// deletes the bullet if it misses enemies 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillWall"))
        {
            Destroy(gameObject);
            Debug.Log("DEAD");
        }

        

    }

    



}
