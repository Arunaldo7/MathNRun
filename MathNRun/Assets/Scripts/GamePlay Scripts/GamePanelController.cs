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
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject easyQuestionButton;
    [SerializeField] private Text countdownTimerText;

    [SerializeField] public Text lifePotionCountText;


    [SerializeField] public Text easyQuestionPotionText;
    [SerializeField] public Text easyQuestionCountText;

    [SerializeField] public GameObject question;

    private int countdownTimer;

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

    void Update()
    {
        //when question is displayed, pause button will not show up
        if (question.gameObject.activeInHierarchy)
        {
            pauseButton.SetActive(false);
        }
        else
        {
            pauseButton.SetActive(true);
        }

        easyQuestionCountText.text = GameplayController.instance.noOfEasyQns.ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        TogglePauseButton(false);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        TogglePauseButton(true);
    }

    public void TogglePauseButton(bool active)
    {
        pauseButton.SetActive(active);
    }

    public void ShowPausePanel()
    {
        if (!gameOverPanel.gameObject.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            GameplayController.instance.playGame = false;
            GameplayController.instance.StopCoroutines();
            PlayerController.instance.playerBody.useGravity = false;
            PlayerController.instance.playerBody.isKinematic = true;
        }
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        HidePausePanel();
        HideGameOverPanel();
        StartCoundownTimer();
    }

    public void StartCoundownTimer()
    {
        countdownPanel.SetActive(true);
        countdownTimer = 3;
        ShowCountDown();
    }

    private void ShowCountDown()
    {
        countdownTimerText.text = countdownTimer.ToString();
        if (countdownTimer > 0)
        {
            countdownTimer--;
            Invoke("ShowCountDown", 1f);
        }
        else
        {
            countdownPanel.SetActive(false);
            GameplayController.instance.playGame = true;
            GameplayController.instance.StartCoroutines();
            PlayerController.instance.playerBody.useGravity = true;
            PlayerController.instance.playerBody.isKinematic = false;
        }
    }

    public void UsePotion()
    {
        GameStateManager.instance.potionCount = GameStateManager.instance.potionCount - 1;
        lifePotionCountText.text = GameStateManager.instance.potionCount.ToString();
        GameplayController.instance.DestroyNearObjects();
        ResumeGame();
    }

    public void UseEasyQnPotion()
    {
        GameStateManager.instance.magicPotionCount = GameStateManager.instance.magicPotionCount - 1;
        easyQuestionPotionText.text = GameStateManager.instance.magicPotionCount.ToString();
        GameplayController.instance.noOfEasyQns = GameplayController.instance.noOfEasyQns + 5;
    }

    public void LoadMainMenu()
    {
        GameStateManager.instance.SaveData();
        AdManager.instance.ShowFullScrAd();
        SceneManager.LoadScene("MainMenu");
        GameplayController.instance.playGame = true;
    }

    public void QuitGame()
    {
        GameStateManager.instance.SaveData();
        Application.Quit();
    }
}
