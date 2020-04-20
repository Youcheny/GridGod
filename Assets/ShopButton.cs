using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button playBtn;

    void Start()
    {
        Button btn = playBtn.GetComponent<Button>();
        btn.onClick.AddListener(Shop);
    }


    public void Shop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}