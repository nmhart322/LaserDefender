using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Variables for handling player input
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 45f;
    [SerializeField] float maxRotation = 30f;
    [SerializeField] float padding = 1f;
    [SerializeField] float UIPadding = 1f;

    Vector2 rawInput;
    float turnInput;
    Quaternion quaternionRotation;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    MissileShooter missileShooter;
    MissileCounter missileCounter;

    PickupHandler pickupHandler;


    //Handles references for dependencies
    void Awake()
    {
        InitBounds();
        shooter = GetComponent<Shooter>();
        missileShooter = GetComponent<MissileShooter>();
        missileCounter = FindObjectOfType<MissileCounter>();
        pickupHandler = GetComponent<PickupHandler>();
    }


    //Initializes rotation for shooting
    void Start()
    {
        quaternionRotation = Quaternion.Euler(0, 0, maxRotation);
    }


    //Calls movement and turning methods on each frame
    void Update()
    {
        Move();
        Turn();
    }


    //Input event that updates input values for movement
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }


    //Updates position of player based on player input
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + padding, maxBounds.x - padding);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + padding + UIPadding, maxBounds.y - padding);

        transform.position = newPos;
    }


    //Initializes bounds for player movement
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0 , 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1 , 1));
    }


    //Input event that updates isFiring flag
    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }


    //Input event that fires missile
    void OnFireMissile(InputValue value)
    {
        if (missileShooter != null && value.isPressed && pickupHandler.GetMissileCount() > 0)
        {
            pickupHandler.DepleteMissile();
            missileCounter.UpdateMissileCounter();
            missileShooter.FirePlayerMissile();
        }
    }


    //Input event that updates player rotation
    public void OnTurn(InputValue value)
    {
        turnInput = -value.Get<float>();
    }


    //Updates rotation within range
    void Turn()
    {
        if (Mathf.Abs(transform.rotation.z) < quaternionRotation.z)
        {
            transform.Rotate(0, 0, turnInput * turnSpeed * Time.deltaTime);
        }

        else
        {
            if(transform.rotation.z > quaternionRotation.z && turnInput < 0)
            {
                transform.Rotate(0, 0, turnInput * turnSpeed * Time.deltaTime);
            }

            else if(transform.rotation.z < -quaternionRotation.z && turnInput > 0)
            {
                transform.Rotate(0, 0, turnInput * turnSpeed * Time.deltaTime);
            }
        }

    }


}
