using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    //Variables for spawning pickups
    [SerializeField] List<GameObject> pickups;
    [SerializeField] float pickupSpeed = 5;
    [SerializeField] float pickupLifetime = 1;


    //Spawns pickup on destruction of enemy
    public void SpawnPickup(Vector3 position)
    {
        GameObject instance = Instantiate(GetRandomPickup(), position, Quaternion.identity);

        Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
            rigidbody.velocity = new Vector2(0, -pickupSpeed);

        Destroy(instance, pickupLifetime);
    }


    //Generates random pickup object
    GameObject GetRandomPickup()
    {
        int index = Random.Range(0, pickups.Count);

        return pickups[index];
    }
}
