using UnityEngine;

public class Brick : MonoBehaviour
{
    public int instanceCount;

    private void Awake()
    {
        instanceCount++;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.SetActive(false);
        instanceCount--;
    }
    
}
