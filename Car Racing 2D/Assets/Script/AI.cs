using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Rigidbody2D player;
    int pointNum = 1;
    public int allPointNum;
    string pointName;
    Vector3 moveDir;
    GameObject target;
    public float moveSpeed;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(pointNum <= allPointNum)
        {
            pointName = "checkpoint (" + pointNum.ToString() + ")";
            target = GameObject.Find(pointName);
            moveDir = target.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDir);
            player.velocity = moveDir.normalized * moveSpeed;
        }
        else
        {
            player.velocity = Vector3.zero;
            Debug.Log("123");
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checkpoint")
        {
            if (collision.name == pointName)
            {
                
                pointNum += 1;
            }
        }
    }
}
