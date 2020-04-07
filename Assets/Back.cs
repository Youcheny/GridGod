using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = backBtn.GetComponent<Button>();
        btn.onClick.AddListener(RestartLevelOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartLevelOnClick()
    {
        TrialNum.numOfTrial = 0;
        SceneManager.LoadScene("LevelScene",LoadSceneMode.Additive);
    }
}
