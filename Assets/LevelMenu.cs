using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelButtonContainer;

    public void SelectLevel()
    {
        print("selectLevel");
        SceneManager.LoadScene("SampleScene");

    }

    public void Start()
    {
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/LevelMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        print("len: " + buttons.Length);
        foreach (Button button in buttons)
        {
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(delegate { LoadLevel(int.Parse(text)); });
        }
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene("SampleScene");
        LevelManager.NextLevel = level;
    }
}
