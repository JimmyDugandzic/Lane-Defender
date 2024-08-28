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
using UnityEditor.Timeline.Actions;

public class PlayerController : MonoBehaviour
{
    private InputAction Moves;

    [SerializeField] private PlayerInput _playerInputInstances;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private GameObject _player;
    [SerializeField] private InputAction Shoot;
    [SerializeField] private GameObject _tank;
    [SerializeField] private GameObject Bullet;
    


    private InputAction PlayerAction;
    private InputAction PlayerAction2;

    [SerializeField] private PlayerInput PlayerInputInstances;

    public bool Moving;
    public bool Moving2;

    public float MoveDirection;
    public float MoveDirection2;

    public InputAction Shoot1 { get => Shoot; set => Shoot = value; }
    
    public GameObject Bullet1 { get => Bullet; set => Bullet = value; }

    void Start()
    {
        _playerInputInstances = GetComponent<PlayerInput>();

        _playerInputInstances.currentActionMap.Enable();

        _rb2d = _player.GetComponent<Rigidbody2D>();

        PlayerAction = _playerInputInstances.currentActionMap.FindAction("W/S");

        PlayerAction2 = _playerInputInstances.currentActionMap.FindAction("Arrows");

        PlayerAction.started += PlayerAction_started;

        PlayerAction2.started += PlayerAction2_started;

        Shoot = PlayerInputInstances.currentActionMap.FindAction("Shoot");

       

        PlayerAction.canceled += PlayerAction_canceled;

        PlayerAction2.canceled += PlayerAction2_canceled;

        Shoot.started += Shoot_started;


    }

    private void PlayerAction2_canceled(InputAction.CallbackContext context)
    {
        Moving2 = false;
        _rb2d.velocity = Vector2.zero;
    }

    private void PlayerAction2_started(InputAction.CallbackContext context)
    {
        Moving2 = true;
    }

    private void Shoot_started(InputAction.CallbackContext context)
    {
        Instantiate(Bullet, new Vector2(_tank.transform.position.x, _tank.transform.position.y), Quaternion.identity);
        StartCoroutine(HoldShoot());

        

        Debug.Log("ehhhhhh?");
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


    

    void Update()
    {
        if (Moving == true)
        {

            MoveDirection = PlayerAction.ReadValue<float>();
           
            
            _rb2d.velocity = new Vector2(_rb2d.velocity.x , 4 * MoveDirection);
            Debug.Log(MoveDirection + "up");

        }
        else
        {
            _rb2d.velocity = new Vector2(0,0);
        }




        if (Moving2 == true)
        {

            MoveDirection2 = PlayerAction2.ReadValue<float>();


            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 4 * MoveDirection2);
            Debug.Log(MoveDirection2 + "up");

        }
        else
        {
            _rb2d.velocity = new Vector2(0, 0);
        }


    }



    public void OnDestroy()
    {
        Shoot.started -= Shoot_started; 
    }

    IEnumerator HoldShoot()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Bullet, new Vector2(_tank.transform.position.x, _tank.transform.position.y), Quaternion.identity);
    }
}
