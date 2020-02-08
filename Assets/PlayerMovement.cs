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

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("GridHolder").GetComponent<GridManager>();
        GameObject.Find("Player").transform.position =
            new Vector2(-grid.GetCols() * grid.GetTileSize() / 2,
            grid.GetRows() * grid.GetTileSize() / 2);
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 || movement.y != 0)
        {
            if (Math.Abs(movement.x) < Math.Abs(movement.y))
            {
                movement.x = 0;
                if (movement.y < 0)
                {
                    rb.rotation = 180;
                }
                else
                {
                    rb.rotation = 0;
                }
            }
            else
            {
                movement.y = 0;
                if (movement.x < 0)
                {
                    rb.rotation = 90;
                }
                else
                {
                    rb.rotation = -90;
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
