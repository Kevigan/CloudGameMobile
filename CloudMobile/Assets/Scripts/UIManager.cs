using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Main;

    [SerializeField] private GameObject HUDpanel, PausePanel, DeathPanel, LevelFinishedPanel;
    [SerializeField] private GameObject touchFields;
    public GameObject TouchFields { get => touchFields; set => touchFields = value; }
    public int numOfHearts;
    public Image[] hearts;

    [Header("Score")]
    public Text scoreText;

    // public Sprite fullHeart;

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else if (Main != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHearts();
        if (GameManager.Main.TouchInput) TouchFields.SetActive(true);
        else TouchFields.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeUIState(UIState newState)
    {
        DeactivateAllPanels();
        switch (newState)
        {
            case UIState.HUD:
                HUDpanel.SetActive(true);
                break;
            case UIState.Pause:
                PausePanel.SetActive(true);
                break;
            case UIState.Death:
                DeathPanel.SetActive(true);
                break;
            case UIState.LevelFinished:
                LevelFinishedPanel.SetActive(true);
                break;

        }
    }

    private void DeactivateAllPanels()
    {
        HUDpanel.SetActive(false);
        PausePanel.SetActive(false);
        DeathPanel.SetActive(false);
    }

    public void ActivatePausePanel()
    {
        GameManager.Main.ChangeGameState(GameState.Pause);
    }
    public void ActivateHUDPanel()
    {
        GameManager.Main.ChangeGameState(GameState.Playing);
    }
    public void LoadMenuScene()
    {
        GameManager.Main.LoadMenuScene();
    }

    public void SetHearts()
    {
        numOfHearts = GameManager.Main.Life;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                if (hearts[i].enabled == true)
                {
                    UIParticlesManager.Main.heartParticles[i].Play();
                }
                hearts[i].enabled = false;

            }
        }
    }
}

public enum UIState
{
    HUD,
    Pause,
    Death,
    Menu,
    LevelFinished
}
