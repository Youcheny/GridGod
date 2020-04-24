using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
	public GameObject levelButtonContainer;
    public Button btn;

    public void Start()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/UnlockMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        print("len: " + buttons.Length);
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1] || CoinManager.numOfCoins < 5)
            {
                button.interactable = false;
            }
            else
            {
                button.onClick.AddListener(delegate { UnlockLevel(int.Parse(text), button); });
            }
        }
        Button stepAdderButton = btn.GetComponent<Button>();
        if (CoinManager.numOfCoins < 5) 
        {
            stepAdderButton.interactable = false;
        }
        else
        {
            stepAdderButton.onClick.AddListener( delegate { AddStepAdder(); });
        }
    }

    public void Update()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/UnlockMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1] || CoinManager.numOfCoins < 5)
            {
                button.interactable = false;
            }
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1])
            {
                var colors = button.colors;
                colors.disabledColor = new Color32(0, 255, 0, 128);
                button.colors = colors;
            }
        }
        Button stepAdderButton = btn.GetComponent<Button>();
        if (CoinManager.numOfCoins < 5) 
        {
            stepAdderButton.interactable = false;
        }

    }

    private void UnlockLevel(int level, Button button)
    {
    	if (CoinManager.numOfCoins >= 5)
    	{
    		// actually unlock levels
            LevelManager.IsBonusLevelUnlocked[level-1] = true;
            levelButtonContainer = GameObject.Find("Main Camera/Canvas/UnlockMenu");
            Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
            var colors = buttons[level-1].colors;
            colors.disabledColor = new Color32(0, 255, 0, 128);
            GameObject.Find("Main Camera/Canvas/UnlockMenu").GetComponentsInChildren<Button>()[level-1].colors= colors;
    		// deduct coins
    		CoinManager.numOfCoins-=5;
    	}
        else
        {
            print ("coins are not enough");
        } 
    }

    private void AddStepAdder()
    {
        if (CoinManager.numOfCoins >= 5)
        {
            // add one stepAdder consumable
            ExtraStepAdder.numOfExtraStepAdder+=1;
            //print("num step adder: " + ExtraStepAdder.numOfExtraStepAdder);
            // deduct coins
            CoinManager.numOfCoins-=5;
        }
    }
}
