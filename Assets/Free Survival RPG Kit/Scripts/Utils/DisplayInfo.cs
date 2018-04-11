using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInfo : MonoBehaviour
{
    public GameObject itemCanvas;

    private void Start()
    {
        itemCanvas = this.gameObject.transform.Find("ItemCanvas").gameObject;
    }

    private void OnMouseOver()
    {     
        itemCanvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        itemCanvas.SetActive(false);
    }
}
