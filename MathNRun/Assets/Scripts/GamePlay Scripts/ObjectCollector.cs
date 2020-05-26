using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{

    float zDistance;

    [SerializeField] private List<string> collectableObjects;

    [SerializeField] GameObject player;
    void OnTriggerEnter(Collider target)
    {
        //if any of the objects to be collected by object collector, it comes here and gets collected
        if (collectableObjects.IndexOf(target.gameObject.tag) >= 0)
        {
            Destroy(target.gameObject);
        }
    }

    void Start()
    {
        zDistance = player.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         player.transform.position.z - zDistance);
    }
}
