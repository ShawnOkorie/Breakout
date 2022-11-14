using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PhysicsMov : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private float horizontal;
    private float vertical;
    
    public float speed = 5f;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        print(col.collider.gameObject.name);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 inputDirection = Vector2.ClampMagnitude(new Vector2(horizontal, vertical), 1f);
        
        // float vectorLength = inputDirection.magnitude;
        //
        // if (vectorLength > 1f)
        // {
        //     rb.velocity = inputDirection.normalized * speed;
        // }
        // else
        // {
        //     rb.velocity = inputDirection * speed;
        // }

        myRigidbody2D.velocity = inputDirection * speed;

    }

  
}
