using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorableObjectController : MonoBehaviour
{
     private GameObject player;

    private PlayerScoreController playerScoreController;

    [SerializeField] private int score;

    [SerializeField] private int count;

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
                playerScoreController.AddScore(score);
                if(gameObject.tag == "Coin Normal"){
                    playerScoreController.AddCoinCount(count);
                }
                AudioSource.PlayClipAtPoint(soundToPlay, transform.position);
                Destroy(gameObject);
        }
    }

}
