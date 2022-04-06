using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShooter : MonoBehaviour
{
    //Variables for missile shooter
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = .5f;
    [SerializeField] float maxRotation = 20f;

    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = .5f;
    [SerializeField] float minFiringRate = .3f;

    AudioPlayer audioPlayer;


    //Handles references for dependencies
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    //Starts coroutine that manages missile firing each frame
    void Start()
    {
        FireMissile();
    }


    //Starts coroutine that manages missile firing for enemies each frame
    void FireMissile()
    {
        if(useAI)
        {
            StartCoroutine(FireMissileContinuously());
        }
    }


    //Handles missile firing on each frame (only used by AI)
    IEnumerator FireMissileContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(GetRandomRotation()));

            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
                rigidbody.velocity = rigidbody.transform.up * projectileSpeed;

            Destroy(instance, projectileLifeTime);

            audioPlayer.PlayMissileClip();

            yield return new WaitForSeconds(GetRandomFiringRate());
        }
    }


    //Generates random starting missile rotation
    Vector3 GetRandomRotation()
    {
        float degrees = Random.Range(-maxRotation, maxRotation);

        if (useAI)
            degrees = 180 - degrees;

        return new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, degrees);
    }


    //Generates random delay between firing
    float GetRandomFiringRate()
    {
        float tempFiringRate = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
        return Mathf.Clamp(tempFiringRate, minFiringRate, float.MaxValue);
    }


    //Handles missile firing on player prompt
    public void FirePlayerMissile()
    {
        if(!useAI)
        {
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(GetRandomRotation()));

            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
                rigidbody.velocity = instance.transform.up * projectileSpeed;

            Destroy(instance, projectileLifeTime);

            audioPlayer.PlayMissileClip();
        }
    }
}
