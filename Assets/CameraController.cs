using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraController : MonoBehaviour
{

    public float zoomSpeed;
    public float targetOrtho;
    public float smoothSpeed;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    public Transform player;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
