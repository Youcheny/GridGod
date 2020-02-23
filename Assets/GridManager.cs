using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GridManager : MonoBehaviour
{
	private int rows = 8;
	private int cols = 6;
	private float tileSize = 1;

    private List<List<Tile>> Tiles;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new List<List<Tile>>();
        ReadGrid("insfal");
        GenerateTestGrid();
    }

    private void ReadGrid(string filename)
    {

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

    private void GenerateTestGrid()
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

        

        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;
        for (int i = 0; i < rows; ++i)
        {
            List<Tile> tileRow = new List<Tile>();
            for (int j = 0; j < cols; ++j)
            {
                GameObject tile;

                if (i == 0 && j == 0)
                {
                    tile = (GameObject)Instantiate(start.tile, transform);
                    StartTile startTile = new StartTile()
                    {
                        tile = tile,
                        type = "StartTile"
                    };
                    tileRow.Add(startTile);
                }
                else if(i == rows - 1 && j == cols - 1)
                {
                    tile = (GameObject)Instantiate(end.tile, transform);
                    EndTile endTile = new EndTile()
                    {
                        tile = tile,
                        type = "EndTile"
                    };
                    tileRow.Add(endTile);
                }
                else if(i == 3 && j == 3)
                {
                    tile = (GameObject)Instantiate(obstacle.tile, transform);
                    ObstacleTile obstacleTile = new ObstacleTile()
                    {
                        tile = tile,
                        type = "ObstacleTile"
                    };
                    tileRow.Add(obstacleTile);
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
}
