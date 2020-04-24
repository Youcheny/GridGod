using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseStepAdder : MonoBehaviour
{
    public GameObject levelButtonContainer;
    public TextMeshProUGUI gameMessage;

    // Update is called once per frame
    void Update()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/LevelMenu");

        //gameMessage.SetText(movement.gameMessage);
        
        

    }
}
