using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float initYPos;
    float xDistance;
    float yDistance;
    float zDistance;

    float desiredX;
    float desiredY;

    float yOffset = 5f;

    private float smoothSpeed = 0.125f;

    [SerializeField] GameObject player;

    void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        initYPos = transform.position.y;
        zDistance = player.transform.position.z - transform.position.z;
        yDistance = (player.transform.position.y - initYPos);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 desiredPos = new Vector3(player.transform.position.x, transform.position.y,
                                         player.transform.position.z - zDistance);

        desiredX = Mathf.Lerp(player.transform.position.x, transform.position.x, smoothSpeed);

        //if in elevated path, camera height follows player height
        //else it is static as initial camera height
        if (player.gameObject.GetComponent<PlayerController>().inPath)
        {
            desiredY = initYPos;
        }
        else
        {
            desiredY = Mathf.Lerp(transform.position.y, player.transform.position.y - yDistance, smoothSpeed);
        }



        transform.position = new Vector3(desiredX, desiredY,
                                         player.transform.position.z - zDistance);
    }

}
