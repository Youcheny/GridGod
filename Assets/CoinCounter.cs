using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


// Used to keep track of the total number of coins
public class CoinCounter : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI counterText;

    // Update is called once per frame
    void Update()
    {
        // Camera camera = Camera.main;
        // this.transform.position = camera.ViewportToWorldPoint(new Vector3(20, 55, camera.nearClipPlane));

        PlayerMovement movement = player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        counterText.SetText("Coins: " + movement.playerStats.Consumables["Coin"].ToString());
    }
}
