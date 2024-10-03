using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarControl : MonoBehaviour
{
    public bool movFoward = false;
    public bool movBack = false;
    public bool movLeft = false;
    public bool movRight = false;


    public bool speeding = false;
    public bool turning = false;

    Rigidbody2D player;

    public Button front;
    public Button right;
    public Button left;
    public Button back;

    public float speed;
    public float angularSpeed;
    public float driftFactorSticky;
    public float driftFactorSlippy;
    public float maxStickyV;
    float driftFactor;

    public Text speedText;

    public TrailRenderer leftskid;
    public TrailRenderer rightskid;
    private float driftCounter = 0;

    public Text N2O;
    public float timeCounter;
    public Text realTime;
    private int N2Onum = 1;
    private float N2Ocounter = 0;
    public float N2Omax;
    private bool N2Oing = false;
    public GameObject burst;
    public GameObject N2Oicon1;
    public GameObject N2Oicon2;
    public Slider N2OBar;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        burst.SetActive(false);
    }


    void FixedUpdate()
    {
        if(movFoward == true)
        {
            accFoward();
        }
        if (movBack == true)
        {
            accBack();
        }
        if (movLeft == true)
        {
            accLeft();
        }
        if (movRight == true)
        {
            accRight();
        }

        if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("i"))
        {
            accFoward();
            //player.AddForce(transform.up * speed);
            //N2Ocounter += Time.deltaTime * 0.1f;
        }
        if (Input.GetKey("down") || Input.GetKey("s") || Input.GetKey("k"))
        {
            accBack();
            /*
            if (Vector2.Dot(player.velocity, player.transform.up) > 0)
            {
                player.AddForce(transform.up * -1f * speed);
            }
            player.AddForce(transform.up * -0.5f * speed);
            */
        }

        float af = Mathf.Lerp(0, angularSpeed, player.velocity.magnitude / 10);


        //player.angularVelocity = Input.GetAxisRaw("Horizontal") * -af;          //angularvelocity
        if (Input.GetKey("j") || Input.GetKey("left") || Input.GetKey("a"))
        {
            accLeft();
            /*
            if (Vector2.Dot(player.velocity, transform.up) >= 0)
            {
                player.angularVelocity = af;
            }
            else
            {
                player.angularVelocity = -af;
                Debug.Log("123");
            }
            */

        }

        if (Input.GetKey("d") || Input.GetKey("right") || Input.GetKey("l"))
        {
            accRight();
            /*
            if (Vector2.Dot(player.velocity, transform.up) >= 0)
            {
                player.angularVelocity = -af;
            }
            else
            {
                player.angularVelocity = af;
                Debug.Log("123");
            }
            */
        }

        driftFactor = driftFactorSticky;
        leftskid.emitting = false;
        rightskid.emitting = false;

        if (RightV().magnitude > maxStickyV && Input.GetKey("left shift"))          //dirfting 2.8
        {
            driftFactor = driftFactorSlippy;
            leftskid.emitting = true;
            rightskid.emitting = true;
            driftCounter += Time.deltaTime;
            N2Ocounter += Time.deltaTime;
        }
        N2OBar.maxValue = N2Omax;
        N2OBar.value = N2Ocounter;

        if (RightV().magnitude < maxStickyV && !Input.GetKey("left shift"))
        {
            if (driftCounter > 0.5f && !N2Oing)
            {
                player.AddForce(transform.up * 500);
                burst.SetActive(true);
                StartCoroutine(DriftBurst());
            }
            driftCounter = 0;
        }

        if (N2Ocounter >= N2Omax || Input.GetKey(KeyCode.Q))
        {
            N2Ocounter = 0;
            if (N2Onum < 2)
            {
                N2Onum += 1;
            }
        }
        N2O.text = N2Ocounter.ToString("F3");

        if (N2Onum > 0 && !N2Oing && Input.GetKey(KeyCode.LeftControl) && player.velocity.magnitude > 0)
        {
            N2Oing = true;
            burst.SetActive(true);
            StartCoroutine(N2Ofunction());
        }

        if (N2Oing)
        {
            if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L) || Input.GetAxisRaw("Horizontal") != 0)
            {
                player.AddForce(transform.up * 10);
            }
            else
            {
                player.AddForce(transform.up * 5);
            }
        }
        if (N2Onum == 0)
        {
            N2Oicon1.SetActive(false);
            N2Oicon2.SetActive(false);
        }
        else if (N2Onum == 1)
        {
            N2Oicon1.SetActive(true);
            N2Oicon2.SetActive(false);
        }
        else if (N2Onum == 2)
        {
            N2Oicon1.SetActive(true);
            N2Oicon2.SetActive(true);
        }

        timeCounter += Time.deltaTime;
        realTime.text = timeCounter.ToString("F2");


        player.velocity = ForwardV() + RightV() * driftFactor;
        speedText.text = (4 * player.velocity.magnitude).ToString("F2");


    }

    Vector2 ForwardV()
    {
        return transform.up * Vector2.Dot(player.velocity, transform.up);
    }
    Vector2 RightV()
    {
        return transform.right * Vector2.Dot(player.velocity, transform.right);
    }
    IEnumerator DriftBurst()
    {
        yield return new WaitForSeconds(0.5f);
        if (!N2Oing)
        {
            burst.SetActive(false);
        }
    }
    IEnumerator N2Ofunction()
    {
        N2Onum -= 1;
        yield return new WaitForSeconds(4);
        N2Oing = false;
        burst.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checkpoint")
        {
            Debug.Log("123");
        }
    }
    IEnumerator waitSpeed()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        speeding = false;
    }
    IEnumerator waitTurn()
    {
        yield return new WaitForSeconds(0.2f);
        turning = false;
    }
    public void accFoward()
    {
        if (speeding == false)
        {
            player.AddForce(transform.up * speed);
            N2Ocounter += Time.deltaTime * 0.1f;
            //speeding = true;
            //StartCoroutine(waitSpeed());
        }
    }
    public void accBack()
    {
        N2Oing = false;
        burst.SetActive(false);
        if (speeding == false)
        {
            if (Vector2.Dot(player.velocity, player.transform.up) > 0)
            {
                player.AddForce(transform.up * -1f * speed);
            }
            player.AddForce(transform.up * -0.5f * speed);
            //speeding = true;
            //StartCoroutine(waitSpeed());
        }
    }

    public void accLeft()
    {
        if (turning == false)
        {
            float af = Mathf.Lerp(0, angularSpeed, player.velocity.magnitude / 6);
            if (Vector2.Dot(player.velocity, transform.up) >= 0)
            {
                player.angularVelocity = af;
            }
            else
            {
                player.angularVelocity = -af;
                Debug.Log("123");
            }
            //speeding = true;
            //StartCoroutine(waitSpeed());
        }
    }
    public void accRight()
    {
        if (turning == false)
        {
            float af = Mathf.Lerp(0, angularSpeed, player.velocity.magnitude / 6);
            if (Vector2.Dot(player.velocity, transform.up) >= 0)
            {
                player.angularVelocity = -af;
            }
            else
            {
                player.angularVelocity = af;
                Debug.Log("123");
            }
            //speeding = true;
            //StartCoroutine(waitSpeed());
        }
    }
    public void FowardPress()
    {
        movFoward = true;
    }
    public void FowardUp()
    {
        movFoward = false;
    }
    public void BackPress()
    {
        movBack = true;
    }
    public void BackUp()
    {
        movBack = false;
    }
    public void LeftPress()
    {
        movLeft = true;
    }
    public void LeftUp()
    {
        movLeft = false;
    }
    public void RightPress()
    {
        movRight = true;
    }
    public void RightUp()
    {
        movRight = false;
    }
}
