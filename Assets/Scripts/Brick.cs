using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public static int instanceCount;

    private void Awake()
    {
        instanceCount++;
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        instanceCount--;
        print(instanceCount);
    }
}
