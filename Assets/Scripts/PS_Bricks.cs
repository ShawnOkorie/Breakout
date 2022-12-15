using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Bricks : MonoBehaviour
{
    public BrickSpawn spawner;
    private ParticleSystem particleSystem;
    public int particleCount;
    private Color particleColor;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        spawner.BeforeBrickInactive += EmitParticles;
    }
    private void OnDisable()
    { 
        spawner.BeforeBrickInactive -= EmitParticles;
    }
    
    private void EmitParticles(Vector2 brickposition, Color brickcolor)
    {
        transform.position = brickposition;
        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = brickcolor;
        particleSystem.Emit(particleCount);
    }
}

   