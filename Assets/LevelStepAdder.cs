using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


// Used to keep track of the total number of coins
public class LevelStepAdder : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    // Update is called once per frame
    void Update()
    {
        // Camera camera = Camera.main;
        // this.transform.position = camera.ViewportToWorldPoint(new Vector3(20, 55, camera.nearClipPlane));

        //print(ExtraStepAdder.numOfExtraStepAdder.ToString()+"num ber of extra stepader");
        counterText.SetText("StepAdder: " + ExtraStepAdder.numOfExtraStepAdder.ToString());
    }
}
