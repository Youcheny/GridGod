using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


// Used to keep track of the total number of coins
public class LevelCoins : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    // Update is called once per frame
    void Update()
    {
        // Camera camera = Camera.main;
        // this.transform.position = camera.ViewportToWorldPoint(new Vector3(20, 55, camera.nearClipPlane));

        
        counterText.SetText("Coins: " + CoinManager.numOfCoins.ToString());
    }
}
