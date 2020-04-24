using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Models;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEditor;
using UnityEngine.UI;

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
    public bool winFlag = false;
    // Swipe Behavior
    public SwipeBehavior swipeBehavior;

    public string gameMessage;
    public int level;

    public bool sentLoseAnalytics = false;

    public int star;

    // animation
    public Animator animator;

    // Start is called before the first frame update

    public bool dialogBoxFlag = false;
    public int dialogBoxCounter = 0;

    public GameMessagePanel gameMessagePanel;

    void Start()
    {
        gameMessagePanel = GameObject.Find("GameMessagePanel").GetComponent<GameMessagePanel>();
        gameMessagePanel.togglePanel(false);

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

        if (!PlayerOnNextPosition())
        {
            return;
        }       

        // if on a coin
        if (/*GridManager*/grid.GetConsumable(rb.position.x,  rb.position.y).type == "CoinTile")
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
            Destroy(grid.GetConsumable(rb.position.x, rb.position.y).consumable);
            grid.GetConsumable(rb.position.x,  rb.position.y).type = "null";
            return;
        }
        // if on a stepAdder
        if (grid.GetConsumable(nextPosition.x, nextPosition.y).type == "StepAdderTile")
        {
            stepConstraint += 5;
            print("constraint: " + stepConstraint);
            Destroy(grid.GetConsumable(nextPosition.x, nextPosition.y).consumable);
            grid.GetConsumable(nextPosition.x, nextPosition.y).type = "null";
            return;
        }

        // if player on ice
        if (/*GridManager*/grid.GetTile(nextPosition.x, nextPosition.y).type == "IceTile")
        {
            if(CurrDir =="down")
            {
                // if in bound
                if(nextPosition.y - tileSize > bottomLimit) 
                {
                    Tile nextTile = grid.GetTile(nextPosition.x, nextPosition.y-tileSize);
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
                    Tile nextTile = grid.GetTile(nextPosition.x, nextPosition.y+tileSize);
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
                    Tile nextTile = grid.GetTile(nextPosition.x-tileSize, nextPosition.y);
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
                    Tile nextTile = grid.GetTile(nextPosition.x+tileSize, nextPosition.y);
                    if (nextTile.IsPassable())
                    {
                        nextPosition.x += tileSize;
                        return;
                    }
                }
            }

        }


        // if player on trap, compare if player moved since last frame
        if (grid.GetTile(nextPosition.x, nextPosition.y).type == "TrapTile" )
        {
            if(nextPosition.x != currFrameX || nextPosition.y != currFrameY)
            {
                TrapTile currTrapTile = (TrapTile)(grid.GetTile(nextPosition.x, nextPosition.y));
                if (currTrapTile.IsVulnerable)
                {
                    IsGameOver = true;
                }
                else
                {
                    ((TrapTile)(grid.GetTile(nextPosition.x, nextPosition.y))).IsVulnerable = true;

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
        if (grid.GetTile(nextPosition.x, nextPosition.y).type == "EndTile")
        {
            IsWin = true;
        }

        if (IsWin)
        {
            // if(dialogBoxFlag && dialogBoxCounter > 30)
            // {
            //     dialogBoxFlag = false;
            //     dialogBoxCounter = 0;
            //     if(EditorUtility.DisplayDialog("You win","Choose your action", "Back to level selection", "Ok" ))
            //     {
            //         print("Pressed back to level selection.");
            //         SceneManager.LoadScene("LevelScene",LoadSceneMode.Additive);
            //         // SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
            //     }
            //     else
            //     {
            //         print("Pressed OK.");
            //     }
            // }
            if (!winFlag)
            {
                star = 1;
                if (playerStats.Consumables["Coin"] > 2)
                {
                    print("coin if");
                    star++;
                }
                if (stepConstraint - counter > 3)
                {
                    print("step if");
                    star++;
                }
                gameMessage = "You Win!!! Rating: You Got " + star + "/3 Stars!!";
                CoinManager.numOfCoins += playerStats.Consumables["Coin"];
                SendWinAnalytics();
                winFlag = true;

                dialogBoxFlag = true;
                dialogBoxCounter++;
            }
            // keep counting frames to wait for enough time to show the dialog box
            dialogBoxCounter++;

            return;
        }

        if (IsGameOver)
        {
            gameMessage = "You Lose!!!";
            // if(dialogBoxFlag && dialogBoxCounter > 30)
            // {
            //     dialogBoxFlag = false;
            //     dialogBoxCounter = 0;
            //     if(EditorUtility.DisplayDialog("You lose","Choose your action", "Back to level selection", "Ok" ))
            //     {
            //         print("Pressed back to level selection.");
            //         SceneManager.LoadScene("LevelScene",LoadSceneMode.Additive);
            //         // SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
            //     }
            //     else
            //     {
            //         print("Pressed OK.");
            //     }
            // }
            if(!sentLoseAnalytics)
            {
                TrialNum.numOfTrial++;
                SendLoseAnalytics();

                CoinManager.numOfCoins -= 5;
                if (CoinManager.numOfCoins < 0)
                {
                    CoinManager.numOfCoins = 0;
                }

                sentLoseAnalytics = true;

                dialogBoxFlag = true;
                dialogBoxCounter++;
            }
            // keep counting frames to wait for enough time to show the dialog box
            dialogBoxCounter++;

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
                    CurrDir = "down";
                    // check bound
                    if(nextPosition.y - tileSize > bottomLimit) 
                    {
                        if(grid.GetTile(nextPosition.x, nextPosition.y - tileSize).IsPassable())
                        {
                            // move down
                            print("next tile: " + grid.GetTile(nextPosition.x, nextPosition.y - tileSize).type);
                            nextPosition.y -= tileSize;
                            IncrementCounter();
                        }
                        if (grid.GetConsumable(rb.position.x, rb.position.y).type == "SpikeTile")
                        {
                            Destroy(grid.GetConsumable(rb.position.x, rb.position.y).consumable);
                            grid.GetConsumable(rb.position.x, rb.position.y).type = "null";
                        }
                    }
                }
                else
                {
                    // move up
                    CurrDir = "up";
                    // check bound
                    if(nextPosition.y + tileSize <= topLimit) 
                    {
    
                        if (grid.GetTile(nextPosition.x, nextPosition.y + tileSize).IsPassable())
                        {
                            print("next tile: " + grid.GetTile(nextPosition.x, nextPosition.y + tileSize).type);
                            nextPosition.y += tileSize;
                            IncrementCounter();
                        }
                        if (grid.GetConsumable(rb.position.x, rb.position.y).type == "SpikeTile")
                        {
                            Destroy(grid.GetConsumable(rb.position.x, rb.position.y).consumable);
                            grid.GetConsumable(rb.position.x, rb.position.y).type = "null";
                        }
                    }
                }
            }
            else
            {
                movement.y = 0;
                if (movement.x < 0)
                {
                    CurrDir = "left";
                    // move left
                    if(nextPosition.x - tileSize >= leftLimit) 
                    {
                        if(grid.GetTile(nextPosition.x - tileSize, nextPosition.y).IsPassable())
                        {
                            print("next tile: " + grid.GetTile(nextPosition.x - tileSize, nextPosition.y).type);
                            nextPosition.x -= tileSize;
                            IncrementCounter();
                        }
                        if (grid.GetConsumable(rb.position.x, rb.position.y).type == "SpikeTile")
                        {
                            Destroy(grid.GetConsumable(rb.position.x, rb.position.y).consumable);
                            grid.GetConsumable(rb.position.x, rb.position.y).type = "null";
                        }
                    }
                }
                else
                {
                    // move right
                    CurrDir = "right";
                    if(nextPosition.x + tileSize < rightLimit) 
                    {
                        if (grid.GetTile(nextPosition.x + tileSize, nextPosition.y).IsPassable())
                        {
                            print("next tile: " + grid.GetTile(nextPosition.x + tileSize, nextPosition.y).type);
                            nextPosition.x += tileSize;
                            IncrementCounter();
                        }
                        if (grid.GetConsumable(rb.position.x, rb.position.y).type == "SpikeTile")
                        {
                            Destroy(grid.GetConsumable(rb.position.x, rb.position.y).consumable);
                            grid.GetConsumable(rb.position.x, rb.position.y).type = "null";
                        }
                    }
                }
            }
            print("next pos: " + nextPosition.x + ", " + nextPosition.y);

        }
        PlayerAnimationUpdate();
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

    void PlayerAnimationUpdate()
    {
        Vector2 dir = movement;
        if (grid.GetTile(rb.position.x, rb.position.y).type != "IceTile")
        {
            dir += nextPosition - rb.position;
        }
        if (dir.sqrMagnitude < epsilon)
            dir = Vector2.zero;
        else dir.Normalize();
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        animator.SetFloat("Speed", dir.sqrMagnitude);
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

    public void SendWinAnalytics()
    {
        print("Sending win analytics level_complete(coin:" + playerStats.Consumables["Coin"] + ", step_left:" + (stepConstraint - counter) + ")");

        AnalyticsResult result = AnalyticsEvent.Custom("level_complete", new Dictionary<string, object>
        {
            { "coin", playerStats.Consumables["Coin"] },
            { "time_elapsed", Time.timeSinceLevelLoad },
            { "step_left", stepConstraint - counter},
            { "num_trial" ,TrialNum.numOfTrial },
            { "keypoint_choice","Detour" },
            { "star", star }
        });
        
        print("result = " + result + " num of trials: " + TrialNum.numOfTrial);

        
        if (result == AnalyticsResult.Ok)
        {
            print("result = True");
            TrialNum.numOfTrial = 0;
        }
        else
        {
            print("result = false");
        }
    }

    public void SendLoseAnalytics()
    {
        print("Sending lose analytics level_incomplete(coin:" + playerStats.Consumables["Coin"] + ")");

        AnalyticsResult result = AnalyticsEvent.Custom("level_incomplete", new Dictionary<string, object>
        {
            { "coin", playerStats.Consumables["Coin"] },
            { "time_elapsed", Time.timeSinceLevelLoad },
            { "keypoint_choice","Toward_Endpoint" }
        });

        print("result = " + result);


        if (result == AnalyticsResult.Ok)
        {
            print("result = True");
        }
        else
        {
            print("result = false");
        }
    }

}
