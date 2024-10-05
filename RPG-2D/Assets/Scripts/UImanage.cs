using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanage : MonoBehaviour
{
    public Slider healthBar;
    public Text HPText;
    public PlayerStats playerHealth;
    private static bool UIExist;

    void Start()
    {
        if (!UIExist)
        {
            UIExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    void Update()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.health;
        HPText.text = "HP :  " + playerHealth.health + " / " + playerHealth.maxHealth;
    }
}
