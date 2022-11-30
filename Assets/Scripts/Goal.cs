using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI textRef;
    public GameObject text0;
    public BrickSpawn brickSpawner;
    public int lives = 5;
    private int startlives;
    private bool ingame;
    public void Awake()
    {
        
    }
    private void Start()
    {
        startlives = lives;
        textRef.text = "Press Space to Start \n \n Lives: " + lives;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ingame == false)                   //Disabling the text when the Ball Starts moving
        {
            text0.SetActive(false);
            ingame = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("Ball"))                                 
        {
            lives--;
            text0.SetActive(true);
            textRef.text = "Press Space to Start \n \n Lives:" + lives;
            ingame = false;
        }

        if (lives <= 0)                                                         //Restart the Game when out of Lives
        {
            lives = startlives;
            textRef.text = "Press Space to Start \n \n Lives:" + lives;
            brickSpawner.DestroyBricks();
            brickSpawner.Spawn();
        }
    }
}
