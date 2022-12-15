using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFX : MonoBehaviour
{
    private Ball myBall;
    private AudioSource hitSfx;
    public AudioClip brickhitsfx;
    public AudioClip dmghitsfx;
    public AudioClip elsehitsfx;
    private void Awake()
    {
        myBall = GetComponentInParent<Ball>();
        hitSfx = GetComponent<AudioSource>();
    }

    private void OnEnable()
    { 
        myBall.OnBallCollision += BallHit;
    }

    private void OnDisable()
    { 
        myBall.OnBallCollision -= BallHit;
        
    }

    private void BallHit(Vector2 ballhit, string what)
    {
        switch (what)
        { 
            case "Brick":
                hitSfx.PlayOneShot(brickhitsfx);
                break; 
            
            case "Finish":
                hitSfx.PlayOneShot(dmghitsfx);
                break;
            
            default:
                hitSfx.PlayOneShot(elsehitsfx);
                break;
               
        }
    }
}
