using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //Variables for path finding
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;


    //Handles references for dependencies
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }


    //Accesses paths from current wave in wave spawner
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }


    //Calles path finding method
    void Update()
    {
        FollowPath();
    }


    //Updates target for enemy objects to follow, and assigns next node as target once enemy reaches position
    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
                waypointIndex++;
        }

        else
            Destroy(gameObject);
    }
}
