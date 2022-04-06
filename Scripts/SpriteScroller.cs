using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    //Variables for scrolling background sprites
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;


    //Handles references for dependencies
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    //Updates sprite offset on each frame
    void Update()
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
