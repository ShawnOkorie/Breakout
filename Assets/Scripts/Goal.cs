using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI textRef0;
    public TextMeshProUGUI textRef1;
    public GameObject text0;
    public GameObject text1;
    public int lives = 5;
    private int startlives;
    private int levelCleared = 0;
    private void Awake()
    {
        startlives = lives;
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
                text0.SetActive(true);
                text1.SetActive(false);
                textRef0.text = "Press Space to Start \n \n Lives: " + lives;
                break;
            
            case GameStateManager.GameState.playing:
                text0.SetActive(false);
                break;
            
            case GameStateManager.GameState.win:
                levelCleared++;
                textRef1.text = "You Win! \n Level cleared:" + levelCleared + "\n Press Space to Continue";
                text1.SetActive(true);
                lives++; 
                break;
            
            case GameStateManager.GameState.lose:
                text0.SetActive(true);
                levelCleared = 0;
                lives = startlives; 
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("Ball"))                                 
        {
            GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.ready);
            lives--;
            textRef0.text = "Press Space to Start \n \n Lives:" + lives;
        }

        if (lives <= 0)   
        {
            GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.lose);
        }
    }
}
