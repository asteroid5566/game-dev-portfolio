using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Dead());
        }
        
    }
    public void HurtEnemy(int damage)
    {
        health -= damage;
        
    }
    public void SetMaxHealth()
    {
        health = maxHealth;
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
