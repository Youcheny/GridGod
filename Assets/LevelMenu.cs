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
    public void SelectLevel()
    {
        print("selectLevel");
        SceneManager.LoadScene("SampleScene");

    }
    public void togglePanel(bool on, int level, bool isBonus)
    {
        if (on)
        {
            panel.gameObject.SetActive(true);
            yesButton.onClick.AddListener(delegate { useStepAdder(level, isBonus); });
            noButton.onClick.AddListener(delegate { notUseStepAdder(level, isBonus); });

        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }
    public void useStepAdder(int level, bool isBonus)
    {
        ExtraStepAdder.numOfExtraStepAdder -= 1;
        IfUseStepAdder.useOrNot = true;
        SceneManager.LoadScene("SampleScene");
        if(isBonus)
        LevelManager.NextLevel = level + 9;
        else
            LevelManager.NextLevel = level;
    }
    public void notUseStepAdder(int level, bool isBonus)
    {
        //ExtraStepAdder.numOfExtraStepAdder -= 1;
        IfUseStepAdder.useOrNot = false;
        SceneManager.LoadScene("SampleScene");
        if (isBonus)
            LevelManager.NextLevel = level + 9;
        else
            LevelManager.NextLevel = level;
    }
    public void Start()
    {
        togglePanel(false, 0, false);
        levelButtonContainer = GameObject.Find("Main Camera/Canvas/LevelMenu");
        Button[] buttons = levelButtonContainer.GetComponentsInChildren<Button>();
        print("len: " + buttons.Length);
        int Counter = 0;
        foreach (Button button in buttons)
        {
            Counter+=1;

            bool isBonus = false;
            if(Counter >= 9)
            {
                isBonus = true;
            }
            string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
            //print("text:" + text);
            //print("counter: " + Counter);
            button.onClick.AddListener(delegate { LoadLevel(int.Parse(text),isBonus); });
        }
    }

    private void LoadLevel(int level, bool isBonus)
    {
        
        if (ExtraStepAdder.numOfExtraStepAdder > 0)
        {
            togglePanel(true, level, isBonus);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
            if (isBonus)
            {
                LevelManager.NextLevel = level + 9;
            }
            else
            {
                LevelManager.NextLevel = level;
            }
                
        }

        
        
    }
}
