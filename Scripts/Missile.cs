using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //Variables for missile firing
    [SerializeField] float swaySpeed = 2f;
    [SerializeField] float maxMissileSway = 20f;
    [SerializeField] float swayTime = 1f;
    [SerializeField] float swayTimeVariance = .75f;

    [SerializeField] float edgeTriggerAngle = 15f;

    float missileSway;


    //Starts coroutine that manages missile rotation
    void Start()
    {
        StartCoroutine(MissileSway());
    }


    //Updates missile rotation and position
    void Update()
    {
        transform.Rotate(0, 0, missileSway);
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * transform.up;
    }


    //Generates new rotation over time interval
    IEnumerator MissileSway()
    {
        while (true)
        {
            missileSway = GetRandomSway() * swaySpeed * Time.deltaTime;

            yield return new WaitForSeconds(GetRandomSwayTime());
        }

    }


    //Generates a random rotation wihtin range
    float GetRandomSway()
    {
        return Random.Range(-maxMissileSway, maxMissileSway);
    }


    //Generates a random time period between new rotation
    float GetRandomSwayTime()
    {
        return Random.Range(swayTime - swayTimeVariance, swayTime + swayTimeVariance);
    }


    //Handles collision with bounds left and right of screen
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Edge Collider Left")                                                          //if missile collides with bounds, turn toward center of screen at half magnitude
            missileSway = -edgeTriggerAngle / 2 * swaySpeed * Time.deltaTime;

        if (other.gameObject.name == "Edge Collider Right")
            missileSway = edgeTriggerAngle / 2 * swaySpeed * Time.deltaTime;
    }


    //Handles exit collision with bounds of left and right of screen
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Edge Collider Left" || other.gameObject.name == "Edge Collider Right")        //if missile exits collision with bounds, zero rotation until new rotation update
            missileSway = 0f;
    }


    //Handles constant collision with left and right of screen
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Edge Collider Left")                                                          //if missile stays in collision with bounds, turn toward center of screen at full magnitude
            missileSway = -edgeTriggerAngle * swaySpeed * Time.deltaTime;

        if (other.gameObject.name == "Edge Collider Right")
            missileSway = edgeTriggerAngle * swaySpeed * Time.deltaTime;
    }
}
