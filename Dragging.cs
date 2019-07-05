using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public Vector3 offset;

    private Vector3 lastPosition;
    private bool dragging;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerPanel = GameObject.Find("PlayerPanel");
        offset = PlayerPanel.transform.position - Camera.main.transform.position;
        offset.z -= 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        { 
            var screenPoint = Input.mousePosition;
            screenPoint.z = offset.z;
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }

        if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        lastPosition = Input.mousePosition;
    }
    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }
    }
    
}
