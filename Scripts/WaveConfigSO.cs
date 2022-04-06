using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    //Variables for wave configuration
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnTimeVariance;
    [SerializeField] float minSpawnTime = .2f;


    //Accessor for enemy movement speed
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }


    //Accessor for starting waypoint of path
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }


    //Accessor for waypoints in path prefab
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }


    //Accessor for number of enemies in wave
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }


    //Accessor for enemy prefabs
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }


    //Generates random time between enemy spawning
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenSpawns - spawnTimeVariance, timeBetweenSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);

    }


    //Adds enemy to wave
    public void AddEnemy(GameObject enemy)
    {
        enemyPrefabs.Add(enemy);
    }


    //Sets path of wave
    public void SetPath(Transform path)
    {
        pathPrefab = path;
    }
}
