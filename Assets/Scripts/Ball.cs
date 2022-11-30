using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour                                             //Behaviour of the Ball
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 bStart;
    private Vector2 bvStart;
    public float speedMultiplier = 1.05f;
    public float speed = 10f;
    private int startDir;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bStart = transform.position;                                  
        bvStart = myRigidbody2D.velocity;
    }

    private void Update()                                                    //Check if Ball can be launched
    {
        if (myRigidbody2D.velocity == Vector2.zero)
        { 
            if (Input.GetKeyDown(KeyCode.Space))                             
            {
                LaunchBall();
            }
        }
    }

    private void LaunchBall()                                                //Launching Ball randomly left or right
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

    public void ResetBall()                                                  //Resets Ball to starting Velocity and Position
    {
        transform.position = bStart;
        myRigidbody2D.velocity = bvStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))                       //Improved Ball Bounce Patterns
        {
            myRigidbody2D.velocity *= speedMultiplier;
           
            float hitdistance = transform.position.x - collision.transform.position.x;
            
            float nHitdistance = hitdistance / collision.bounds.extents.x;
            
            Vector2 nDirection = myRigidbody2D.velocity.normalized;
            
            nDirection.x += nHitdistance;
            
            nDirection.Normalize();
            
            myRigidbody2D.velocity = nDirection * myRigidbody2D.velocity.magnitude;
        }
        
        if (collision.gameObject.CompareTag("Finish"))
        {
            ResetBall();
        }
    }
}   
