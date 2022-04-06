using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Variables for enemy spawner
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] List<Transform> paths;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    [SerializeField] WaveConfigSO blankWave;

    WaveConfigSO currentWave;


    //Starts coroutine for spawning waves
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }


    //Handles wave spawning
    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(5);

        do
        {
            foreach (WaveConfigSO wave in waveConfigs)                                              //for each wave scriptable object...
            {
                currentWave = wave;
                currentWave.SetPath(GetRandomPath());                                               //set random path

                for (int index = 0; index < currentWave.GetEnemyCount(); index++)                   //for each enemy in wave, spawn at first node in path at time interval
                {
                    Instantiate(currentWave.GetEnemyPrefab(index), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0 , 0 , 180), transform);  
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);

        isLooping = true;                                                                           //after last wave, begin randomly generating enemies with paths

        do
        {
            currentWave = Instantiate(blankWave);
            currentWave.AddEnemy(GetRandomEnemy());
            currentWave.SetPath(GetRandomPath());

            Instantiate(currentWave.GetEnemyPrefab(0), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);

            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());

        } while (isLooping);

    }


    //Accessor for current wave scriptable object
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }


    //Handles random generation of enemy path
    Transform GetRandomPath()
    {
        int index = Mathf.Clamp(Random.Range(0, paths.Count), 0, paths.Count);
        return paths[index];
    }


    //Handles random generation of enemy prefab
    GameObject GetRandomEnemy()
    {
        int index = Mathf.Clamp(Random.Range(0, enemies.Count), 0, enemies.Count);
        return enemies[index];
    }
}