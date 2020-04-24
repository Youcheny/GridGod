using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelButtonContainer;
    //public UseStepAdder gameMessagePanel;
    public GameObject panel;
    public Button yesButton;
    public Button noButton;

    public void togglePanel(bool on, int level)
    {
        if (on)
        {
            panel.gameObject.SetActive(true);
            yesButton.onClick.AddListener(delegate { useStepAdder(level); });
            noButton.onClick.AddListener(delegate { notUseStepAdder(level); });

        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }
    public void useStepAdder(int level)
    {
        ExtraStepAdder.numOfExtraStepAdder -= 1;
        IfUseStepAdder.useOrNot = true;
        SceneManager.LoadScene("SampleScene");
        LevelManager.NextLevel = level;

    }
    public void notUseStepAdder(int level)
    {
        //ExtraStepAdder.numOfExtraStepAdder -= 1;
        IfUseStepAdder.useOrNot = false;
        SceneManager.LoadScene("SampleScene");
        LevelManager.NextLevel = level;
    }
    public void Start()
    {
        togglePanel(false, 0);
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
        if (ExtraStepAdder.numOfExtraStepAdder > 0)
        {
            togglePanel(true, level);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
            LevelManager.NextLevel = level;

        }

        
        
    }
}
