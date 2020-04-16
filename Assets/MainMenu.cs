using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playBtn;

    void Start()
    {
        Button btn = playBtn.GetComponent<Button>();
        btn.onClick.AddListener(PlayGame);
    }


    public void PlayGame()
    {
        print("here");
        SceneManager.LoadScene("LevelScene");
        
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
    }
}
