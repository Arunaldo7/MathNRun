using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float acceleration;
    [SerializeField] public float frontSpeed;

    [SerializeField] public float maxFrontSpeed;
    [SerializeField] public float jumpSpeed;

    [SerializeField] private Transform[] lanes;

    [SerializeField] public ParticleSystem dust;


    private float lerpTime = 0.25f;

    private float lerpStartTime;

    private float percentCompleted;

    private bool movePlayerSideways;

    private bool movePlayerDown;

    private Vector3 startPos;

    private Vector3 endPos;

    private Rigidbody playerBody;

    private float initPlayerYPos;

    //used to animate player
    public Animator anim;

    private int currentLane;

    public bool inPath;

    float timeSinceStarted;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentLane = 1;
        inPath = true;

        playerBody = GetComponent<Rigidbody>();

        initPlayerYPos = transform.position.y;

        movePlayerSideways = false;
        movePlayerDown = false;

        // MovePlayer();
    }


    void Update()
    {
        ControlPlayer();
    }

    void FixedUpdate()
    {
        //always move player position in front based upon speed
        if (frontSpeed <= maxFrontSpeed)
        {
            frontSpeed = frontSpeed + (Time.deltaTime * acceleration);
        }
        Vector3 playerPos = transform.position;
        playerPos = playerPos + (Vector3.forward * (frontSpeed * Time.deltaTime));
        

        transform.position = playerPos;

        if (movePlayerSideways || movePlayerDown)
        {
            timeSinceStarted = Time.time - lerpStartTime;
            percentCompleted = timeSinceStarted / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, percentCompleted);

            //if 100% movement completed, then reset movePlayerSideways to false
            if (percentCompleted >= 1f && movePlayerSideways)
            {
                movePlayerSideways = false;
            }

            if (percentCompleted >= 1f && movePlayerDown)
            {
                movePlayerDown = false;
            }
        }
    }

    public void MovePlayer()
    {

        startPos = transform.position;

        endPos = transform.position;
        endPos.x = lanes[currentLane].position.x;
        endPos.z = endPos.z + (frontSpeed * lerpTime);

        lerpStartTime = Time.time;
        movePlayerSideways = true;
    }


    public void MoveRight()
    {
        if (currentLane != 2)
        {
            currentLane++;
            MovePlayer();
        }
    }

    public void MoveLeft()
    {
        if (currentLane != 0)
        {
            currentLane--;
            MovePlayer();
        }
    }

    public void PlayerJump()
    {
        if (anim.GetBool("run"))
        {
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            playerBody.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f), ForceMode.Impulse);

            dust.Play();
        }
    }
    public void PlayerDown()
    {
        startPos = transform.position;
        endPos = transform.position;
        endPos.y = initPlayerYPos;

        endPos.z = endPos.z + (frontSpeed * lerpTime);

        lerpStartTime = Time.time;
        movePlayerDown = true;
    }

    void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerDown();
        }

    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Obstacle")
        {
            GamePanelController.instance.ShowGameOverPanel();
            Time.timeScale = 0;
        }
    }

}
