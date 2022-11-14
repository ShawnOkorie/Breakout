using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public float speed = 5f;
    public float speedMultiplier = 1.5f;
    private float _startSpeed ;
    private Vector2 _moveInput;

    private BoxCollider2D myCollider;
    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
        
        
        
    }

    void Update()
    {
        /*
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float step = speed * Time.deltaTime;
        float vectorLength = new Vector3(horizontal, vertical, 0).magnitude;
        /*    
            //wasd Controls
            /*
            if (vectorLength > 1)                                                                              
            {
                transform.position += new Vector3(horizontal, vertical, 0)* step / vectorLength;
            }
            else
            {
                transform.position += new Vector3(horizontal, vertical, 0)* step;
            }
            */
           
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            
            _moveInput.Normalize();
        
            transform.position += (Vector3) _moveInput * (speed * Time.deltaTime);
            //Sprint Taste
            if (Input.GetKeyDown(KeyCode.LeftShift))   
            {
                speed = speed * speedMultiplier ;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = _startSpeed;
            }
            
            

           
           
            //collider
         

            Vector2 colliderSize = new Vector2( transform.localScale.x * myCollider.size.x,
                transform.localScale.y * myCollider.size.y);

            Vector2 colliderPoint = new Vector2(transform.localScale.x * myCollider.offset.x + transform.position.x,
                transform.localScale.y * myCollider.offset.y + transform.position.y);


            Collider2D[] colliders = Physics2D.OverlapBoxAll(colliderPoint , colliderSize, 0);
        
            foreach (Collider2D othercollider in colliders)
            {
                if (othercollider == myCollider) continue;

                Vector3 myboundscenter = transform.position;
                Vector3 myboundextents = myCollider.bounds.extents;


                float overlap = myboundscenter.x + myboundextents.x - othercollider.bounds.center.x + othercollider.bounds.extents.x;
           
                transform.position += Vector3.right * overlap;
                
              
              
            }
    }
}
