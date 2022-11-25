using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Rigidbody2D myRigidbody2D;
    public float speedMultiplier = 1.05f;
    public float speed = 10f;
    private int startDir;
    private bool spacekeyState;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        print("Press Space to Start");
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
        startDir = Random.Range(0,2);
        switch (startDir)
        {
            case 0:
                myRigidbody2D.velocity = Vector2.left * speed + Vector2.up * speed;
                break;
            case 1:
                myRigidbody2D.velocity = Vector2.right * speed + Vector2.down * speed;
                break;
        }
    }

    private void ResetBall()
    {
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.position = Vector2.zero;
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            myRigidbody2D.velocity *= speedMultiplier;
           
            float hitdistance = transform.position.y - col.transform.position.y;
            
            float nHitdistance = hitdistance / col.bounds.extents.y;
            
            Vector2 nDirection = myRigidbody2D.velocity.normalized;
            
            nDirection.y += nHitdistance;
            
            nDirection.Normalize();
            
            myRigidbody2D.velocity = nDirection * myRigidbody2D.velocity.magnitude;
        }
        
        if (col.gameObject.CompareTag("Finish"))
        {
            ResetBall();
        }
    }
}
