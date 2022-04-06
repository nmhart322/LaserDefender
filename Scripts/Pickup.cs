using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Holds pickup type
    [SerializeField] string pickupType;


    //Accessor for pickup type
    public string GetPickupType()
    {
        return pickupType;
    }


    //Destroys pickup on collision
    public void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
