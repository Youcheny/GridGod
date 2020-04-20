using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
	public GameObject levelButtonContainer;

    public void Start()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/UnlockMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        print("len: " + buttons.Length);
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(delegate { UnlockLevel(int.Parse(text)); });
        }
    }

    private void UnlockLevel(int level)
    {
    	if (CoinManager.numOfCoins >= 5)
    	{
    		// TODO: actually unlock levels

    		// deduct coins
    		CoinManager.numOfCoins-=5;
    	}
        else
        {
            print ("coins are not enough");
        }
        
        
    }
}
