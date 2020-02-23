﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Store the current moving direction
    private string CurrDir;
    // Store the current tile player is on
    private string CurrTile;
    public Dictionary<string, int> Consumables;
    // Start is called before the first frame update
    void Start()
    {
        Consumables = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
