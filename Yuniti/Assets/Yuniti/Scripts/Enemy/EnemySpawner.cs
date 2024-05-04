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
    public bool factionSpawns;
    public bool firstFaction;
    public bool secondFaction;

    public int spawnCount = 0;
    private int curRound = 0;
    public int enemiesToSpawn = 0;

    private EnemyBehaviour enemy;

    public static List<GameObject> enemiesSpawned = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        foreach (GameObject g in enemiesSpawned.ToArray())
        {
            if (g == null)
            {
                enemiesSpawned.Remove(g);
            }
        }
            
    }

    public void StartSpawning()
    {
        curRound = RoundManager.instance.roundNumber;
        if (factionSpawns)
        {
            enemiesToSpawn = (RoundManager.instance.totalEnemies / 2);
        }
        else
        {
            enemiesToSpawn = RoundManager.instance.totalEnemies;
        }
        
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
                firstEnemyCount = enemiesToSpawn * .7f;
                secondEnemyCount = enemiesToSpawn * .3f;


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

                firstEnemyCount = enemiesToSpawn * .4f;
                secondEnemyCount = enemiesToSpawn * .3f;
                thirdEnemyCount = enemiesToSpawn * .2f;
                fourthEnemyCount = enemiesToSpawn * .1f;


                firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                Debug.Log("First Enemy Count: " + firstEnemyCount);

                secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                Debug.Log("Second Enemy Count: " + secondEnemyCount);

                thirdEnemyCount = Mathf.FloorToInt(thirdEnemyCount);
                Debug.Log("Third Enemy Count: " + thirdEnemyCount);

                fourthEnemyCount = Mathf.FloorToInt(fourthEnemyCount);
                Debug.Log("Fourth Enemy Count: " + fourthEnemyCount);

                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    secondEnemyCount++;
                    Debug.Log("Second Enemy Added");
                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    thirdEnemyCount++;
                    Debug.Log("Third Enemy Added");

                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    firstEnemyCount++;
                    Debug.Log("First Enemy Added");

                }
                if (firstEnemyCount + secondEnemyCount + thirdEnemyCount + fourthEnemyCount != enemiesToSpawn)
                {
                    fourthEnemyCount++;
                    Debug.Log("Fourth Enemy Added");

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
                    firstEnemyCount = enemiesToSpawn * .4f;
                    secondEnemyCount = enemiesToSpawn * .3f;
                    thirdEnemyCount = enemiesToSpawn * .2f;
                    fourthEnemyCount = enemiesToSpawn * .1f;

                    firstEnemyCount = Mathf.FloorToInt(firstEnemyCount);
                    Debug.Log("First Enemy Count: " + firstEnemyCount);

                    secondEnemyCount = Mathf.FloorToInt(secondEnemyCount);
                    Debug.Log("Second Enemy Count: " + secondEnemyCount);

                    thirdEnemyCount = Mathf.FloorToInt(thirdEnemyCount);
                    Debug.Log("Third Enemy Count: " + thirdEnemyCount);

                    fourthEnemyCount = Mathf.FloorToInt(fourthEnemyCount);
                    Debug.Log("Fourth Enemy Count: " +  fourthEnemyCount);

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
            else if (curRound == 9) // This type of enemy count goes forever. Change later when end round is added plus more enemies.
            {
                firstEnemyCount = enemiesToSpawn * .4f;
                secondEnemyCount = enemiesToSpawn * .3f;
                thirdEnemyCount = enemiesToSpawn * .2f;
                fourthEnemyCount = enemiesToSpawn * .1f;

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

                Debug.Log("FINAL First Enemy Count: " + firstEnemyCount);

                Debug.Log("FINAL Second Enemy Count: " + secondEnemyCount);

                Debug.Log("FINAL Third Enemy Count: " + thirdEnemyCount);

                Debug.Log("FINAL Fourth Enemy Count: " + fourthEnemyCount);

                for (int i = 0; firstEnemyCount > i && spawnCount < RoundManager.instance.remainingEnemies; i++)
                {
                    if (!factionSpawns)
                    {
                        int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                        enemiesSpawned.Add(Instantiate(objectToSpawn[0], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation));
                        spawnCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                    else
                    {
                        if (firstFaction)
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[0], spawnPoint[0].position, spawnPoint[0].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                        else
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[0], spawnPoint[1].position, spawnPoint[1].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }

                }

                for (int i = 0; secondEnemyCount > i; i++)
                {
                    if (!factionSpawns)
                    {
                        int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                        enemiesSpawned.Add(Instantiate(objectToSpawn[1], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation));
                        spawnCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                    else
                    {
                        if (firstFaction)
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[1], spawnPoint[0].position, spawnPoint[0].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                        else
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[1], spawnPoint[1].position, spawnPoint[1].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
                }

                for (int i = 0; thirdEnemyCount > i; i++)
                {
                    if (!factionSpawns)
                    {
                        int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                        enemiesSpawned.Add(Instantiate(objectToSpawn[2], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation));
                        spawnCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                    else
                    {
                        if (firstFaction)
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[2], spawnPoint[0].position, spawnPoint[0].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                        else
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[2], spawnPoint[1].position, spawnPoint[1].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
                }

                for (int i = 0; fourthEnemyCount > i; i++)
                {
                    if (!factionSpawns)
                    {
                        int spawnPointChosen = Random.Range(0, spawnPoint.Length);
                        enemiesSpawned.Add(Instantiate(objectToSpawn[3], spawnPoint[spawnPointChosen].position, spawnPoint[spawnPointChosen].rotation));
                        spawnCount++;
                        yield return new WaitForSeconds(spawnInterval);
                    }
                    else
                    {
                        if (firstFaction)
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[3], spawnPoint[0].position, spawnPoint[0].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                        else
                        {
                            enemiesSpawned.Add(Instantiate(objectToSpawn[3], spawnPoint[1].position, spawnPoint[1].rotation));
                            spawnCount++;
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
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

            yield return new WaitForSeconds(5);

            if (EnemyTower.instance != null)
            {
                if (spawnCount >= enemiesToSpawn || EnemyTower.instance.towerDefeated)
                {
                    //enemiesSpawned.Clear();
                    break;
                }
            }
            else
            {
                if (spawnCount >= enemiesToSpawn)
                {
                    //enemiesSpawned.Clear();
                    break;
                }
            }
        }
    }
}
