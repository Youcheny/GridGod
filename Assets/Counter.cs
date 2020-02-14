using UnityEngine;
using UnityEngine.UI;

// Used to keep track of the total number of movements
public class Counter : MonoBehaviour
{
	public Transform player;
	public Text counterText;
 
    // Update is called once per frame
    void Update()
    {
    	PlayerMovement movement = player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        counterText.text = "Steps: "+ movement.GetCounter().ToString();
    }
}
