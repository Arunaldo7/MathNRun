using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuController : MonoBehaviour
{
    public static MainmenuController instance;

    [SerializeField] public Text scoreText;
    [SerializeField] public Text coinText;

    [SerializeField] public Text correctAnswerCountText;

    [SerializeField] public Text potionCountText;
    [SerializeField] public Text magicPotionCountText;
    private string scoreFormat = "00000000";

    [SerializeField] public GameObject mainMenuPanel;
    [SerializeField] public GameObject gameStatePanel;
    [SerializeField] public GameObject lifePotionShopPanel;

    [SerializeField] public GameObject easyPotionShopPanel;

    [SerializeField] public GameObject gemShopPanel;

    [SerializeField] private Animator settingsAnim;

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
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins + GameStateManager.instance.currentCoins;
        GameStateManager.instance.totalCorrectAns = GameStateManager.instance.totalCorrectAns + GameStateManager.instance.currentCorrectAns;
        if (GameStateManager.instance.currentScore > GameStateManager.instance.highScore)
        {
            GameStateManager.instance.highScore = GameStateManager.instance.currentScore;
        }
        if (GameStateManager.instance.currentCorrectAns > GameStateManager.instance.highCorrectAns)
        {
            GameStateManager.instance.highCorrectAns = GameStateManager.instance.currentCorrectAns;
        }
        DisplayGameState();
    }

    public void DisplayGameState()
    {
        scoreText.text = GameStateManager.instance.highScore.ToString(scoreFormat);
        coinText.text = GameStateManager.instance.totalCoins.ToString();
        correctAnswerCountText.text = GameStateManager.instance.totalCorrectAns.ToString();
        potionCountText.text = GameStateManager.instance.potionCount.ToString();
        magicPotionCountText.text = GameStateManager.instance.magicPotionCount.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame()
    {
        GameStateManager.instance.SaveData();
        Application.Quit();
    }

    public void ShowHideSettings()
    {
        settingsAnim.SetBool("SlideUp", !settingsAnim.GetBool("SlideUp"));
    }

    public void ShowLifePotionShopPanel()
    {
        Debug.Log("show panel");
        lifePotionShopPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideLifePotionShopPanel()
    {
        lifePotionShopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ShowEasyPotionShopPanel()
    {
        easyPotionShopPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideEasyPotionShopPanel()
    {
        easyPotionShopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ShowGemPotionShopPanel()
    {
        gemShopPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideGemPotionShopPanel()
    {
        gemShopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

}
