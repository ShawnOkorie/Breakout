using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour                                             //Behaviour of the Ball
{
    public delegate void BallDelegate(Vector2 ballhit, string what);
    public event BallDelegate OnBallCollision;
    public delegate void PowerUpDelegate(bool isActive);
    public event PowerUpDelegate PowerupActive;
    private Rigidbody2D myRigidbody2D;
    private Vector2 bStart;
    private Vector2 bvStart;
    public float speedMultiplier = 1.05f;
    public float speed = 10f;
    public int powerupCount = 5;
    public float powerupDuration = 3;
    private int destroyCount; 
    private float powerupTimer;
    private  bool timerStart = false;
    public float explosionSize = 3f;
    public LayerMask brickMask;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        PowerupActive?.Invoke(false);
        bStart = transform.position;                                  
        bvStart = myRigidbody2D.velocity;
    }

    private void Update()
    {
        if (timerStart)
        {
            if (powerupTimer >= 0)
            {
                powerupTimer -= Time.deltaTime; 
            }
            else
            {
                PowerupActive?.Invoke(false);
                print("stop");
                GetComponent<SpriteRenderer>().color = Color.white;
                timerStart = false;
                destroyCount = 0;  
            } 
        }
    }

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.ready:
                break;
            
            case GameStateManager.GameState.playing:
                break;
            
            case GameStateManager.GameState.win:
                ResetBall();
                break;
            
            case GameStateManager.GameState.lose:
                ResetBall();
                break;
            
            default: break;
        }
    }
    
    public void LaunchBall()                                                //Launching Ball randomly left or right
    {
        if (myRigidbody2D.velocity == Vector2.zero)
        {
            myRigidbody2D.velocity = Vector2.up * speed;
        }
    }

    public void ResetBall()                                                  //Resets Ball to starting Velocity and Position
    {
        transform.position = bStart;
        myRigidbody2D.velocity = bvStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            OnBallCollision?.Invoke(transform.position, collision.gameObject.tag);
            ResetBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            OnBallCollision?.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
            
            myRigidbody2D.velocity = myRigidbody2D.velocity * speedMultiplier;
            
            float hitdistance = transform.position.x - collision.transform.position.x;                                  //Improved Ball Bounce Patterns
            
            float nHitdistance = hitdistance / collision.transform.localScale.x;
            
            Vector2 nDirection = myRigidbody2D.velocity.normalized;
            
            nDirection.x += nHitdistance;
            
            nDirection.Normalize();
            
            myRigidbody2D.velocity = nDirection * myRigidbody2D.velocity.magnitude;
        }

        if (collision.collider.CompareTag("Brick"))
        {
            OnBallCollision?.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
            
            Brick brick = collision.gameObject.GetComponent<Brick>();

            int lastbrickhp = 0;
            
            if (brick != null)
            {
               lastbrickhp = brick.ApplyDamage();
            }

            if (lastbrickhp == 0)
            {
                destroyCount++;
                print(destroyCount);
            }

            if (destroyCount == powerupCount)
            {
                PowerupActive?.Invoke(true);
                
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionSize, Vector2.zero, explosionSize, brickMask);

                GetComponent<SpriteRenderer>().color = Color.red;
                
                foreach (var hit in hits)
                {
                    hit.collider.gameObject.GetComponent<Brick>().ApplyDamage();
                }
               
                powerupTimer = powerupDuration;
                timerStart = true;
                print("start");
                
            }
        }

        if (collision.collider.CompareTag("Finish"))
        {
            OnBallCollision.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
            PowerupActive?.Invoke(false);
        }

        if (collision.collider.CompareTag("Wall"))
        {
            OnBallCollision.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
        }
    }
}   
