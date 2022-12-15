using System;
using UnityEngine;

public class Player : MonoBehaviour                                          
{
    public BrickSpawn spawner;
    public Ball ball;
    public float speed = 5f;
    private Vector2 pStart;
    private Vector2 moveInput;

    private void Awake()
    {
        pStart = transform.position;
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
                transform.position = pStart;
                break;
            
            case GameStateManager.GameState.playing:
                break;
            
            case GameStateManager.GameState.win:
                break;
            
            case GameStateManager.GameState.lose:
                break;

            default: break;
        }
    }
    void Update()
    {
        switch (GameStateManager.Instance.GetCurrentState())
        {
            case GameStateManager.GameState.ready:
               
                if (Input.GetKeyDown(KeyCode.Space) )
                {
                    GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.playing);
                    ball.LaunchBall();
                }
                break;
            
            case GameStateManager.GameState.playing:
                
                moveInput.x = Input.GetAxisRaw("Horizontal");                                   //Player Movement
                                
                transform.position += (Vector3) moveInput * (speed * Time.deltaTime);
                break;
            
            case GameStateManager.GameState.win:
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.ready);
                    spawner.Spawn();
                }
                break;
            
            case GameStateManager.GameState.lose:
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.ready);
                    spawner.Spawn();
                }
                break;
        }
        
       

       
       
    }   
}