using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStartpoint : MonoBehaviour
{
    public string pointName;
    private PlayerControl player;
    //public Vector2 startDirection;
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        if (player.startPoint == pointName)
        {
            GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
            thePlayer.transform.position = transform.position;

            GameObject theCamera = GameObject.FindGameObjectWithTag("MainCamera");
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }
}
