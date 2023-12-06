using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public static TowerSpawner instance;

    [Header("Towers")]
    public GameObject towerStageOne;
    public GameObject towerStageTwo;
    public GameObject towerStageThree;


    [Header("TT")]
    public GameObject notEnoughTTOne;
    public GameObject justEnoughTTOne;
    public GameObject notEnoughTT_Two;
    public GameObject justEnoughTT_Two;
    public GameObject notEnoughTT_Three;
    public GameObject justEnoughTT_Three;


    [Header("Tower Booleans")]
    public bool spawnOne = true;
    public bool spawnTwo = true;
    public bool spawnThree = true;

    [Header("Location Markers")]
    public GameObject locationMarkerOne;
    public GameObject locationMarkerTwo;
    public GameObject locationMarkerThree;


    [Header("Price Markers")]
    public GameObject priceMarkerOne;
    public GameObject priceMarkerTwo;
    public GameObject priceMarkerThree;

    [Header("Other Mechanics")]
    public Transform spawnPoint;
    public float spawnRadius = 3f;
    public int price = 2;
    private bool paidFor = true;

    [SerializeField]
    public bool hasSpawned = false;
    private bool maxedOut = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnOne = false;
        spawnTwo = false;
        spawnThree = false;
        maxedOut = false;
        towerStageOne.SetActive(false);
        notEnoughTTOne.SetActive(false);
        paidFor = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTower();
        MakeMarkerGoByeBye();
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasSpawned && GameManager.instance.coins >= price && GameManager.instance.inRound == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius);

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    hasSpawned = true;
                    GameManager.instance.coins = GameManager.instance.coins - price;
                    SpawnObject();

                }
            }
        }

        if (RoundManager.instance.remainingEnemies <= 0 && hasSpawned)
        {
            towerStageOne.SetActive(true);
            //TowerHealth.instance.curHealth = TowerHealth.instance.maxHealth;
        }
    }
    public void SpawnTowerOnClick()
    {
        if (GameManager.instance.coins >= price)
        {
            MultiTowerSpawner.instance.multiDeactivate = true;
            hasSpawned = true;
            GameManager.instance.coins = GameManager.instance.coins - price;
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        if(!maxedOut)
        {
            if (!spawnOne)
            {
                Instantiate(towerStageOne, spawnPoint.position, spawnPoint.rotation);
                towerStageOne.SetActive(true);
                locationMarkerOne.SetActive(false);
                spawnOne = true;
                hasSpawned = false;
                locationMarkerTwo.SetActive(true);
                price = 7;
            }
            else if (!spawnTwo)
            {

                towerStageOne.SetActive(false);
                Instantiate(towerStageTwo, spawnPoint.position, spawnPoint.rotation);
                towerStageTwo.SetActive(true);
                locationMarkerTwo.SetActive(false);
                spawnTwo = true;
                hasSpawned = false;
                locationMarkerThree.SetActive(true);
                price = 15;
            }
            else if (!spawnThree)
            {

                towerStageTwo.SetActive(false);
                Instantiate(towerStageThree, spawnPoint.position, spawnPoint.rotation);
                towerStageThree.SetActive(true);
                locationMarkerThree.SetActive(false);
                spawnThree = true;
                hasSpawned = false;
            }
            else
                maxedOut = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !hasSpawned && GameManager.instance.inRound == false)
        {
            if (!spawnOne)
            {
                priceMarkerOne.SetActive(true);
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTTOne.SetActive(true);
                }
                else
                {
                    notEnoughTTOne.SetActive(true);
                }
            }
            else if (!spawnTwo)
            {
                priceMarkerTwo.SetActive(true);
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTT_Two.SetActive(true);
                }
                else
                {
                    notEnoughTT_Two.SetActive(true);
                }
            }
            else if (!spawnThree)
            {
                priceMarkerThree.SetActive(true);
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTT_Three.SetActive(true);
                }
                else
                {
                    notEnoughTT_Three.SetActive(true);
                }
            }


        }
    }

    private void MakeMarkerGoByeBye()
    {
        if (!spawnOne)
        {
            if (GameManager.instance.inRound)
            {
                locationMarkerOne.SetActive(false);
            }
            else if (GameManager.instance.inRound == false)
            {
                locationMarkerOne.SetActive(true);
            }
        }
        else if (!spawnTwo)
        {
            if (GameManager.instance.inRound)
            {
                locationMarkerTwo.SetActive(false);
            }
            else if (GameManager.instance.inRound == false)
            {
                locationMarkerTwo.SetActive(true);
            }
        }
        else if (!spawnThree)
        {
            if (GameManager.instance.inRound)
            {
                locationMarkerThree.SetActive(false);
            }
            else if (GameManager.instance.inRound == false)
            {
                locationMarkerThree.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceMarkerOne.SetActive(false);
            justEnoughTTOne.SetActive(false);
            notEnoughTTOne.SetActive(false);
            priceMarkerTwo.SetActive(false);
            justEnoughTT_Two.SetActive(false);
            notEnoughTT_Two.SetActive(false);
            priceMarkerThree.SetActive(false);
            justEnoughTT_Three.SetActive(false);
            notEnoughTT_Three.SetActive(false);
        }
    }
}
