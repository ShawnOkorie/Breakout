using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallB : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Rigidbody2D myRigidbody2D;
    public float speedMultiplier = 1.05f;
    public float speed = 10f;
    private int startDir;
    private bool spacekeyState;
    private Vector2 bStart;
    private Vector2 bvStart;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        bStart = transform.position;
        bvStart = myRigidbody2D.velocity;
    }

    private void Update()
    {
        if (myRigidbody2D.velocity == Vector2.zero)
        {
            spacekeyState = Input.GetKeyDown(KeyCode.Space);
            if (spacekeyState == true)
            {
                LaunchBall();
            }
        }
    }

    private void LaunchBall()
    {
        startDir = Random.Range(0, 2);
        switch (startDir)
        {
            case 0:
                myRigidbody2D.velocity = Vector2.right * speed + Vector2.down * speed;
                break;
            case 1:
                myRigidbody2D.velocity = Vector2.left * speed + Vector2.down * speed;
                break;
        }
    }

    public void ResetBall()
    {
        transform.position = bStart;
        myRigidbody2D.velocity = bvStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myRigidbody2D.velocity *= speedMultiplier;
           
            float hitdistance = transform.position.x - collision.transform.position.x;
            
            float nHitdistance = hitdistance / collision.bounds.extents.x;
            
            Vector2 nDirection = myRigidbody2D.velocity.normalized;
            
            nDirection.y += nHitdistance;
            
            nDirection.Normalize();
            
            myRigidbody2D.velocity = nDirection * myRigidbody2D.velocity.magnitude;
        }
        
        if (collision.gameObject.CompareTag("Finish"))
        {
            ResetBall();
        }
    }
}   
