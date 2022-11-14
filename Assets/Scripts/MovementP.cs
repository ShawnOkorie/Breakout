using System;
using UnityEngine;

public class MovementP : MonoBehaviour
{

    public float speed = 5f;

    private float _startSpeed;
    private Vector2 _moveInput;
    public string Axis;
    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D myCollider;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        _moveInput.y = Input.GetAxisRaw(Axis);

        transform.position += (Vector3) _moveInput * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            print(myCollider.bounds.min.y*0-1);
            print(myCollider.bounds.max.y*0+1);
            
        }

    }
}   






