using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public GameObject followTarget;
    
    void Start()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z) ;
        transform.rotation = followTarget.transform.rotation;
    }


    void FixedUpdate()
    {
        
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.rotation = followTarget.transform.rotation;

    }
}
