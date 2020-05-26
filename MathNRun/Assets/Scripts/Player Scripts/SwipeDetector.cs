using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{

    private Vector2 fingerUpPosition;
    private Vector2 fingerDownPosition;

    private bool detectSwipeAfterRel = false;

    private float minSwipeDistance = 10f;

    private PlayerController playerController;

    bool touchMinDistMoved;

    Touch touch;

    void Awake()
    {
        touchMinDistMoved = false;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchMinDistMoved = false;
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (!touchMinDistMoved && SwipeMinDistMet())
        {
            touchMinDistMoved = true;
            if (IsVerticalSwipe())
            {
                if (fingerDownPosition.y > fingerUpPosition.y)
                {
                    playerController.PlayerJump();
                }else{
                    playerController.PlayerDown();
                }
            }
            else
            {
                if (fingerDownPosition.x < fingerUpPosition.x)
                {
                    playerController.MoveLeft();
                }
                else
                {
                    playerController.MoveRight();
                }
            }
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeMinDistMet()
    {
        return VerticalMovementDistance() > minSwipeDistance || HorizontalMovementDistance() > minSwipeDistance;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }
}
