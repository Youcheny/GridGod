using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public Button restartLevelBtn;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = restartLevelBtn.GetComponent<Button>();
        btn.onClick.AddListener(RestartLevelOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartLevelOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
