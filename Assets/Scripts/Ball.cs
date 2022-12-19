using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public delegate void BallDelegate(Vector2 ballhit, string what);
    public event BallDelegate OnBallCollision;
    public delegate void PowerUpDelegate(bool isActive);
    public event PowerUpDelegate PowerupActive;
    public delegate void ExplosionDelegate(Vector2 ballLocation);
    public event ExplosionDelegate PowerupHit;
    private Rigidbody2D myRigidbody2D;
    private Vector2 bposStart;
    private Vector2 bvelStart;
    public float speedMultiplier = 1.05f;
    public float speed = 10f;
    public float speedIncrease = 1f;
    public int powerupCount = 5;
    public float powerupDuration = 3;
    private int destroyCount; 
    private float powerupTimer;
    private  bool timerStart = false;
    public float explosionSize = 3f;
    public LayerMask brickMask;
    public Explosion explosion;
    public  bool powerupActive = false;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        PowerupActive?.Invoke(false);
        bposStart = transform.position;                                  
        bvelStart = myRigidbody2D.velocity;
    }

    private void Update()
    {
        if (timerStart)
        {
            if (powerupTimer >= 0 && powerupActive)
            {
                powerupTimer -= Time.deltaTime; 
            }
            else
            {
                StopPowerup();
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
                speed += speedIncrease;
                ResetBall();
                break;
            
            case GameStateManager.GameState.lose:
                ResetBall();
                break;
            
            default: break;
        }
    }
    
    public void LaunchBall()
    {
        if (myRigidbody2D.velocity == Vector2.zero)
        {
            myRigidbody2D.velocity = Vector2.up * speed;
        }
    }

    public void ResetBall()
    {
        transform.position = bposStart;
        myRigidbody2D.velocity = bvelStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            OnBallCollision?.Invoke(transform.position, collision.gameObject.tag);
            ResetBall();
            powerupActive = false;
            StopPowerup();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            OnBallCollision?.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
            
            myRigidbody2D.velocity = myRigidbody2D.velocity * speedMultiplier;
            
            float hitdistance = transform.position.x - collision.transform.position.x;
            
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

                powerupActive = true;

                if (powerupActive)
                {
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionSize, Vector2.zero, explosionSize, brickMask);

                    GetComponent<SpriteRenderer>().color = Color.red;
                
                    foreach (var hit in hits)
                    {
                        hit.collider.gameObject.GetComponent<Brick>().ApplyDamage();
                    } 
                }
                powerupTimer = powerupDuration;
                timerStart = true;
                print("start");
            }
            
            if (powerupActive)
            {
                PowerupHit?.Invoke(transform.position);
            }  
        }

        if (collision.collider.CompareTag("Wall"))
        {
            OnBallCollision?.Invoke(collision.GetContact(0).point, collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    private void StopPowerup()
    {
        PowerupActive?.Invoke(false);
        print("stop");
        GetComponent<SpriteRenderer>().color = Color.white;
        timerStart = false;
        powerupTimer = 0;
        destroyCount = 0;  
    }
}   
