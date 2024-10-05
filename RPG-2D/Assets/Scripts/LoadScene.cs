using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string loadScene;

    public string exitPoint;

    private PlayerControl thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>();


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "player")
        {
            SceneManager.LoadScene(loadScene);
            thePlayer.startPoint = exitPoint;
        }
    }
}
