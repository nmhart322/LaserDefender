using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    //Variables for meteor spawning
    [SerializeField] List<GameObject> meteors;
    [SerializeField] float baseVelocity = 5f;
    [SerializeField] float velocityVariance = 2f;
    [SerializeField] float spawnRate = 10f;
    [SerializeField] float spawnRateVariance = 2f;

    [SerializeField] float padding = .5f;

    [SerializeField] float meteorLifetime = 2f;

    Vector2 minXPos;
    Vector2 maxXPos;

    static MeteorSpawner meteorInstance;


    //Calls singleton management
    void Awake()
    {
        ManageSingleton();
    }


    //Initializes position and starts coroutine for spawning
    void Start()
    {
        InitRange();
        StartCoroutine(SpawnMeteor());
    }


    //Initializes range of positions for possible spawning
    void InitRange()
    {
        Vector2 paddingLeft = new Vector2(-padding, padding);
        Vector2 paddingRight = new Vector2(padding, padding);

        Camera mainCamera = Camera.main;
        minXPos = mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
        maxXPos = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        minXPos = new Vector2(minXPos.x + paddingLeft.x, minXPos.y + paddingLeft.y);
        maxXPos = new Vector2(maxXPos.x + paddingRight.x, maxXPos.y + paddingRight.y);
    }


    //Coroutine that spawns meteor object at random position within range
    IEnumerator SpawnMeteor()
    {
        while(true)
        {
            GameObject instance = Instantiate(GetRandomMeteor(), GetRandomPosition(), Quaternion.Euler(0, 0, GetRandomRotation()), transform);

            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();

            Vector2 tempVelocity = GetRandomVelocity();

            if (rigidbody != null)
                rigidbody.velocity = new Vector2(transform.up.x - tempVelocity.x, transform.up.y - tempVelocity.y);

            Destroy(instance, meteorLifetime / rigidbody.velocity.magnitude);

            yield return new WaitForSeconds(GetRandomSpawnRate());
        }
    }


    //Handles randomly generating meteor objects
    GameObject GetRandomMeteor()
    {
        return meteors[Mathf.Clamp(Random.Range(0, meteors.Count) , 0, meteors.Count)];
    }


    //Handles randomly generating position within range
    Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(minXPos.x, maxXPos.x), minXPos.y);
        
    }


    //Handles randomly generating rotation for meteor
    float GetRandomRotation()
    {
        return Random.Range(0, 360);
    }


    //Handles randomly generating velocity for spawned meteor
    Vector2 GetRandomVelocity()
    {
        return new Vector2(Random.Range(-velocityVariance, velocityVariance) , Random.Range(baseVelocity - velocityVariance, baseVelocity + velocityVariance));
    }


    //Handles randomly generating time value for spawn rate
    float GetRandomSpawnRate()
    {
        return Random.Range(spawnRate - spawnRateVariance, spawnRate + spawnRateVariance);
    }


    //Manages singleton of meteor spawner
    void ManageSingleton()
    {
        if(meteorInstance != null)                      //if object exists in scene, destroy this
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {                                               //if object does not exist in scene, make this persistent
            meteorInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
