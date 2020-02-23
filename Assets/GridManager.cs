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
    	for (int i = 0; i < rows; ++i) {
    		for (int j = 0; j < cols; ++j) {
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
            tile = (GameObject)Instantiate(Resources.Load("Tile")),
            type = "Tile"
        };

        Tile start = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("TrapTile")),
            type = "TrapTile"
        };

        Tile end = new Tile()
        {
            tile = (GameObject)Instantiate(Resources.Load("TrapTileActivated")),
            type = "TrapTileActivated"
        };

        

        float centerOffsetX = -cols * tileSize / 2; // center
        float centerOffsetY = rows * tileSize / 2;
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                GameObject tile;

                if (i == 0 && j == 0)
                {
                    tile = (GameObject)Instantiate(start.tile, transform);
                    
                }
                else if(i == rows - 1 && j == cols - 1)
                {
                    tile = (GameObject)Instantiate(end.tile, transform);
                }
                else if(i == 3 && j == 3)
                {
                    tile = (GameObject)Instantiate(obstacle.tile, transform);
                }
                else
                {
                    tile = (GameObject)Instantiate(normal.tile, transform);
                }

                float posX = j * tileSize + centerOffsetX;
                float posY = i * -tileSize + centerOffsetY;
                tile.transform.position = new Vector2(posX, posY);

            }
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
}
