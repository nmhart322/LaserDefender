using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //Variables for meteor rotation and scale
    [SerializeField] float minRotation = 1f;
    [SerializeField] float maxRotation = 45f;

    [SerializeField] float minScale = 1f;
    [SerializeField] float maxScale = 2f;

    float rotation;


    //Initializes rotation and scale for spawned meteors
    void Start()
    {
        rotation = Random.Range(minRotation, maxRotation) * Time.deltaTime;
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1);
    }


    //Rotates meteor every frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotation));
    }
}
