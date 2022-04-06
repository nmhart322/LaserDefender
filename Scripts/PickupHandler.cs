using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    //Variables for handling pickups
    [SerializeField] float pickupTime = 5f;
    [SerializeField] int missileCount = 5;

    Health playerHealth;

    Shooter shooter;

    MissileCounter missileCounter;

    GameObject shield;
    [HideInInspector] public bool shielded = false;
    [HideInInspector] public bool missileFiring = false;


    //Handles references for dependencies
    void Awake()
    {
        playerHealth = gameObject.GetComponent<Health>();

        shooter = gameObject.GetComponent<Shooter>();

        shield = transform.Find("Shield").gameObject;

        missileCounter = FindObjectOfType<MissileCounter>();
    }


    //Activates pickup of string type
    public void AddPickup(string type)
    {
        switch (type)
        {
            case "Health":
            {
                playerHealth.PickupHealth();
                break;
            }

            case "Shield":
            {
                if (!shielded)
                    StartCoroutine(ActivateShield());
                break;
            }

            case "Trigun":
            {
                if(!shooter.GetTrigun())
                    StartCoroutine(ActivateTrigun());
                break;
            }

            case "Missiles":
            {
                missileCount++;
                missileCounter.UpdateMissileCounter();
                break;
            }

            case "Beam":
            {
                Debug.Log("Beam");
                break;
            }

            default:
                break;
        }
    }


    //Activates trigun pickup, and deactivates after delay
    IEnumerator ActivateTrigun()
    {
        shooter.ActivateTrigun(true);

        yield return new WaitForSeconds(pickupTime);

        shooter.ActivateTrigun(false);
    }


    //Activates shield pickup, and deactivates after delay
    IEnumerator ActivateShield()
    {
        shield.SetActive(true);
        shielded = true;

        yield return new WaitForSeconds(pickupTime);

        if(shield.activeInHierarchy)
        {
            shield.SetActive(false);
            shielded = false;
        }
    }


    //Activates missile firing pickup, and deactivates after delay (not currently in use)
    IEnumerator ActivateMissiles()
    {
        missileFiring = true;

        yield return new WaitForSeconds(pickupTime);

        missileFiring = false;
    }


    //Accessor for missile count
    public int GetMissileCount()
    {
        return missileCount;
    }


    //Updates missile count on use
    public void DepleteMissile()
    {
        if(missileCount > 0)
        {
            missileCount--;
        }
    }
}
