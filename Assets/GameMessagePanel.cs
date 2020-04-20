using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessagePanel : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePanel(bool on)
    {
        if(on)
        {
            panel.gameObject.SetActive(true);
        }
        else{
            panel.gameObject.SetActive(false);
        }
    }
}
