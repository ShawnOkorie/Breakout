using UnityEngine;
using Random = UnityEngine.Random;
public class BrickSpawn : MonoBehaviour
{
    public GameObject brickPrefab;
    private SpriteRenderer sprite;
    private Vector2 startP;
    public Color[] colors;
    private GameObject [,] brickArray;
    public int row = 6;
    public int column = 17;
    public float cDistance = 0.5f;
    public float rDistanceR = 1.5f;
    private float rDistanceL;
    private int rColor;
    
    void Start()
    {
        startP = transform.position;
        rDistanceL = rDistanceR * (-1);
        brickArray = new GameObject[row, column];
        Spawn();
    }
    
    public void Spawn()                                             //Spawns Bricks and chooses a random colour for every row 
    { 
        for (int i = 0; i < row; i++)
        {
            rColor = Random.Range(0 , colors.Length);
            for (int j = 0; j < column; j++)
            {
                brickArray[i,j] = Instantiate(brickPrefab, transform.position, Quaternion.identity);
                brickArray[i,j].GetComponent<SpriteRenderer>().color= colors[rColor];
                transform.position += new Vector3(x: rDistanceR, y: 0f, z: 0f);
            }
            transform.position += new Vector3(x: rDistanceL, y: 0f, z: 0f) * column;
            transform.position += Vector3.down * cDistance;
        }

        transform.position = startP;
    }

    public void DestroyBricks()                                     //Deletes every Brick
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            { 
                brickArray[i,j].SetActive(true);
                
                Destroy(brickArray[i,j]);
            }
        }  
    }
}
