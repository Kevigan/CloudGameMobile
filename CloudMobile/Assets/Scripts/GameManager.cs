using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Main;
    [SerializeField] private int maxLife;
    public int MaxLife { get => maxLife; set => maxLife = value; }

    private bool gyroScopeInput = true;
    public bool GyroScopeInput { get => gyroScopeInput; set => gyroScopeInput = value; }
    private bool touchInput = false;
    public bool TouchInput { get => touchInput; set => touchInput = value; }

    private int actualHighScore = 0;
    public int ActualHighScore { get => actualHighScore; set { actualHighScore = value; } }
    [HideInInspector]
    public int HighScore;

    private GameState gameState;
    public GameState GameState
    {
        get => gameState;
    }

    [SerializeField] private PlayerCharacter2D player;

    private int _life;
    public int Life
    {
        get => _life;
        set
        {
            _life = value;
            UIManager.Main.SetHearts();
            if (Life <= 0)
            {
                SaveHighScore();
                ChangeGameState(GameState.Death);
            }
        }
    }

    [SerializeField] private GameObject startPointGameObject;
    public float startHeight;
    [SerializeField] private GameObject endPointGameObject;
    public float endHeight;

    public float actualHeight = 0;
    private float activateWindHeight = 25;
    [HideInInspector]
    public float highestHeight = 0;
    //[HideInInspector]
    public float _highestHeight = 0;

    public delegate void OnHeightReached();
    public static event OnHeightReached activateWindField;

    public int windDirection;

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Main != this)
        {
            Destroy(gameObject);
        }
        _life = maxLife;
    }
    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("Car");
        startPointGameObject.transform.position = new Vector3(startPointGameObject.transform.position.x, startHeight);
        endPointGameObject.transform.position = new Vector3(endPointGameObject.transform.position.x, endHeight);
        if (SceneManager.GetSceneByName("StartBildschirm").isLoaded)
        {
            gameState = GameState.Menu;
        }
        else gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (highestHeight > _highestHeight) _highestHeight = highestHeight;
        if (actualHeight > activateWindHeight)
        {
            int[] a = { -1, 1 };
            int i = Random.Range(0,2);
            windDirection = a[i];
            activateWindField.Invoke();
            activateWindHeight += 75;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        Time.timeScale = 1;
        switch (newState)
        {
            case GameState.Playing:
                UIManager.Main.ChangeUIState(UIState.HUD);
                break;
            case GameState.Pause:
                UIManager.Main.ChangeUIState(UIState.Pause);
                Time.timeScale = 0;
                break;
            case GameState.Death:
                UIManager.Main.ChangeUIState(UIState.Death);
                break;
            case GameState.LostLive:
                break;
            case GameState.Menu:
                break;
            case GameState.LevelFinished:
                UIManager.Main.ChangeUIState(UIState.LevelFinished);
                break;
        }
        gameState = newState;
    }

    public void UpdateScore()
    {
        UIManager.Main.scoreText.text = "Score: " + actualHighScore.ToString();
    }

    public void ResetValues()
    {
        GameManager.Main.activateWindHeight = 25;
        GameManager.Main.ActualHighScore = 0;
        GameManager.Main.highestHeight = 0;
        GameManager.Main._highestHeight = 0;
        GameManager.Main.actualHeight = 0;
    }

    public void LoadMenuScene()
    {
        SaveHighScore();
        ChangeGameState(GameState.Menu);
        SceneManager.LoadScene(0);
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
        ResetValues();
        ChangeGameState(GameState.Playing);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetGyroscopeInputActive()
    {
        gyroScopeInput = true;
        touchInput = false;
    }
    public void SetTouchInputActive()
    {
        touchInput = true;
        gyroScopeInput = false;
    }

    public void SaveHighScore()
    {
            
        if (GameManager.Main.ActualHighScore > GameManager.Main.HighScore)
        {
            HighScore = ActualHighScore;
            PlayerPrefs.SetInt("Car", GameManager.Main.HighScore);
            PlayerPrefs.Save();
           
        }
    }
}

public enum GameState
{
    Playing,
    Pause,
    Death,
    LostLive,
    Menu,
    LevelFinished
}
