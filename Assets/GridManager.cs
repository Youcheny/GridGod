﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Threading.Tasks;
public class GridManager : MonoBehaviour
{
	private int rows;
	private int cols;
	private float tileSize = 1;
    
    private List<List<Tile>> Tiles;
    public List<List<Consumable>> Consumables;

    //spike tile for replacement
    public Tile spike = null;

    public Vector2 StartPos;
    
    // Start is called before the first frame update
    void Start()
    {
        Tiles = new List<List<Tile>>();
        Consumables = new List<List<Consumable>>();
        int level = LevelManager.NextLevel;
        string levelString = level.ToString();
        print("level: " + levelString);
        
        if(levelString == "0")
        {
            levelString = "1";
        }
        //print(level);
        GenerateGridFromCSV("Assets/Resources/level" + levelString + ".csv", "level" + levelString);
        GenerateConsumableFromCSV("Assets/Resources/consumable" + levelString + ".csv", "consumable" + levelString);
       
        // GenerateGridFromCSV("Assets/Resources/level2.csv", "level2");

        // Tutorial Levels

        // GenerateGridFromCSV("Assets/Resources/TutorialLevel1.csv", "TutorialLevel1");
        // GenerateConsumableFromCSV("Assets/Resources/ConTutorial1.csv", "ConTutorial1");

        // GenerateGridFromCSV("Assets/Resources/TutorialLevel2.csv", "TutorialLevel2");
        // GenerateConsumableFromCSV("Assets/Resources/ConTutorial2.csv", "ConTutorial2");

        //GenerateGridFromCSV("Assets/Resources/TutorialLevel3.csv", "TutorialLevel3");
        //GenerateConsumableFromCSV("Assets/Resources/ConTutorial3.csv", "ConTutorial3");

        //GenerateGridFromCSV("Assets/Resources/TutorialLevel4.csv", "TutorialLevel4");
        //GenerateConsumableFromCSV("Assets/Resources/ConTutorial4.csv", "ConTutorial4");
    }

    // unused
    private string[,] ReadGrid(string filename)
    {
        return  CsvUtil.readData("Assets/Resources/level1.csv", "level1");
    }

