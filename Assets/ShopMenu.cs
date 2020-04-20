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
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1])
            {
                button.interactable = false;
            }
            else
            {
                button.onClick.AddListener(delegate { UnlockLevel(int.Parse(text)); });
            }
        }
    }

    public void Update()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/UnlockMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1])
            {
                button.interactable = false;
            }
        }

    }

    private void UnlockLevel(int level)
    {
    	if (CoinManager.numOfCoins >= 5)
    	{
    		// actually unlock levels
            LevelManager.IsBonusLevelUnlocked[level-1] = true;

    		// deduct coins
    		CoinManager.numOfCoins-=5;
    	}
        else
        {
            print ("coins are not enough");
        }
        
        
    }
}
