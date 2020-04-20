using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button playBtn;

    void Start()
    {
        Button btn = playBtn.GetComponent<Button>();
        btn.onClick.AddListener(Back);
    }


    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene");
        
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
    }
}
