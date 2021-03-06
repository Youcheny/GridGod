﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMessage : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI gameMessage;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement movement = player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        
        if(movement.gameMessage != "")
        {
            gameMessage.SetText(movement.gameMessage);
            movement.gameMessagePanel.togglePanel(true);
        }
        
    }
}
