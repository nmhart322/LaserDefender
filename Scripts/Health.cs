using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Variables for health values
    [SerializeField] bool isPlayer;                 //tracks whether game object is player
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem explosion;

    int maxHealth;

    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;

    HealthBar healthBar;

    LevelManager levelManager;

    PickupSpawner pickupSpawner;


    //Handles references for dependencies
    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        healthBar = FindObjectOfType<HealthBar>();
        levelManager = FindObjectOfType<LevelManager>();
        pickupSpawner = FindObjectOfType<PickupSpawner>();
    }


    //Sets health to max health at scene start
    void Start()
    {
        maxHealth = health;
    }


    //Handles collision with game objects
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        Pickup pickup = other.GetComponent<Pickup>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayExplosion();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }

        if(pickup != null && isPlayer)
        {
            PickupHandler pickupHandler = gameObject.GetComponent<PickupHandler>();

            pickupHandler.AddPickup(pickup.GetPickupType());
            pickup.DestroyPickup();
        }
    }


    //Handles updating health value
    void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (isPlayer)
        {
            healthBar.UpdateHealthBar(health, maxHealth);
        }

        if (health <= 0)
        {
            Die();
        }
    }


    //Plays explosion clip on hit at camera position
    void PlayExplosion()
    {
        if(explosion != null)
        {
            ParticleSystem instance = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }


    //Plays camera shake script
    void ShakeCamera()
    {
        if (cameraShake != null && isPlayer)
            cameraShake.Play();
    }


    //Accessor for this object's current health
    public int GetHealth()
    {
        return health;
    }


    //Handles destruction of enemy prefab and scene change on health value falling below 0
    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            pickupSpawner.SpawnPickup(transform.position);
        }


        else
            levelManager.LoadGameOver();

        Destroy(gameObject);
    }


    //Handles updating health on health pickup
    public void PickupHealth()
    {
        if(isPlayer)
        {
            health += 10;
            health = Mathf.Clamp(health, 0, maxHealth);
            healthBar.UpdateHealthBar(health, maxHealth);
        }
    }
}
