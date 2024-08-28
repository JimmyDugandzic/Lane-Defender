using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   [SerializeField] private Rigidbody2D BulletRB2D;
   [SerializeField] private Rigidbody2D RB2D;
      

    // [SerializeField] private AudioClip EnemyDeathSound;
    [SerializeField] private AudioClip BulletSound;
    void Start()
    {
        
        BulletRB2D.velocity = Vector2.right * 100;
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // AudioSource.PlayClipAtPoint(EnemyDeathSound, transform.position);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
