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

    [SerializeField] private GameObject startPointGameObject;
    public float startHeight;
    [SerializeField] private GameObject endPointGameObject;
    public float endHeight;

    public float actualHeight = 0;

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Main != this)
        {
            Destroy(this);
        }
        _life = maxLife;
    }
    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log(gameState);
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
    }
    public void SetTouchInputActive()
    {
        touchInput = true;
        gyroScopeInput = false;
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
