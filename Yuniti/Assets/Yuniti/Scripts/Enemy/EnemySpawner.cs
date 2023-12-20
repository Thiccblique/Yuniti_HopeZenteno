using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject[] objectToSpawn;
    public Transform[] spawnPoint;
    public float spawnInterval = 3.0f;

    public int spawnCount = 0;
    private int curRound = 0;
    private int enemiesToSpawn = 0;

    private EnemyBehaviour enemy;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        

    }

    public void StartSpawning()
    {
        curRound = RoundManager.instance.roundNumber;
        enemiesToSpawn = RoundManager.instance.totalEnemies;
        spawnCount = 0;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float firstEnemyCount = 0f;
            float secondEnemyCount = 0f;

            if (curRound >= 1 && curRound <= 3)
            {
                firstEnemyCount = RoundManager.instance.remainingEnemies * .8f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .2f;

                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);

                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                }
            }
            else if (curRound >= 4 && curRound <= 7)
            {
                
                firstEnemyCount = RoundManager.instance.remainingEnemies * .6f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .4f;
              

                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
            }
            else if (curRound >= 8) // This type of enemy count goes forever. Change later when end round is added plus more enmies.
            {
                firstEnemyCount = RoundManager.instance.remainingEnemies * .2f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .8f;

                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);

                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
            }


            for (int i = 0; firstEnemyCount > i && spawnCount < RoundManager.instance.remainingEnemies; i++)
            {
                int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                Instantiate(objectToSpawn[0], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
                spawnCount++;
                yield return new WaitForSeconds(spawnInterval);
            }

            for (int i = 0; secondEnemyCount > i; i++)
            {
                int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                Instantiate(objectToSpawn[1], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
                spawnCount++;
                yield return new WaitForSeconds(spawnInterval);
            }

            if(spawnCount >= enemiesToSpawn)
            {
                break;
            }
        }
    }

    /* Old Code
     
    public static EnemySpawner instance; 

    public GameObject objectToSpawn; 
    public Transform spawnPoint;
    public int numberOfSpawns = 5;
    public float spawnInterval = 2.0f; 

    public int spawnCount = 0;
    private float spawnTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        IfSpawn();
    }
   
    public void IfSpawn()
    {
        if (RoundManager.instance.canContinue == false )
        {
            
            WillSpawn();

        }
        if (RoundManager.instance.remainingEnemies <= 0)
        {
            spawnCount = 0;
        }


    }

    public void WillSpawn()
    {

        if (spawnCount < numberOfSpawns)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnObject();
                spawnTimer = 0.0f;
            }
        }
    }
    public void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        spawnCount++;
    }

     */
}
