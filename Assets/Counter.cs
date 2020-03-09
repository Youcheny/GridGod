using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Used to keep track of the total number of movements
public class Counter : MonoBehaviour
{
	public Transform player;
	public TextMeshProUGUI counterText;

    //void Start()
    //{
    //    counterText1 = GetComponent<TextMeshProUGUI>();
    //}
    
    // Update is called once per frame
    void Update()
    {
        // Camera camera = Camera.main;
        // this.transform.position = camera.ViewportToWorldPoint(new Vector3(20, 50, camera.nearClipPlane));

        PlayerMovement movement = player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        counterText.SetText("Steps: " + movement.GetRemainingSteps().ToString());
        
    }
}
