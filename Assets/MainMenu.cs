using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        print("here");
        SceneManager.LoadScene("LevelScene",LoadSceneMode.Additive);
        
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
    }
}
