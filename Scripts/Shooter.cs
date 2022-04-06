using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //Variables for managing shooting
    [Header("General")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = .2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = .5f;
    [SerializeField] float minFiringRate = .5f;

    [Header("Trigun")]
    [SerializeField] bool trigunActivated = false;
    [SerializeField] float trigunRotation = 30f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;


    //Handles references for dependencies
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    //Activates auto-firing for enemies
    void Start()
    {
        if (useAI)
            isFiring = true;
    }


    //Calls fire method on each frame
    void Update()
    {
        Fire();
    }


    //Updates firing state each frame
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }


    //Fires projectiles on time interval
    IEnumerator FireContinuously()
    {
        while (true)
        {
            FireGun();

            if (!useAI)
            {
                yield return new WaitForSeconds(firingRate);
            }

            else
                yield return new WaitForSeconds(GetRandomFiringRate());
        }
    }


    //Generates random fire rate
    float GetRandomFiringRate()
    {
        float tempFiringRate = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
        return Mathf.Clamp(tempFiringRate, minFiringRate, float.MaxValue);
    }


    //Activates trigun pickup
    public void ActivateTrigun(bool state)
    {
        trigunActivated = state;
    }


    //Fires projectiles
    void FireGun()
    {
        GameObject instance = Instantiate(projectile, transform.position, transform.rotation);

        Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
            rigidbody.velocity = transform.up * projectileSpeed;

        if(trigunActivated)
        {
            FireLeft();
            FireRight();
        }

        Destroy(instance, projectileLifeTime);

        audioPlayer.PlayShootingClip();
    }


    //Fires left projectile of trigun
    void FireLeft()
    {
        GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - trigunRotation)));

        Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
            rigidbody.velocity = rigidbody.transform.up * projectileSpeed;

        Destroy(instance, projectileLifeTime);
    }


    //Fires right projectile of trigun
    void FireRight()
    {
        GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + trigunRotation)));

        Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
            rigidbody.velocity = rigidbody.transform.up * projectileSpeed;

        Destroy(instance, projectileLifeTime);
    }


    //Accessor for state of trigun
    public bool GetTrigun()
    {
        return trigunActivated;
    }
}
