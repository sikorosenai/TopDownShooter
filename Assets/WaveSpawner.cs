using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //contains variables to tweak for each wave
    //this is so that wave structure shows up in unity inspector
    [System.Serializable]
    public class Wave
    {
        //contains all different enemies that it is possible to spawn
        public Enemy[] enemies;
        //how many enemies in wave
        public int count;
        public float timeBetweenSpawns;
    }

    //wave array that is going to call the waves
    public Wave[] waves;


    //array of spawn points
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    //stores current wave in game
    private Wave currentWave;
    private int currentWaveIndex;
    //gives reference to player
    private Transform player;
    private bool finishedSpawning;

    private void Start()
    {
        //player is equal to position of player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //start first wave
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        //wait for time between waves amount of seconds
        yield return new WaitForSeconds(timeBetweenWaves);
        //spawn the actual wave
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave (int index)
    {
        //current wave = waves array 
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++)
        {
            //if player is dead leave the coroutine
            if (player == null)
            {
                yield break;
            }
            //pick a random enemy from current enemies in the wave array
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            //specific spot for an enemy to spawn, choose one of the spawn points
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //instantiate the enemy
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            //if count of enemies (i) = amount of enemies spawned (-1 because it starts at 0)
            if(i == currentWave.count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            //wait for the time between spawns for the next enemy
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);  
        }
    }

    private void Update()
    {
        //if finsished spawning and there are no enemy game objects in scene
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //reset finished spawning
            finishedSpawning = false;
            //check if there is another wave to be spawned
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex ++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("Game finsished :D");
                
            }
        }
    }
}
