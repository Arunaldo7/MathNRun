using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuController : MonoBehaviour
{

    [SerializeField] public Text scoreText;
    [SerializeField] public Text coinText;

    [SerializeField] public Text correctAnswerCountText;
    private string scoreFormat = "00000000";


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



        scoreText.text = GameStateManager.instance.highScore.ToString(scoreFormat);
        coinText.text = GameStateManager.instance.totalCoins.ToString();
        correctAnswerCountText.text = GameStateManager.instance.totalCorrectAns.ToString();
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
}
