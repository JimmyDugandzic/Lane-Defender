/*****************************************************************************
// File Name : PlayerController.cs
// Author : Jimmy D.
// Creation Date : 8/25/2025
//
// Brief Description : Script for PlayerControls
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;



public class PlayerController : MonoBehaviour
{
    private InputAction Moves;

    [SerializeField] private PlayerInput _playerInputInstances;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Rigidbody2D _rb2d2;
    [SerializeField] private GameObject _player;
    [SerializeField] private InputAction Shoot;
    [SerializeField] private GameObject _tank;
    [SerializeField] private GameObject Bullet;
    [SerializeField] Animator Anim;
    [SerializeField] Animation TankAnim;
    

    [SerializeField] private int Lives;
    [SerializeField] private int Health;

    [SerializeField] private GameManager GM;    


    private InputAction PlayerAction;

    [SerializeField] private AudioSource FireBullet;
    [SerializeField] private AudioSource HealthLost;

    [SerializeField] private PlayerInput PlayerInputInstances;

    public bool Moving;

    public bool Moving2;
    

    public float MoveDirection;
    public float MoveDirection2;
   

    public InputAction Shoot1 { get => Shoot; set => Shoot = value; }
    
    public GameObject Bullet1 { get => Bullet; set => Bullet = value; }
    public int Health1 { get => Health; set => Health = value; }
    public AudioSource HealthLost1 { get => HealthLost; set => HealthLost = value; }

    void Start()
    {
        Anim = GetComponent<Animator>();

        _playerInputInstances = GetComponent<PlayerInput>();

        _playerInputInstances.currentActionMap.Enable();

        _rb2d = _player.GetComponent<Rigidbody2D>();

        PlayerAction = _playerInputInstances.currentActionMap.FindAction("Move");

      

        PlayerAction.started += PlayerAction_started;

       

        Shoot = PlayerInputInstances.currentActionMap.FindAction("Shoot");

       

        PlayerAction.canceled += PlayerAction_canceled;
       


        Shoot.started += Shoot_started;

        

        

        

    }

   
    /// <summary>
    /// creates bullet
    /// </summary>
    /// <param name="context"></param>
    private void Shoot_started(InputAction.CallbackContext context)
    {

        Anim.SetTrigger("Shoot");

        FireBullet.Play();
        Instantiate(Bullet, new Vector2(_tank.transform.position.x, _tank.transform.position.y), Quaternion.identity);
        StartCoroutine(HoldShoot());



        Debug.Log("pew");
    }

   

    

    private void PlayerAction_canceled(InputAction.CallbackContext context)
    {
        Moving = false;
        _rb2d.velocity = Vector2.zero;

        
    }

   

    private void PlayerAction_started(InputAction.CallbackContext context)
    {
        Moving = true;
    }


    
    /// <summary>
    /// lets the player move
    /// </summary>
    void Update()
    {
        if (Moving == true)
        {

            MoveDirection = PlayerAction.ReadValue<float>();

            

            _rb2d.velocity = new Vector2(_rb2d.velocity.x , 4 * MoveDirection);
           

           

        }
        else
        {
            _rb2d.velocity = new Vector2(0,0);

          
        }

        
        if (Health1 <= 0)
        {
            SceneManager.LoadScene(0);
        }
        //relaods scene if health is 0 
    }



    public void OnDestroy()
    {
        Shoot.started -= Shoot_started;

        PlayerAction.canceled -= PlayerAction_canceled;

    }
    /// <summary>
    /// when holding spacebar it creates another bullet after a second of wait time 
    /// </summary>
    /// <returns></returns>
    IEnumerator HoldShoot()
    {
        yield return new WaitForSeconds(1);
        FireBullet.Play();
        Anim.SetTrigger("Shoot");
        Instantiate(Bullet, new Vector2(_tank.transform.position.x, _tank.transform.position.y), Quaternion.identity);
        
    }
    /// <summary>
    /// when an enemies hits the player it dies and the players health goes down by one 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Health1--;
            HealthLost1.Play();
            Destroy(collision.gameObject);


        }
    }

    

}
