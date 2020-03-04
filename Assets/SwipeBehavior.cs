using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeBehavior : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease;

    public float SWIPE_THRESHOLD;

    public Vector2 movement;

    public SwipeBehavior()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 0)
        {
            OnNoSwipe();
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        // check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        // check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0)// right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0) // left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    // Callback Functions
    void OnSwipeUp()
    {
        movement.x = 0;
        movement.y = 1;
    }

    void OnSwipeDown()
    {
        movement.x = 0;
        movement.y = -1;
    }

    void OnSwipeLeft()
    {
        movement.x = -1;
        movement.y = 0;
    }

    void OnSwipeRight()
    {
        movement.x = 1;
        movement.y = 0;
    }

    void OnNoSwipe()
    {
        movement.x = 0;
        movement.y = 0;
    }
}