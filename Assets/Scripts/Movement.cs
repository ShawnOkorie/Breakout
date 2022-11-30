using UnityEngine;

public class Movement : MonoBehaviour                                          //Player Movement
{
    public float speed = 5f;
    private float _startSpeed;
    private Vector2 _moveInput;
   
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");

        transform.position += (Vector3) _moveInput * (speed * Time.deltaTime);
    }
}