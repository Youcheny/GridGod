using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Models;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // movement counter
    public int counter = 0;
    public int stepConstraint = 15;

    public float moveSpeed;

    public Rigidbody2D rb;

    public Vector2 movement;

    public GridManager grid;
    public PlayerStats playerStats;
    Vector2 nextPosition;

    float epsilon = 0.05f; // for float comparison

    // Store the current moving direction
    public string CurrDir;

    public bool IsGameOver = false;
    public bool IsWin = false;

    public float currFrameX;
    public float currFrameY;

    // Swipe Behavior
    public SwipeBehavior swipeBehavior;

    public string gameMessage;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("GridHolder").GetComponent<GridManager>();
        playerStats = new PlayerStats();
        GameObject.Find("Player").transform.position = grid.StartPos;
        nextPosition = GameObject.Find("Player").transform.position;
        currFrameX = nextPosition.x;
        currFrameY = nextPosition.y;
        
        swipeBehavior = FindObjectOfType<SwipeBehavior>();

        // gameMessage = "Collect footprint to gain extra steps";

    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = swipeBehavior.movement.x + Input.GetAxis("Horizontal");
        movement.y = swipeBehavior.movement.y + Input.GetAxis("Vertical");
        // store variables used for movement
        float tileSize = grid.GetTileSize();
        float bottomLimit = -1 * grid.GetRows() * tileSize/2;
        float topLimit = grid.GetRows() * tileSize/2;
        float leftLimit = -1 * grid.GetCols() * tileSize/2;
        float rightLimit = grid.GetCols() * tileSize/2;

        // if on a coin
       
        if (GetConsumable((int)Math.Round(rb.position.x),  (int)Math.Round(rb.position.y)).type == "CoinTile")
        {
            if (playerStats.Consumables.ContainsKey("Coin"))
            {
                playerStats.Consumables["Coin"]++;
            }
            else
            {
                playerStats.Consumables["Coin"] = 1;
            }

            print("Coins: "+  playerStats.Consumables["Coin"]);
            Destroy(GetConsumable((int)Math.Round(rb.position.x), (int)Math.Round(rb.position.y)).consumable);
            GetConsumable((int)Math.Round(rb.position.x),  (int)Math.Round(rb.position.y)).type = "null";
            return;
        }
        // if on a stepAdder
        if (GetConsumable(nextPosition.x, nextPosition.y).type == "StepAdderTile")
        {
            stepConstraint += 5;
            print("constraint: " + stepConstraint);
            Destroy(GetConsumable(nextPosition.x, nextPosition.y).consumable);
            GetConsumable(nextPosition.x, nextPosition.y).type = "null";
            return;
        }

        // if player on ice
        if (GetTile(nextPosition.x, nextPosition.y).type == "IceTile")
        {
            if(CurrDir =="down")
            {
                // if in bound
                if(nextPosition.y - tileSize > bottomLimit) 
                {
                    Tile nextTile = GetTile(nextPosition.x, nextPosition.y-tileSize);
                    if (nextTile.IsPassable())
                    {
                        nextPosition.y -= tileSize;
                        return;
                    }
                }

            }
            else if(CurrDir =="up")
            {
                // if in bound
                if(nextPosition.y + tileSize <= topLimit) 
                {
                    Tile nextTile = GetTile(nextPosition.x, nextPosition.y+tileSize);
                    if (nextTile.IsPassable())
                    {
                        nextPosition.y += tileSize;
                        return;
                    }
                }

            }
            else if(CurrDir =="left")
            {
                // if in bound
                if(nextPosition.x - tileSize >= leftLimit) 
                {
                    Tile nextTile = GetTile(nextPosition.x-tileSize, nextPosition.y);
                    if (nextTile.IsPassable())
                    {
                        nextPosition.x -= tileSize;
                        return;
                    }
                }

            }
            else 
            {
                // if in bound
                if(nextPosition.x + tileSize < rightLimit) 
                {
                    Tile nextTile = GetTile(nextPosition.x+tileSize, nextPosition.y);
                    if (nextTile.IsPassable())
                    {
                        nextPosition.x += tileSize;
                        return;
                    }
                }
            }

        }
        if (GetConsumable(nextPosition.x, nextPosition.y).type == "SpikeTile")
        {
            Destroy(GetConsumable(nextPosition.x, nextPosition.y).consumable);
            GetConsumable(nextPosition.x, nextPosition.y).type = "null";
        }


        // if player on trap, compare if player moved since last frame
        if (GetTile(nextPosition.x, nextPosition.y).type == "TrapTile" )
        {
            if(nextPosition.x != currFrameX || nextPosition.y != currFrameY)
            {
                TrapTile currTrapTile = (TrapTile)(GetTile(nextPosition.x, nextPosition.y));
                if (currTrapTile.IsVulnerable)
                {
                    IsGameOver = true;
                }
                else
                {
                    ((TrapTile)(GetTile(nextPosition.x, nextPosition.y))).IsVulnerable = true;

                }
            }

        }

        //if the player reach the step limit
        if (stepConstraint - counter <= 0)
        {
            print(" is game over, stepConstraint: " + stepConstraint + ", counter: " + counter);
            IsGameOver = true;
        }

        // check whether the player reaches the end
        if (GetTile(nextPosition.x, nextPosition.y).type == "EndTile")
        {
            IsWin = true;
        }

        if (IsWin)
        {
            gameMessage = "You Win!!!";
            return;
        }

        if (IsGameOver)
        {
            gameMessage = "You Lose!!!";
            return;
        }


        currFrameX = nextPosition.x;
        currFrameY = nextPosition.y;

        if ((Math.Abs(movement.x) > epsilon || Math.Abs(movement.y) > epsilon) && PlayerOnNextPosition())
        {


            if (Math.Abs(movement.x) < Math.Abs(movement.y))
            {
                movement.x = 0;
                if (movement.y < 0)
                {
                    rb.rotation = 180;
                    CurrDir = "down";
                    // check bound
                    if(nextPosition.y - tileSize > bottomLimit) 
                    {
                        if(GetTile(nextPosition.x, nextPosition.y - tileSize).IsPassable())
                        {
                            // move down
                            print("next tile: " + GetTile(nextPosition.x, nextPosition.y - tileSize).type);
                            nextPosition.y -= tileSize;
                            IncrementCounter();
                        }
                        
                        
                    }
                }
                else
                {
                    // move up
                    rb.rotation = 0;
                    CurrDir = "up";
                    // check bound
                    if(nextPosition.y + tileSize <= topLimit) 
                    {
    
                        if (GetTile(nextPosition.x, nextPosition.y + tileSize).IsPassable())
                        {
                            print("next tile: " + GetTile(nextPosition.x, nextPosition.y + tileSize).type);
                            nextPosition.y += tileSize;
                            IncrementCounter();
                        }
                            
                    }
                }
            }
            else
            {
                movement.y = 0;
                if (movement.x < 0)
                {
                    rb.rotation = 90;
                    CurrDir = "left";
                    // move left
                    if(nextPosition.x - tileSize >= leftLimit) 
                    {
                        if(GetTile(nextPosition.x - tileSize, nextPosition.y).IsPassable())
                        {
                            print("next tile: " + GetTile(nextPosition.x - tileSize, nextPosition.y).type);
                            nextPosition.x -= tileSize;
                            IncrementCounter();
                        }
                        
                    }
                }
                else
                {
                    // move right
                    rb.rotation = -90;
                    CurrDir = "right";
                    if(nextPosition.x + tileSize < rightLimit) 
                    {
                        if (GetTile(nextPosition.x + tileSize, nextPosition.y).IsPassable())
                        {
                            print("next tile: " + GetTile(nextPosition.x + tileSize, nextPosition.y).type);
                            nextPosition.x += tileSize;
                            IncrementCounter();
                        }
                            
                    }
                }
            }
            print("next pos: " + nextPosition.x + ", " + nextPosition.y);
            
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

    public void IncrementCounter()
    {
        counter+=1;
    }

    public int GetRemainingSteps()
    {
        return stepConstraint - counter;
    }

    private Tile GetTile(float x, float y)
    {
        int row = grid.GetRows() / 2 - (int)y;
        int col = grid.GetCols() / 2 + (int)x;
        //print("In GetTile, "+ "x: " + x + "y: " + y + "; row: " + row + "; col: " + col);
        return grid.GetTiles()[row][col];
    }

    private Consumable GetConsumable(float x, float y)
    {

        int row = grid.GetRows() / 2 - (int)y;
        int col = grid.GetCols() / 2 + (int)x;
        //print("In GetConsumable, "+ "x: " + x + "y: " + y + "; row: " + row + "; col: " + col);
        return grid.Consumables[row][col];
    }



}
