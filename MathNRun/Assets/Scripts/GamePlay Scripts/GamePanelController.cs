using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePanelController : MonoBehaviour
{

    public static GamePanelController instance;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HideGameOverPanel();
        HidePausePanel();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ShowPausePanel()
    {
        if (!gameOverPanel.gameObject.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        HidePausePanel();
        Time.timeScale = 1;
    }


    public void LoadMainMenu()
    {
        AdManager.instance.ShowFullScrAd();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
