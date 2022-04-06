using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //Variables for damage dealer
    [SerializeField] int damage = 10;


    //Accessor method for damage amount
    public int GetDamage()
    {
        return damage;
    }


    //Handles destruction of game object on collision
    public void Hit()
    {
        Destroy(gameObject);
    }
}
