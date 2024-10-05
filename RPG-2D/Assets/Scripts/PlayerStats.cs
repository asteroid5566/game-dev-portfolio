using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
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
            health = maxHealth;
            gameObject.GetComponent<PlayerControl>().Respawn();
        }
    }

    public void HurtPlayer(int damage)
    {
        health -= damage;
    }
    public void SetMaxHealth()
    {
        health = maxHealth;
    }
}
