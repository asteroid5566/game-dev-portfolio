using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtObject : MonoBehaviour
{
    public int damage;

    public GameObject bloodEffect;
    public Transform hitPoint;

    public GameObject showDamage; 
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "player")
        {
            other.gameObject.GetComponent<PlayerStats>().HurtPlayer(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            other.gameObject.GetComponent<PlayerStats>().HurtPlayer(damage);
        }
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().HurtEnemy(damage);
            Instantiate(bloodEffect, hitPoint.position, hitPoint.rotation);

            var clone = (GameObject)Instantiate(showDamage, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<ShowDamage>().damage = damage;
        }
    }
}
