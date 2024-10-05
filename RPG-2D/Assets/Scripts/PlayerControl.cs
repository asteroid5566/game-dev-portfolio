using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Camera mycam;

    public float moveSpeed;

    private Animator anim;

    private bool playerMoving;

    public Vector2 lastMove;

    private Rigidbody2D player;

    private static bool playerExist;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;   //轉換場景
    private Vector3 respawnPoint;

    private Vector3 knockBack;      //擊退效果
    public bool allowMove = true;

    private bool respawning = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();

        if (!playerExist)           //轉換場景時不刪除物件
        {
            playerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
        respawnPoint = player.transform.position;
    }

    
    void FixedUpdate()
    {
        if (Input.GetKey("x"))           //調整視野
        {
            mycam.orthographicSize = 5.5f;
        }
        else if(Input.GetKey("z"))
        {
            mycam.orthographicSize = 9;
        }
        
        playerMoving = false;

        if (allowMove && !respawning)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                player.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, player.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                player.velocity = new Vector2(player.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5)
            {
                player.velocity = new Vector2(0f, player.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5)
            {
                player.velocity = new Vector2(player.velocity.x, 0f);
            }
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("Moving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

        if (!attacking)
        {
            if (Input.GetKey("space"))               //攻擊
            {
                attackTimeCounter = attackTime;
                attacking = true;
                anim.SetBool("Attack", true);
            }
        }
        else
        {
            attacking = false;
            anim.SetBool("Attack", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            knockBack = transform.position - other.transform.position;

            player.AddForce(knockBack.normalized * 650f);
            allowMove = false;
            StartCoroutine(wait(0.2f));
            
        }
    }
    IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(t);
        player.velocity = Vector3.zero;
        player.transform.position = player.transform.position + new Vector3(0, 0, -10f);
        yield return new WaitForSeconds(0.1f);
        player.transform.position = player.transform.position - new Vector3(0, 0, -10f);
        yield return new WaitForSeconds(0.3f);
        allowMove = true;
    }
    public void Respawn()
    {
        respawning = true;
        StartCoroutine(Respawn2());
    }
    public IEnumerator Respawn2()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.transform.position += new Vector3(0, 0, -10);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        respawning = false;
        lastMove = new Vector2(0, -1);
        StartCoroutine(Respawn3());
    }
    IEnumerator Respawn3()
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new Vector3(-11.5f, -10f, player.transform.position.z);
    }
}
