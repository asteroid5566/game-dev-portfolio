using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject ball;
    private bool trigger = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }
    private void OnMouseDrag()
    {
        
            Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
    }
    private void OnTriggerStay(Collider other)
    {
        trigger = true;
        Debug.Log("asd");
    }
    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }
}
