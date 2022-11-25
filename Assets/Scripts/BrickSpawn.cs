using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class BrickSpawn : MonoBehaviour
{
    public GameObject brickPrefab;
    private SpriteRenderer sprite;
    public int row = 6;
    public int column = 17;
    public float cDistance = 0.5f;
    private int rColor;
    public Color[] colors;

    void Start()
    {
        for (int i = 0; i < row; i++)
        {
            rColor = Random.Range(0 , colors.Length);
            for (int j = 0; j < column; j++)
            {
                GameObject brick = Instantiate(brickPrefab, transform.position, Quaternion.identity);
                brick.GetComponent<SpriteRenderer>().color= colors[rColor];
                transform.position += Vector3.right;
            }
            transform.position += Vector3.left * column;
            transform.position += Vector3.down * cDistance;
        }
    }

   
}
