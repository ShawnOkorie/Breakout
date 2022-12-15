using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    private BrickSpawn spawner;
    public Sprite damagedSprite;
    private int brickHP;
    
    public void SetSpawner(BrickSpawn brickSpawn)
    {
        spawner = brickSpawn;
    }

    private void OnEnable()
    {
        brickHP = Random.Range(1,3);
    }

    public int ApplyDamage()
    {
        brickHP--;
        if (brickHP > 0)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }
        
        if (brickHP <= 0)
        {
            gameObject.SetActive(false);
        }
        spawner.OnBrickHit(this);
        
        return brickHP;
    }
    
}
