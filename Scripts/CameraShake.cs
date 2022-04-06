using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //Variables for camera shake
    [Header("Camera Shake Values")]
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = .5f;

    //Holds camera's initial position
    Vector3 initialPosition;


    void Start()
    {
        initialPosition = transform.position;
    }


    //Starts coroutine for camera shake
    public void Play()
    {
        StartCoroutine(Shake());
    }


    //Handles position updating for camera shake
    IEnumerator Shake()
    {
        float elapsedTime = 0;

        while(elapsedTime < shakeDuration)                      //for duration, update position to new random at time interval
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;
    }
}