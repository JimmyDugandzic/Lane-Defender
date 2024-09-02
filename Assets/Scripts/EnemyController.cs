/*****************************************************************************
// File Name : EnemyController.cs
// Author : Jimmy D.
// Creation Date : 8/28/2025
//
// Brief Description : Script for Enemy Movment
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemySpawner ES;
    [SerializeField] private int _speed;
    [SerializeField] private const float ScrollWidth = 13.5f;
    [SerializeField] private GameManager GM;

    [SerializeField] private int Lives;
    [SerializeField] private bool Stun;

    

    [SerializeField] private PlayerController PC;

    [SerializeField] private AudioSource EnemyHit;
    [SerializeField] private AudioSource EnemyDeath;

    [SerializeField] Animator Anim;
    

    public int Lives1 { get => Lives; set => Lives = value; }

    void Start()
    {
        Stun = false;

        ES = FindAnyObjectByType<EnemySpawner>();

        PC = FindAnyObjectByType<PlayerController>();

        Anim.SetBool("Hit", false);
    }

    
    void Update()
    {

        EnemyMovement();

    }

    
    /// <summary>
    /// destroys the enemies if they get to far from the screen
    /// </summary>
    /// <param name="pos"></param>
    public void HandleOffScreen(ref Vector2 pos)
    {
        Destroy(gameObject);
    }





    /// <summary>
    /// how the enemies actually move to the left
    /// </summary>
    void EnemyMovement()
    {

        if(Stun != true)
        {
            Vector2 currentposition = transform.position;

            currentposition.x -= _speed * Time.deltaTime;
            if (transform.position.x < -ScrollWidth)
            {
                HandleOffScreen(ref currentposition);

            }
            transform.position = currentposition;
        }
       
        

    }

    /// <summary>
    /// when the enemy and bullet collide
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("mehehehe");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            StartCoroutine(EnemyStun());

            
            





            Lives1--;

            EnemyHit.Play();

            GM.UpdateScore();


            Debug.Log("DEAD3");
            if (Lives1 <= 0)
            {
                StartCoroutine(EnemyWait());
                
                
            }
            //when colliding with a bullet the enemies life goes down by 1 and if they run out they get destroyed
           



            Destroy(collision.gameObject);
            Debug.Log("DEAD");
        }
        Debug.Log("DEAD2");

        if (collision.gameObject.CompareTag("KillWall"))
        {
            Destroy(gameObject);
            Debug.Log("DEAD");
        }
        //if an enemey touches the wall that destroys the bullets it also dies
       
        if (collision.gameObject.CompareTag("HealthWall"))
        {
            PC.Health1--;
            Debug.Log($"{PC.Health1}");

            PC.HealthLost1.Play();

            Destroy(gameObject);
            Debug.Log("DEAD");
        }
        //if an enemy touches the wall behind the player the player loses a life and the enemy dies
    }

    /// <summary>
    /// has the enemies stop moving for a second when hit
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyStun()
    {
        Anim.SetBool("Hit", true);
        Debug.Log("HitTrue");
        Stun = true;
        yield return new WaitForSeconds(1);
        Stun = false;
        Debug.Log("stunned");
        Anim.SetBool("Hit", false);
        Debug.Log("HitFalse");
        
    }
    /// <summary>
    /// waits befre destroying the enemy so the animation can play
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyWait()
    {
        Anim.SetBool("Hit", true);
        yield return new WaitForSeconds(1);
        Anim.SetBool("Hit", false);
        Destroy(gameObject);
        EnemyDeath.Play();
    }







}
