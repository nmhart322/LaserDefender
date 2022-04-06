using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Holds health bar UI image
    Image healthBar;


    //Handles reference for depencies
    void Awake()
    {
        healthBar = gameObject.GetComponent<Image>();
    }


    //Handles updating health bar image fill
    public void UpdateHealthBar(int health, int maxHealth)
    {
        healthBar.fillAmount = (float)health / maxHealth;
    }
}
