using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControl : MonoBehaviour
{
    float velocity = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(velocity, 0, 0);
        if (gameObject.transform.position.x >= 1.84f || gameObject.transform.position.x <= -1.84f)
        {
            velocity = (-1) * velocity;
        }

    }
}
