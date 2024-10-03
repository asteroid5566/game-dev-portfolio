using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    Rigidbody2D ball;
    public GameObject arrow;
    
    public GameObject arrow2;

    public GameObject net;
    public bool shooting;

    public float speed;

    private Vector2 drag;

    private Vector2 shootRay;

    private bool goal;

    private bool goal2;

    private float ballPos;

    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        ball.gravityScale = 0;
        ballPos = transform.position.x;
    }

    void Update()
    {
        shootRay = new Vector2(arrow.transform.position.x - ball.transform.position.x,
                        arrow.transform.position.y - ball.transform.position.y);
        if (shooting)
        {
            ball.gravityScale = 1;
            ball.AddForce(shootRay * 200);
            shooting = false;
            arrow.SetActive(false);
            arrow2.SetActive(false);
        }
        else
        {
            arrow2.transform.position = ball.transform.position + Vector3.Scale(shootRay, new Vector2(0.5f, 0.5f));
            arrow2.transform.rotation = Quaternion.FromToRotation(Vector3.right, shootRay);
        }
        
    }
    private void OnMouseDown()
    {
        if(!shooting)
        {
            shooting = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D hited)
    {
        if (hited.gameObject.CompareTag("background"))
        {
            ball.gravityScale = 0;
            if(goal2)
            {
                float randomPos;
                randomPos = Random.Range(-5.5f, 3.5f);
                transform.position = new Vector3(randomPos, -2.1f, transform.position.z);
                ballPos = transform.position.x;
            }
            else
            {
                transform.position = new Vector3(ballPos, -2.1f, transform.position.z);
            }
            //transform.position = new Vector2(-4.29f, -2.1f);
            ball.velocity = new Vector2(0, 0);
            arrow.SetActive(true);
            arrow2.SetActive(true);
            goal = false;
            goal2 = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "goal")
        {
            goal = true;
        }
        if (collision.name == "goal2")
        {
            if (goal)
            {
                goal2 = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.name == "netcollider")
        {
            ball.AddForce(40 * transform.right * -Vector2.Dot(ball.velocity, transform.right));

            /*
            float netdist = ball.transform.position.y - net.transform.position.y;
            if (netdist > 0)
            {
                net.transform.position += new Vector3(0, 0.1f * netdist, 0);
            }
            net.transform.localScale = new Vector2(0.325f, Mathf.Abs(netdist));
            */        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.name == "netcollider")
        {
            net.transform.localScale = new Vector2(0.325f, 0.3f);
        }
        */
    }
}
