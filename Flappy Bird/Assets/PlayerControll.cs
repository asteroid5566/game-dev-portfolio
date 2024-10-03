using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           

public class PlayerControll : MonoBehaviour
{
    Rigidbody2D player;
    bool playing = true;
    bool flying = false;
    bool spining = false;
    int wellNum;
    public Text score;          
    public GameObject winText;
    public GameObject loseText;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        wellNum = 0;
        score.text = wellNum.ToString() + (" / 10");
    }

    void FixedUpdate()
    {
        if (playing)
        {
            player.velocity = new Vector2(2f, player.velocity.y);  
            if(Input.GetKey(KeyCode.Space))
            {
                fly();
            }
        }
        if(spining)
        {
            transform.Rotate(0, 0, -1000 * Time.deltaTime);
        }
    }
    public void fly()
    {
        if (playing && flying == false)
        {
            if (player.velocity.y < 0)
            {
                player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.3f);
            }
            flying = true;
            player.AddForce(new Vector2(0, 540));
            StartCoroutine(wait());
        }
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(0.6f);
        playing = true;
        player.gravityScale = 2;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        flying = false;
    }
    IEnumerator win()
    {
        yield return new WaitForSeconds(2f);
        player.velocity = Vector2.zero;
        spining = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            loseText.SetActive(true);
            playing = false;
            player.gravityScale = 0;
            player.velocity = Vector2.zero;
        }
        if (collision.gameObject.tag == "checkpoint")
        {
            wellNum += 1;
            score.text = wellNum.ToString() + ("/10");
            if(wellNum >= 10)
            {
                playing = false;
                player.gravityScale = 0;
                player.velocity = new Vector2(player.velocity.x, 0f);
                winText.SetActive(true);
                StartCoroutine(win());
            }
        }
    }
    public void restart()
    {
        spining = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        winText.SetActive(false);
        loseText.SetActive(false);
        playing = false;
        wellNum = 0;
        score.text = wellNum.ToString() + ("/10");
        player.gravityScale = 0;
        player.transform.position = Vector2.zero;
        player.velocity = Vector2.zero;
        StartCoroutine(start());        
    }
    public void exit()
    {  
        Application.Quit();
    }
}
