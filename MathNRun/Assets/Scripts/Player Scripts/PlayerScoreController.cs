using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreController : MonoBehaviour
{

    [SerializeField] public Text scoreText;
    [SerializeField] public Text coinText;

    [SerializeField] public Text correctAnswerCountText;
    private string scoreFormat = "00000000";


    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.instance.currentScore = 0;
        GameStateManager.instance.currentCoins = 0;
        GameStateManager.instance.currentCorrectAns = 0;
        scoreText.text = GameStateManager.instance.currentScore.ToString(scoreFormat);
        coinText.text = GameStateManager.instance.currentCoins.ToString();
        correctAnswerCountText.text = GameStateManager.instance.currentCorrectAns.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameplayController.instance.playGame)
        {
            AddScore(1);
        }
    }

    public void AddScore(int increaseBy)
    {
        GameStateManager.instance.currentScore = GameStateManager.instance.currentScore + increaseBy;
        scoreText.text = GameStateManager.instance.currentScore.ToString(scoreFormat);
    }

    public void AddCoinCount(int increaseBy)
    {
        GameStateManager.instance.currentCoins = GameStateManager.instance.currentCoins + increaseBy;
        coinText.text = GameStateManager.instance.currentCoins.ToString();
    }

    public void AddCorrectAnswerCount(int increaseBy)
    {
        GameStateManager.instance.currentCorrectAns = GameStateManager.instance.currentCorrectAns + increaseBy;
        correctAnswerCountText.text = GameStateManager.instance.currentCorrectAns.ToString();
    }
}
