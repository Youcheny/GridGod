using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void SelectLevel()
    {
        print("selectLevel");
        SceneManager.LoadScene("SampleScene");

    }
}
