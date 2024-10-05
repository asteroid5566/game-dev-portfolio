using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObj : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(clear());
    }
    IEnumerator clear()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
