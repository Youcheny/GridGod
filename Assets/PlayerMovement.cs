using System.Collections;
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
        if ((Math.Abs(movement.x) > epsilon || Math.Abs(movement.y) > epsilon) && PlayerOnNextPosition())
        {
            if (Math.Abs(movement.x) < Math.Abs(movement.y))
            {
                movement.x = 0;
                if (movement.y < 0)
                {
                    // move down
                    nextPosition.y -= grid.GetTileSize();
                    rb.rotation = 180;
                }
                else
                {
                    // move up
                    nextPosition.y += grid.GetTileSize();
                    rb.rotation = 0;
                }
            }
            else
            {
                movement.y = 0;
                if (movement.x < 0)
                {
                    // move left
                    nextPosition.x -= grid.GetTileSize();
                    rb.rotation = 90;
                }
                else
                {
                    // move right
                    nextPosition.x += grid.GetTileSize();
                    rb.rotation = -90;
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
