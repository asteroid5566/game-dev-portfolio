using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerControl : MonoBehaviour
{
    float velocity = 0.025f;
    Rigidbody2D rigid;
    public Text showtext;   //顯示數量
    int num = 0;
    public Text wintext;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += new Vector3(0, velocity, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += new Vector3(-velocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += new Vector3(0, -velocity, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += new Vector3(velocity, 0, 0);
        }
        */
        rigid.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),  CrossPlatformInputManager.GetAxis("Vertical"));
        //float horispeed = Input.GetAxis("Horizontal");
        //float vertspeed = Input.GetAxis("Vertical");
        //rigid.velocity = new Vector2(horispeed * 1.25f, vertspeed * 1.45f);
        
        if (gameObject.transform.position.x >= 2.5f)
        {
            wintext.text = "You win !";
            if (gameObject.transform.position.x >= 2.8f && gameObject.transform.position.y >= -1.8f)
            {
                rigid.velocity = new Vector2(0.1f, -0.6f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D hited)
    {
        if (hited.gameObject.CompareTag("pickup"))
        {
            gameObject.transform.localPosition = new Vector3(-3.2f, 0.25f, 0);
            num++;
            showtext.text = num.ToString();
        }
    }
    public void reset()
    {
        gameObject.transform.localPosition = new Vector3(-3.2f, 0.25f, 0);
        rigid.velocity = new Vector2(0, 0);
        wintext.text = "";
        num = 0;
        showtext.text = num.ToString();
    }
    public void esc()
    {
        Application.Quit();
    }
}
