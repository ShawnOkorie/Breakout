using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bGoal : MonoBehaviour
{
    public TextMeshProUGUI textRef;
    public GameObject Text_0;
    private BrickSpawn brickSpawner;
    private Brick Bricks;
    private BallB ball;
    public int lives = 5;
    private bool ingame;
    public void Awake()
    {
        Bricks = GetComponent<Brick>();
        ball = GetComponent<BallB>();
        brickSpawner = GetComponent<BrickSpawn>();
    }
    private void Start()
    {
        textRef.text = "Press Space to Start \n \n Lives: " + lives;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ingame == false)
        {
            Text_0.SetActive(false);
            ingame = true;
        }

        if (Bricks.instanceCount <= 0)
        {
            textRef.text = "You Win! \n \n Press Space to Play again";
                ball.ResetBall();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    brickSpawner.Restart(); 
                }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("Ball"))
        {
            lives--;
            Text_0.SetActive(true);
            textRef.text = "Press Space to Start \n \n Lives:" + lives;
            ingame = false;
        }

        if (lives <= 0)
        {
            textRef.text = "Press Space to Start \n \n Lives:" + lives;
            brickSpawner.DestroyBricks();
            brickSpawner.Spawn();
            
            
        }
       
    }
}
