using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllySpawner : MonoBehaviour
{
    public static AllySpawner instance;

    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public int numberOfSpawns = 5;
    public float spawnInterval = 2.0f;

    private int spawnCount = 0;
    private float spawnTimer = 0.0f;

    private TowerHealth tH;
    public float health;



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
        if (RoundManager.instance.canContinue == false)
        {
            WillSpawn();
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
