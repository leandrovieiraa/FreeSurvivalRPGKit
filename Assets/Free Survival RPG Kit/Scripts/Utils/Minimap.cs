using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour 
{
	public Transform player;

    GameObject canvas;
    public Button zoomIn;
    public Button zoomOut;

    float maxZoom = 20;
    float minZoom = 5;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        zoomIn.onClick.AddListener(ZoomIn);
        zoomOut.onClick.AddListener(ZoomOut);
    }

    private void Update()
    {
        if (Input.GetButtonDown("MinimapZoom"))
            ZoomOut();

        if (Input.GetButtonDown("MinimapZoomOut"))
            ZoomIn();
    }

    void LateUpdate ()
	{

        Vector3 newPosition = player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
	}

    void ZoomIn()
    {
        if(gameObject.GetComponent<Camera>().orthographicSize > minZoom)
            gameObject.GetComponent<Camera>().orthographicSize -= 1;
    }

    void ZoomOut()
    {
        if (gameObject.GetComponent<Camera>().orthographicSize < maxZoom)
            gameObject.GetComponent<Camera>().orthographicSize += 1;
    }
}