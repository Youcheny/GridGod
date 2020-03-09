using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Threading.Tasks;
public class GridManager : MonoBehaviour
{
	private int rows = 8;
	private int cols = 6;
	private float tileSize = 1;
    
    private List<List<Tile>> Tiles;
    public List<List<Consumable>> Consumables;

    //spike tile for replacement
    public Tile spike = null;
    
    // Start is called before the first frame update
    void Start()
    {
        Tiles = new List<List<Tile>>();
        Consumables = new List<List<Consumable>>();
        //ReadGrid("level1.csv");
        //GenerateTestGrid();
        // GenerateGridFromCSV("Assets/Resources/level1.csv", "level1");

        //GenerateGridFromCSV("Assets/Resources/level2.csv", "level2");
        GenerateGridFromCSV("Assets/Resources/TutorialLevel1.csv", "TutorialLevel1");
        GenerateConsumableFromCSV("Assets/Resources/ConTutorial1.csv", "ConTutorial1");
        //GenerateConsumableFromCSV("Assets/Resources/consumableGrid1.csv", "consumableGrid1");
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
        Tile obstacle = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("ObstacleTile")),
            type = "ObstacleTile"
        };

        Tile normal = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("NormalTile")),
            type = "NormalTile"
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
            tile = (GameObject)Instantiate(Resources.Load("TrapTile")),
            type = "TrapTile"
        };

        Tile ice = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("IceTile")),
            type = "IceTile"
        };






    float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;

        string[,] GridCSV = CsvUtil.readData(filepath, filename);
        rows = GridCSV.GetLength(0);
        cols = GridCSV.GetLength(1);
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
                    tile = (GameObject)Instantiate(obstacle.tile, transform);
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
                    tile = (GameObject)Instantiate(normal.tile, transform);
                    NormalTile normalTile = new NormalTile()
                    {
                        tile = tile,
                        type = "NormalTile"
                    };
                    tileRow.Add(normalTile);
                }

                float posX = j * tileSize + centerOffsetX;
                float posY = i * -tileSize + centerOffsetY;
                tile.transform.position = new Vector2(posX, posY);
            }
            Tiles.Add(tileRow);
        }




        Destroy(obstacle.tile);
        Destroy(normal.tile);
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
            consumable = (GameObject)Instantiate(Resources.Load("SpikeTile")),
            type = "SpikeTile"
        };

        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;

        string[,] GridCSV = CsvUtil.readData(filepath, filename);
        

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
    public void ReloadTrap(int row, int col)
    {
        Tile temp = Tiles[row][col];

        Tiles[row][col] = spike;
    }
}
