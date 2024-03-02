using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject[] objectToSpawn;
    public GameObject bullEnemy;
    public Transform bullSpawnPoint;
    public Transform enemyTowerSP;
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
            float thirdEnemyCount = 0f;
            float fourthEnemyCount = 0f;

            /*if (curRound >= 1)
            {
                firstEnemyCount = RoundManager.instance.remainingEnemies * 1f;
                

                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                

                if (firstEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
                
            }*/
            if (curRound >= 1 && curRound <= 3)
            {
                firstEnemyCount = RoundManager.instance.remainingEnemies * .7f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .3f;

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

                firstEnemyCount = RoundManager.instance.remainingEnemies * .4f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .3f;
                thirdEnemyCount = RoundManager.instance.remainingEnemies * .2f;
                fourthEnemyCount = RoundManager.instance.remainingEnemies * .1f;


                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                thirdEnemyCount = Mathf.FloorToInt(thirdEnemyCount);
                fourthEnemyCount = Mathf.FloorToInt(fourthEnemyCount);

                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    thirdEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    fourthEnemyCount++;
                }

            }
            else if (curRound == 8) // bull spawning
            {
                if (bullEnemy.GetComponent<BullBehaviour>() != null)
                {
                    int filler = enemiesToSpawn - 1;
                    spawnCount = filler;
                    RoundManager.instance.remainingEnemies = RoundManager.instance.remainingEnemies - filler;
                    Instantiate(bullEnemy, bullSpawnPoint.position, bullSpawnPoint.rotation);
                    spawnCount++;

                    if (spawnCount >= enemiesToSpawn)
                    {
                        break;
                    }
                }
                else
                {
                    firstEnemyCount = RoundManager.instance.remainingEnemies * .4f;
                    secondEnemyCount = RoundManager.instance.remainingEnemies * .3f;
                    thirdEnemyCount = RoundManager.instance.remainingEnemies * .2f;
                    fourthEnemyCount = RoundManager.instance.remainingEnemies * .1f;

                    firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                    secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                    thirdEnemyCount = Mathf.FloorToInt(thirdEnemyCount);
                    fourthEnemyCount = Mathf.FloorToInt(fourthEnemyCount);

                    if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                    {
                        thirdEnemyCount++;
                    }
                    if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                    {
                        secondEnemyCount++;
                    }
                    if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                    {
                        firstEnemyCount++;
                    }
                    if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                    {
                        fourthEnemyCount++;
                    }
                }

            }
            else if (curRound == 9) // This type of enemy count goes forever. Change later when end round is added plus more enmies.
            {
                firstEnemyCount = RoundManager.instance.remainingEnemies * .4f;
                secondEnemyCount = RoundManager.instance.remainingEnemies * .3f;
                thirdEnemyCount = RoundManager.instance.remainingEnemies * .2f;
                fourthEnemyCount = RoundManager.instance.remainingEnemies * .1f;

                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                thirdEnemyCount = Mathf.FloorToInt(thirdEnemyCount);
                fourthEnemyCount = Mathf.FloorToInt(fourthEnemyCount);

                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    thirdEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    fourthEnemyCount++;
                }
            }


            if (curRound <= 9)
            {

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

                for (int i = 0; thirdEnemyCount > i; i++)
                {
                    int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                    Instantiate(objectToSpawn[2], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
                    spawnCount++;
                    yield return new WaitForSeconds(spawnInterval);
                }
                for (int i = 0; fourthEnemyCount > i; i++)
                {
                    int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                    Instantiate(objectToSpawn[3], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation);
                    spawnCount++;
                    yield return new WaitForSeconds(spawnInterval);
                }
            }

            if (curRound == 10 && !EnemyTower.instance.towerDefeated)
            {
                RoundManager.instance.remainingEnemies = 999;
                spawnInterval = 1.5f;
                //spawnInterval /= 2; (yeah this thing gets updated each frame so if the first one is 3 it will then go to 1.5 and then .75 and so on. I learned this the hard way. Made me laugh so I am keeping it here).
                int randomEnemyProbability = Random.Range(0, 100);

                if (randomEnemyProbability >= 0 && randomEnemyProbability < 20)
                {
                    Instantiate(objectToSpawn[0], enemyTowerSP.position, enemyTowerSP.rotation);
                    yield return new WaitForSeconds(spawnInterval);
                }
                else if (randomEnemyProbability >= 20 && randomEnemyProbability < 40)
                {
                    Instantiate(objectToSpawn[1], enemyTowerSP.position, enemyTowerSP.rotation);
                    yield return new WaitForSeconds(spawnInterval);
                }
                else if (randomEnemyProbability >= 40 && randomEnemyProbability < 70)
                {
                    Instantiate(objectToSpawn[2], enemyTowerSP.position, enemyTowerSP.rotation);
                    yield return new WaitForSeconds(spawnInterval);
                }
                else if (randomEnemyProbability >= 70 && randomEnemyProbability <= 100)
                {
                    Instantiate(objectToSpawn[3], enemyTowerSP.position, enemyTowerSP.rotation);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }

            if (spawnCount >= enemiesToSpawn)// || EnemyTower.instance.towerDefeated)
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
