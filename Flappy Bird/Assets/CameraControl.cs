using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerPosition;

    void FixedUpdate()
    {
        transform.position = new Vector3(playerPosition.transform.position.x +4, transform.position.y, transform.position.z);
    }
}
