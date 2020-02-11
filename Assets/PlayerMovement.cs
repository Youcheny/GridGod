﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    Vector2 movement;

    public GridManager grid;

    Vector2 nextPosition;

    float epsilon = 0.001f; // for float comparison

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("GridHolder").GetComponent<GridManager>();
        GameObject.Find("Player").transform.position =
            new Vector2(-grid.GetCols() * grid.GetTileSize() / 2,
            grid.GetRows() * grid.GetTileSize() / 2);
        nextPosition = GameObject.Find("Player").transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // store variables used for movement
        float tileSize = grid.GetTileSize();
        float bottomLimit = -1 * grid.GetRows() * tileSize/2;
        float topLimit = grid.GetRows() * tileSize/2;
        float leftLimit = -1 * grid.GetCols() * tileSize/2;
        float rightLimit = grid.GetCols() * tileSize/2;

        if ((Math.Abs(movement.x) > epsilon || Math.Abs(movement.y) > epsilon) && PlayerOnNextPosition())
        {
            if (Math.Abs(movement.x) < Math.Abs(movement.y))
            {
                movement.x = 0;
                if (movement.y < 0)
                {
                    rb.rotation = 180;
                    // check bound
                    if(nextPosition.y - tileSize > bottomLimit) 
                    {
                        // move down
                        nextPosition.y -= tileSize;
                        
                    }
                }
                else
                {
                    // move up
                    rb.rotation = 0;
                    // check bound
                    if(nextPosition.y + tileSize <= topLimit) 
                    {
                        nextPosition.y += tileSize;
                    }
                }
            }
            else
            {
                movement.y = 0;
                if (movement.x < 0)
                {
                    rb.rotation = 90;
                    // move left
                    if(nextPosition.x - tileSize >= leftLimit) 
                    {
                        nextPosition.x -= tileSize;
                    }
                }
                else
                {
                    // move right
                    rb.rotation = -90;
                    if(nextPosition.x + tileSize < rightLimit) 
                    {
                        nextPosition.x += tileSize;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!PlayerOnNextPosition())
        {
            Vector2 dir = nextPosition - rb.position;
            dir.Normalize();
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    bool PlayerOnNextPosition()
    {
        return Math.Abs(rb.position.x - nextPosition.x) < epsilon && Math.Abs(rb.position.y - nextPosition.y) < epsilon;
    }
}
