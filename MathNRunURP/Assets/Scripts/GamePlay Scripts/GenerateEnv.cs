using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnv : MonoBehaviour
{
    [SerializeField]
    private GameObject otherGround;
    public float groundLength = 130f;

    private float endOffset = 30f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if player has crossed a full ground, just add the ground to front of the next ground
        if ((player.transform.position.z - endOffset )> (transform.position.z + (groundLength / 2)))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = otherGround.transform.position.z + groundLength;

            transform.position = newPosition;
        }
    }
}
