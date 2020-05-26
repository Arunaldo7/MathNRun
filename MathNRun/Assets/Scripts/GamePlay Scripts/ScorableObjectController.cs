using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorableObjectController : MonoBehaviour
{
    private GameObject player;

    private PlayerScoreController playerScoreController;

    [SerializeField] private int count;

    [SerializeField] private int score;


    [SerializeField] AudioClip soundToPlay;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScoreController = player.gameObject.GetComponent<PlayerScoreController>();
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.gameObject.tag == "Player")
        {

            if (gameObject.tag == "Coin Normal")
            {
                playerScoreController.AddCoinCount(count);
                playerScoreController.AddScore(score);
                AudioSource.PlayClipAtPoint(soundToPlay, transform.position);
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Correct Option")
            {
                playerScoreController.AddScore(score);
                AudioSource.PlayClipAtPoint(soundToPlay, transform.position);
                Destroy(gameObject);
            }

        }
    }

}
