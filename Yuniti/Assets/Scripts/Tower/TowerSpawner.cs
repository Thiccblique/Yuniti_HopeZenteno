using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public static TowerSpawner instance;

    [Header("This.Tower")]
    public GameObject currentTower;
    public GameObject locationMarker;
    public GameObject priceMarker;
    public GameObject transparentTower;
    public Transform spawnPoint; 
    public float spawnRadius = 3f;
    public int price = 2;


    [SerializeField]
    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTower.SetActive(false);
        transparentTower.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       SpawnTower();
    }

    private void SpawnObject()
    {
        Instantiate(currentTower, spawnPoint.position, spawnPoint.rotation);
        currentTower.SetActive(true);
        locationMarker.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasSpawned && GameManager.instance.coins >= price)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius);

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    SpawnObject();
                    
                    GameManager.instance.coins = GameManager.instance.coins - price;

                    // Break the loop to avoid spawning multiple objects simultaneously
                    hasSpawned = true; break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player") && !hasSpawned)
        {
            priceMarker.SetActive(true);
            transparentTower.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceMarker.SetActive(false);
            transparentTower.SetActive(false);
        }
    }
}