    private void GenerateGrid() {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Tile"));
        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;
        for (int i = 0; i < rows; ++i){
            for (int j = 0; j < cols; ++j){
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                float posX = j * tileSize + centerOffsetX;
                float posY = i * -tileSize + centerOffsetY;
                tile.transform.position = new Vector2(posX, posY);
            }
        }
        Destroy(referenceTile);
    }
    private void GenerateGridFromCSV(string filepath, string filename)
    {
        Tile[] obstacleTiles = {
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-481")),
                type = "ObstacleTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-484")),
                type = "ObstacleTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-487")),
                type = "ObstacleTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-489")),
                type = "ObstacleTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-554")),
                type = "ObstacleTile"
            }
        };

        Tile[] normalTiles = {
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-105")),
                type = "NormalTile"
            }, // uncracked
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-81")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-82")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-83")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-103")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-104")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-292")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-293")),
                type = "NormalTile"
            },
            new Tile()
            {
                tile = (GameObject)Instantiate(Resources.Load("tileset-294")),
                type = "NormalTile"
            }
        };

        Tile start = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("StartTile")),
            type = "StartTile"
        };

        Tile end = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("EndTile")),
            type = "EndTile"
        };

        Tile trap = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("SpikeTile")),
            type = "TrapTile"
        };

        Tile ice = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("IceTile")),
            type = "IceTile"
        };


        string[,] GridCSV = CsvUtil.readData(filepath, filename);
        rows = GridCSV.GetLength(0);
        cols = GridCSV.GetLength(1);

        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;
        for (int i = 0; i < GridCSV.GetLength(0); i++)
        {
            List<Tile> tileRow = new List<Tile>();

            for (int j = 0; j < GridCSV.GetLength(1); j++)
            {
                GameObject tile;
                if (GridCSV[i,j] == "S")
                {
                    tile = (GameObject)Instantiate(start.tile, transform);
                    StartTile startTile = new StartTile()
                    {
                        tile = tile,
                        type = "StartTile"
                    };
                    tileRow.Add(startTile);
                }
                else if (GridCSV[i, j] == "E")
                {
                    tile = (GameObject)Instantiate(end.tile, transform);
                    EndTile endTile = new EndTile()
                    {
                        tile = tile,
                        type = "EndTile"
                    };
                    tileRow.Add(endTile);
                }
                else if (GridCSV[i, j] == "W") //W = wallobstacle
                {
                    int randomChoice = (int)Random.Range(0, obstacleTiles.Length - 0.001f);
                    tile = (GameObject)Instantiate(obstacleTiles[randomChoice].tile, transform);
                    ObstacleTile obstacleTile = new ObstacleTile()
                    {
                        tile = tile,
                        type = "ObstacleTile"
                    };
                    tileRow.Add(obstacleTile);
                }
                else if (GridCSV[i, j] == "T")
                {
                    tile = (GameObject)Instantiate(trap.tile, transform);
                    TrapTile trapTile = new TrapTile()
                    {
                        tile = tile,
                        type = "TrapTile"
                    };
                    tileRow.Add(trapTile);
                }
                else if (GridCSV[i, j] == "I")
                {
                    tile = (GameObject)Instantiate(ice.tile, transform);
                    IceTile iceTile = new IceTile()
                    {
                        tile = tile,
                        type = "IceTile"
                    };
                    tileRow.Add(iceTile);
                }
                else
                {
                    int randomChoice = (int)Random.Range(0, normalTiles.Length - 0.001f);
                    tile = (GameObject)Instantiate(normalTiles[randomChoice].tile, transform);
                    NormalTile normalTile = new NormalTile()
                    {
                        tile = tile,
                        type = "NormalTile"
                    };
                    tileRow.Add(normalTile);
                }

                float posX = j * tileSize + centerOffsetX;
                float posY = i * -tileSize + centerOffsetY;
                if (GridCSV[i,j] == "S")
                {
                    StartPos = new Vector2(posX, posY);
                }
                tile.transform.position = new Vector2(posX, posY);
            }
            Tiles.Add(tileRow);
        }
        
        foreach (Tile obstacleTile in obstacleTiles)
        {
            Destroy(obstacleTile.tile);
        }
        foreach (Tile normalTile in normalTiles)
        {
            Destroy(normalTile.tile);
        }
        Destroy(start.tile);
        Destroy(end.tile);
        Destroy(trap.tile);
        Destroy(ice.tile);
        
        //bool x = await Task.FromResult(false);
        
    }



    private void GenerateConsumableFromCSV(string filepath, string filename)
    {
        print("entering");
        Consumable coin = new Consumable()
        {
            consumable = (GameObject)Instantiate(Resources.Load("Coin")),
            type = "CoinTile"
        };
        Consumable transparent = new Consumable()
        {
            consumable = (GameObject)Instantiate(Resources.Load("TransparentTile")),
            type = "TransparentTile"
        };
        Consumable stepAdder = new Consumable()
        {
            consumable = (GameObject)Instantiate(Resources.Load("StepAdder")),
            type = "StepAdderTile"
        };
        Consumable spike = new Consumable()
        {
            consumable = (GameObject)Instantiate(Resources.Load("TrapTile")),
            type = "SpikeTile"
        };

        string[,] GridCSV = CsvUtil.readData(filepath, filename);
        rows = GridCSV.GetLength(0);
        cols = GridCSV.GetLength(1);

        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;
        

        for (int i = 0; i < GridCSV.GetLength(0); i++)
        {
           
            List<Consumable> consumableRow = new List<Consumable>();

            for (int j = 0; j < GridCSV.GetLength(1); j++)
            {
               
                GameObject consumable;
                
                if (GridCSV[i, j] == "C")
                {
                    consumable = (GameObject)Instantiate(coin.consumable, transform);
                    Coin coinTile = new Coin()
                    {
                        consumable = consumable,
                        type = "CoinTile"
                    };
                    consumableRow.Add(coinTile);
                }
                else if (GridCSV[i, j] == "S")
                {
                    consumable = (GameObject)Instantiate(stepAdder.consumable, transform);
                    StepAdder stepAdderTile = new StepAdder()
                    {
                        consumable = consumable,
                        type = "StepAdderTile"
                    };
                    consumableRow.Add(stepAdderTile);
                }
                else if (GridCSV[i, j] == "T")
                {
                    consumable = (GameObject)Instantiate(spike.consumable, transform);
                    SpikeTile spikeTile = new SpikeTile()
                    {
                        consumable = consumable,
                        type = "SpikeTile"
                    };
                    consumableRow.Add(spikeTile);
                }
                else 
                {
                    consumable = (GameObject)Instantiate(transparent.consumable, transform);
                    TransparentTile transparentTile = new TransparentTile()
                    {
                        consumable = consumable,
                        type = "TransparentTile"
                    };
                    consumableRow.Add(transparentTile);
                }

                float posX = j * tileSize + centerOffsetX;
                float posY = i * -tileSize + centerOffsetY;
                
                consumable.transform.position = new Vector2(posX, posY);
            }
            
            Consumables.Add(consumableRow);
        }




        
        Destroy(coin.consumable);
        Destroy(transparent.consumable);
        Destroy(stepAdder.consumable);
        Destroy(spike.consumable);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRows()
    {
        return rows;
    }

    public int GetCols()
    {
        return cols;
    }

    public float GetTileSize()
    {
        return tileSize;
    }

    public List<List<Tile>> GetTiles()
    {
        return Tiles;
    }

    public void ReloadTrap(float x, float y)
    {
        int row = (int)Mathf.Round(GetRows() / 2.0f - y);
        int col = (int)Mathf.Round(GetCols() / 2.0f + x);
        Tile temp = Tiles[row][col];

        Tiles[row][col] = spike;
    }
    public Tile GetTile(float x, float y)
    {
        int row = (int)Mathf.Round(GetRows() / 2.0f - y);
        int col = (int)Mathf.Round(GetCols() / 2.0f + x);
        //print("In GetTile, " + "x: " + x + "y: " + y + "; row: " + row + "; col: " + col);
        return GetTiles()[row][col];
    }

    public Consumable GetConsumable(float x, float y)
    {
        int row = (int)Mathf.Round(GetRows() / 2.0f - y);
        int col = (int)Mathf.Round(GetCols() / 2.0f + x);
        //print(row + " " + col)
        print("In GetConsumable, " + "x: " + x + ", y: " + y + "; row: " + row + "; col: " + col);
        return Consumables[row][col];
    }
}
