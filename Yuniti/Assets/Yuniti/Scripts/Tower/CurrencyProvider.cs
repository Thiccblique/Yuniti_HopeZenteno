using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProvider : MonoBehaviour
{
    public static CurrencyProvider instance;

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
        if (RoundManager.instance.endRound == true)
        {

            WillSpawn();

        }
        if (RoundManager.instance.endRound == false)
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
}
