using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreController : MonoBehaviour
{

    [SerializeField] public Text scoreText;
    [SerializeField] public Text coinText;
    private string scoreFormat = "00000000";


    private int scoreCount;
    private int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        coinCount = 0;
        scoreText.text = scoreCount.ToString(scoreFormat);
        coinText.text = coinCount.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AddScore(1);
    }

    public void AddScore(int increaseBy)
    {
        scoreCount = scoreCount + increaseBy;
        scoreText.text = scoreCount.ToString(scoreFormat);
    }

    public void AddCoinCount(int increaseBy)
    {
        coinCount = coinCount + increaseBy;
        coinText.text = coinCount.ToString();
    }
}
