using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Variables for handling shield pickup
    [SerializeField] int maxShieldHealth = 20;
    [SerializeField] ParticleSystem sparkEffect;

    int currentShieldHealth;

    PickupHandler pickupHandler;


    //Handles references for dependencies
    void Awake()
    {
        pickupHandler = FindObjectOfType<PickupHandler>();
    }


    //Initializes shield health
    void Start()
    {
        currentShieldHealth = maxShieldHealth;
    }


    //Handles collision with game objects
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            IgnoreDamage(damageDealer.GetDamage());
            PlaySpark(other.transform.position);
            damageDealer.Hit();
        }

    }


    //Handles projectile hits while shield is active
    void IgnoreDamage(int value)
    {
        currentShieldHealth -= value;

        if (currentShieldHealth <= 0)
            DeactivateShield();
    }


    //Deactivates shield
    void DeactivateShield()
    {
        currentShieldHealth = maxShieldHealth;
        pickupHandler.shielded = false;
        gameObject.SetActive(false);
    }


    //Plays particle effect on shield hit
    void PlaySpark(Vector3 position)
    {
        if (sparkEffect != null)
        {
            ParticleSystem instance = Instantiate(sparkEffect, position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
