using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    // Store the current moving direction
    private string CurrDir;
    // Store the current tile player is on
    private string CurrTile;
    public Dictionary<string, int> Consumables = new Dictionary<string, int>();
    // Start is called before the first frame update
  

    public string GetCurrDir() 
    {
        return CurrDir;
    }

    public string GetCurrTile() 
    {
        return CurrTile;
    }

    public void SetCurrDir(string dir)
    {
        CurrDir = dir;
    }

    public void SetCurrTile(string tile)
    {
        CurrTile = tile;
    }
}
