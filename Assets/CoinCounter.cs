using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// Used to keep track of the total number of coins
public class CoinCounter : MonoBehaviour
{
    public Transform player;
    public Text counterText;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement movement = player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        counterText.text = "Coins: " + movement.playerStats.Consumables["Coin"].ToString();
    }
}
