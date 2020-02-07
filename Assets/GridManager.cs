using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	private int rows = 8;
	private int cols = 6;
	private float tileSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
