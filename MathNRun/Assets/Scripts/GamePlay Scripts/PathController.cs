using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

    Animator anim;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            anim.SetBool("run", true);
            anim.SetBool("jump", false);
            target.gameObject.GetComponent<PlayerController>().dust.Play();
            Debug.Log(gameObject.tag);
            if (gameObject.tag == "Path")
            {
                target.gameObject.GetComponent<PlayerController>().inPath = true;
            }
            else
            {
                target.gameObject.GetComponent<PlayerController>().inPath = false;
            }
        }
    }
}
