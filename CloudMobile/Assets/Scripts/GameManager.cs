using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Main;
    [SerializeField] private int maxLife;
    public int MaxLife { get=> maxLife; set => maxLife = value; }

    private bool gyroScopeInput = true;
    public bool GyroScopeInput { get => gyroScopeInput; set => gyroScopeInput = value; }
    private bool touchInput = false;
    public bool TouchInput { get => touchInput; set => touchInput = value; }

    private GameState gameState;
    public GameState GameState
    {
        get => gameState;
    }

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
                ChangeGameState(GameState.Death);
            }
        }
    }

    private void Awake()
    {
        if (Main == null)
        {
            Debug.Log("this is main");
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Main != this)
        {
            Debug.Log("this isnt main");
            Destroy(this);
        }
        _life = maxLife;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("StartBildschirm").isLoaded)
        {
            gameState = GameState.Menu;
        }
        else gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        gameState = newState;
    }

    public void LoadMenuScene()
    {
        ChangeGameState(GameState.Menu);
        SceneManager.LoadScene(0);
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
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
        Debug.Log(gyroScopeInput);
    }
    public void SetTouchInputActive()
    {
        touchInput = true;
        gyroScopeInput = false;
        Debug.Log(touchInput);
    }

}

public enum GameState
{
    Playing,
    Pause,
    Death,
    LostLive,
    Menu
}
