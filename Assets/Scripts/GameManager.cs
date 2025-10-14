using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private InputSystem_Actions input = null;
    private int score = 0;

    [SerializeField] private GameObject uIManager;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject gameManagerObj = new GameObject("GameManager");
                    _instance = gameManagerObj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        input = new InputSystem_Actions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public UIManager GetUIManager()
    {
        return uIManager.GetComponent<UIManager>();
    }

    public void UpdateScore(int scoreDelta)
    {
        score += scoreDelta;
        uIManager.GetComponent<UIManager>().UpdateScore(score);

        if (score < -100)
        {
            GameOver();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        uIManager.GetComponent<UIManager>().ActivateGameOverScreen();
    }
}
