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
                text1.SetActive(true);
                lives = startlives; 
                break;
            
            case GameStateManager.GameState.lose:
                text0.SetActive(true);
                lives = startlives; 
                break;
            
            default: break;
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
        
        
        if (lives <= 0)                                                         //Restart the Game when out of Lives
        {
            GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.lose);
        }
    }
}
