using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject followTarget;

    private Vector3 targetPosition;

    public float moveSpeed;
    void Start()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z) + followTarget.transform.up * 5;
        transform.rotation = followTarget.transform.rotation;
    }

    
    void FixedUpdate()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z) + followTarget.transform.up * 5;
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.transform.rotation, moveSpeed * Time.deltaTime);

    }
}
