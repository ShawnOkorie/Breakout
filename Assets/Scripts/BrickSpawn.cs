using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawn : MonoBehaviour
{
    public delegate void BricksSpawnDelegate(Vector2 brickposition, Color brickcolor);
    public event BricksSpawnDelegate BeforeBrickInactive;
    public Brick brickPrefab;
    private SpriteRenderer sprite;
    public Color[] colors;
    private Brick[,] brickArray;
    private List<Color> startColors;
    public int row = 6;
    public int column = 17;
    public float xDistance = 0.5f;
    public float yDistance = 1.5f;
    private int rColor;
    
    
    void Start()
    {
        brickArray = new Brick[row, column];
        startColors = new List<Color>();
        Spawn();
    }
    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.ready:
                break;
            
            case GameStateManager.GameState.playing:
                break;
            
            case GameStateManager.GameState.win:
                DestroyBricks();
                break;
            
            case GameStateManager.GameState.lose:
                DestroyBricks();
                break;
            
            default: break;
        }
    }
    
    public void Spawn()                                                            //Spawns Bricks and chooses a random colour for every row 
    {
        FillColorList();
        
        Vector3 offset = Vector3.zero;
        for (int i = 0; i < row; i++)
        {
            rColor = Random.Range(0, startColors.Count);
            for (int j = 0; j < column; j++)
            {
                brickArray[i, j] = Instantiate(brickPrefab, transform.position + offset, Quaternion.identity);

                brickArray[i, j].SetSpawner(this);

                brickArray[i, j].transform.SetParent(transform);

                brickArray[i, j].GetComponent<SpriteRenderer>().color = startColors[rColor];

                offset += Vector3.right * xDistance;
            }
            startColors.RemoveAt(rColor);
            
            offset.x = 0;
            offset += Vector3.down * yDistance;
        }
    }

    private void FillColorList()
    {
        for (int i = 0; i < colors.Length ; i++)
        {
            startColors.Add(colors[i]);
        }
    }
    public void OnBrickHit(Brick brick)
    {
        BeforeBrickInactive.Invoke(brick.transform.position, brick.gameObject.GetComponent<SpriteRenderer>().color);

        if (IsGameWon())
        {
            GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.win);
        }
    }

    public bool IsGameWon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
    
    public void DestroyBricks()                                                 //Deletes every Brick
    {
        FillColorList();
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (brickArray[i, j] == null)
                {
                   continue;
                }
                brickArray[i, j].gameObject.SetActive(false);
                Destroy(brickArray[i, j].gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = Vector3.zero;

        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < column; x++)
            {
                Gizmos.DrawWireCube(transform.position + offset, new Vector3(1f, 0.5f, 1f));
                
                offset += Vector3.right * xDistance;
            }
            offset.x = 0;
            offset += Vector3.down * yDistance;
        }
    }
}