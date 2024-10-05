using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slime : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D slime;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;

    private Vector3 knockBack;  //擊退效果
    private bool allowMove = true;

    void Start()
    {
        slime = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeToMove * 1.25f);
    }

    
    void Update()
    {
        if (allowMove)
        {
            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                slime.velocity = moveDirection;
                if (timeToMoveCounter < 0f)
                {
                    moving = false;
                    timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;
                slime.velocity = Vector2.zero;
                if (timeBetweenMoveCounter < 0f)
                {
                    moving = true;
                    timeToMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeToMove * 1.25f);
                    moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "weapon")
        {
            knockBack = transform.position - other.transform.position;

            slime.AddForce(knockBack.normalized * 600f);
            allowMove = false;
            StartCoroutine(wait(0.2f));
        }
    }
    IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(t);
        slime.velocity = Vector3.zero;
        slime.transform.position = transform.position + new Vector3(0, 0, -10f);
        yield return new WaitForSeconds(0.1f);
        slime.transform.position = transform.position - new Vector3(0, 0, -10f);
        yield return new WaitForSeconds(0.3f);
        allowMove = true;
    }
}
