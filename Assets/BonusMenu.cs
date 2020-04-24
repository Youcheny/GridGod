using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BonusMenu : MonoBehaviour
{
    public GameObject bonusLevelButtonContainer;

    public void Start()
    {
        bonusLevelButtonContainer = GameObject.Find("Main Camera/Canvas/BonusMenu");
        Button[] buttons = bonusLevelButtonContainer.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            if (LevelManager.IsBonusLevelUnlocked[int.Parse(text)-1])
            {
            	button.interactable = true;
            	button.onClick.AddListener(delegate { LoadLevel(int.Parse(text)); });

            }
        }
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene("SampleScene");
        // TODO: fix level to be the correct level
        LevelManager.NextLevel = level + 9;
    }
}
