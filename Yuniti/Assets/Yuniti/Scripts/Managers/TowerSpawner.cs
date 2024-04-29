using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Data;
using UnityEngine.SocialPlatforms.Impl;

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

    [Header("Upgrade UI")]
    public GameObject firstUpgrade;
    public GameObject secondUpgrade;
    public GameObject thirdUpgrade;

    [Header("Price")]
    public int priceOne = 0;
    public int priceTwo = 0;
    public int priceThree = 0;

    [Header("TowerHealth")]
    public TowerHealth health1;
    public TowerHealth health2;
    public TowerHealth health3;

    [Header("Destroyed")]
    public GameObject[] destroyed;
    
    [Header("Range")]
    public GameObject[] tRange;
    public GameObject[] range;

    [Header("Other Mechanics")]
    public ParticleSystem buildPartical;
    private TowerHealth towerHealth;
    public GameObject pointer;
    public CameraZoom cameraZoom;
    public Transform spawnPoint;
    public float spawnRadius = 3f;
    public int price;
    private bool paidFor = true;
    public Animator hudAnim;

    //[SerializeField]
    public bool hasSpawned = false;
    private bool maxedOut = true;

    // Start is called before the first frame update
    void Start()
    {
        price = priceOne;

        spawnOne = false;
        spawnTwo = false;
        spawnThree = false;
        maxedOut = false;
        towerStageOne.SetActive(false);
        notEnoughTTOne.SetActive(false);
        paidFor = false;
        
        firstUpgrade.SetActive(false);
        secondUpgrade.SetActive(false);
        thirdUpgrade.SetActive(false);

        pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!maxedOut)
        {
            SpawnTower();
        }
        
        MakeMarkerGoByeBye();
        TowerPointer();
        RangeShower();
        TowerHealth();
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
                    buildPartical.Play();
                    hasSpawned = true;
                    GameManager.instance.coins = GameManager.instance.coins - price;
                    SpawnObject();
                    FindAnyObjectByType<AudioManager>().Play("Buy");
                    FindAnyObjectByType<AudioManager>().Play("Building");


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

    private void TowerHealth()
    {
        if (health1.died == true)
        {
            if (towerStageOne.GetComponent<AllySpawner>() != null)
            {
                GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
                foreach (GameObject a in allies)
                    Destroy(a);
            }
            destroyed[0].SetActive(true);
            towerStageOne.SetActive(false);
            spawnOne = false;
            price = priceOne;
            health1.curHealth = health1.maxHealth;
        }
        if (health2.died == true)
        {
            if (towerStageTwo.GetComponent<AllySpawner>() != null)
            {
                GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
                foreach (GameObject a in allies)
                    Destroy(a);
            }
            destroyed[1].SetActive(true);
            towerStageTwo.SetActive(false);
            spawnTwo = false;
            spawnOne = false;
            health2.curHealth = health2.maxHealth;
            price = priceOne;

        }
        if (health3.died == true)
        {
            if (towerStageThree.GetComponent<AllySpawner>() != null)
            {
                GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
                foreach (GameObject a in allies)
                    Destroy(a);
            }
            destroyed[2].SetActive(true);
            towerStageThree.SetActive(false);
            spawnThree = false;
            spawnTwo = false;
            spawnOne = false;
            health3.curHealth = health3.maxHealth;
            price = priceOne;
            maxedOut = false;
        }
        
    }
    private void SpawnObject()
    {
        if(!maxedOut)
        {
            if (spawnOne == false)
            {
                health1.died = false;
                health2.died = false;
                health3.died = false;
                Instantiate(towerStageOne, spawnPoint.position, spawnPoint.rotation);
                towerStageOne.SetActive(true);
                destroyed[0].SetActive(false);
                destroyed[1].SetActive(false);
                destroyed[2].SetActive(false);
                locationMarkerOne.SetActive(false);
                spawnOne = true;
                hasSpawned = false;
                locationMarkerTwo.SetActive(true);
                price = priceTwo;
                GameManager.instance.score += 156;
            }
            else if (!spawnTwo)
            {
                health2.died = false;
                health3.died = false;
                towerStageOne.SetActive(false);
                destroyed[0].SetActive(false);
                Instantiate(towerStageTwo, spawnPoint.position, spawnPoint.rotation);
                towerStageTwo.SetActive(true);
                destroyed[1].SetActive(false);
                destroyed[2].SetActive(false);
                locationMarkerTwo.SetActive(false);
                spawnTwo = true;
                hasSpawned = false;
                locationMarkerThree.SetActive(true);
                price = priceThree;
                GameManager.instance.score += 1796;
            }
            else if (!spawnThree)
            {
                health3.died = false;
                towerStageTwo.SetActive(false);
                destroyed[0].SetActive(false);
                destroyed[1].SetActive(false);
                destroyed[2].SetActive(false);
                Instantiate(towerStageThree, spawnPoint.position, spawnPoint.rotation);
                towerStageThree.SetActive(true);
                locationMarkerThree.SetActive(false);
                spawnThree = true;
                hasSpawned = false;
                maxedOut = true;
                GameManager.instance.score += 4999;
            }
            
              
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") && !hasSpawned && GameManager.instance.inRound == false)
        {
            hudAnim.SetBool("open", true);
            if (!spawnOne)
            {
                priceMarkerOne.SetActive(true);
                //range[0].SetActive(true);
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTTOne.SetActive(true);
                }
                else
                {
                    notEnoughTTOne.SetActive(true);
                }
                firstUpgrade.SetActive(true);
            }
            else if (!spawnTwo)
            {
                priceMarkerTwo.SetActive(true);
                //range[1].SetActive(true);        
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTT_Two.SetActive(true);
                }
                else
                {
                    notEnoughTT_Two.SetActive(true);
                }
                secondUpgrade.SetActive(true);
            }
            else if (!spawnThree)
            {
                priceMarkerThree.SetActive(true);
                //range[2].SetActive(true);
                if (GameManager.instance.coins >= price)
                {
                    justEnoughTT_Three.SetActive(true);
                }
                else
                {
                    notEnoughTT_Three.SetActive(true);
                }
                thirdUpgrade.SetActive(true);
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

    private void RangeShower()
    {
        if (spawnThree)
        {
            if (cameraZoom.zoom <= cameraZoom.minZom)
            {
                range[2].SetActive(false);
            }
            else
            {
                range[2].SetActive(true);
            }
        }
        else if (spawnTwo)
        {
            if (cameraZoom.zoom <= cameraZoom.minZom)
            {
                range[1].SetActive(false);
            }
            else
            {
                range[1].SetActive(true);
            }
        }
        else if (spawnOne)
        {
            if (cameraZoom.zoom <= cameraZoom.minZom)
            {
                range[0].SetActive(false);
            }
            else
            {
                range[0].SetActive(true);
            }
        }
    }

    private void TowerPointer()
    {
        if (!spawnOne && GameManager.instance.inRound == false)
        {
            if (cameraZoom.zoom <= cameraZoom.minZom)
            {
                pointer.SetActive(false);
            }
            else
            {
                pointer.SetActive(true);
            }
        }
        else
        {
            pointer.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            hudAnim.SetBool("open", false);
            priceMarkerOne.SetActive(false);
            justEnoughTTOne.SetActive(false);
            notEnoughTTOne.SetActive(false);
            priceMarkerTwo.SetActive(false);
            justEnoughTT_Two.SetActive(false);
            notEnoughTT_Two.SetActive(false);
            priceMarkerThree.SetActive(false);
            justEnoughTT_Three.SetActive(false);
            notEnoughTT_Three.SetActive(false);

            firstUpgrade.SetActive(false);
            secondUpgrade.SetActive(false);
            thirdUpgrade.SetActive(false);

            range[0].SetActive(false);
            range[1].SetActive(false);
            range[2].SetActive(false);

        }
    }
}
