using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Monted Orion")]
    public MeshRenderer[] orionBodyPartsMounted;
    
    [Header("Free Orion")]
    public MeshRenderer[] orionBodyPartsFree;

    [Header("Scripts")]
    public IsometricController pController;
    public TowerSpawner defendingPoints;
    public TowerHealth defendingPointHP1;
    public TowerHealth defendingPointHP2;
    public TowerHealth defendingPointsHP3;
    //private EnemyBehaviour enemy;

    [Header("Enemys")]
    public EnemyBehaviour standardBehaviour;
    public EnemyBehaviour flyBehaviour;
    public SpiderBehaviour spiderBehaviour;
    public EnemyBehaviour tankBehaviour;

    [Header("HUD")]
    public GameObject hudCanvas;
    public GameObject gameOver;

    [SerializeField]
    public bool readyForUpgradeII = false;
    public bool readyForUpgradeIII = false;
    public bool inRound = true;
    public GameObject player;

    [Header("FirstRound")]
    public GameObject round1TowerOne;
    public GameObject round1TowerTwo;
    public GameObject round1TowerThree;
    public GameObject round1TowerFour;
    public GameObject round1TowerFive;
    public GameObject round1TowerSix;

    [Header("SecondRound")]
    public GameObject round2TowerOne;
    public GameObject round2TowerTwo;
    public GameObject round2TowerThree;
    public GameObject round2TowerFour;
    public GameObject round2TowerFive;
    public GameObject round2TowerSix;
    public GameObject round2TowerSeven;
    public GameObject round2TowerEight;

    [Header("ThirdRound")]
    public GameObject round3TowerOne;
    public GameObject round3TowerTwo;
    public GameObject round3TowerThree;
    public GameObject round3TowerFour;
    public GameObject round3TowerFive;
    public GameObject rounde3TowerSix;

    [Header("UI")]
    public TMPro.TMP_Text coinsCount;
    public TMPro.TMP_Text enemyCount;
    public TMPro.TMP_Text roundCount;

    [Header("Money")]
    public float coins = 5f;
    public int itemPrice = 2;
    public bool towerIsLocked = true;

    [Header("Dialouge")]
    public GameObject[] dialougeBox;

    [HideInInspector]
    public int killCount;
    private bool hasRan = true;
    




    // Start is called before the first frame update
    void Start()
    {
        //enemy = GetComponent<EnemyBehaviour>();
      
        Disability();
        CodeTooUI();
        hudCanvas.SetActive(false);
        hasRan = false;
    }

    public void CodeTooUI()
    {
        coinsCount.text = coins.ToString();
        enemyCount.text = RoundManager.instance.remainingEnemies.ToString();
        roundCount.text = RoundManager.instance.roundNumber.ToString();
    }

    public void DisableAllMeshMounted()
    {
        foreach (MeshRenderer meshRenderer in orionBodyPartsMounted)
        {
            // Check if the MeshRenderer is not null
            if (meshRenderer != null)
            {
                // Disable the MeshRenderer
                meshRenderer.enabled = false;
            }
            else
            {
                // Log a warning if a MeshRenderer in the array is null
                Debug.LogWarning("Null MeshRenderer in the array");
            }
        }
    }

    public void EnableAllMeshMounted()
    {
        foreach (MeshRenderer meshRenderer in orionBodyPartsMounted)
        {
            // Check if the MeshRenderer is not null
            if (meshRenderer != null)
            {
                // Disable the MeshRenderer
                meshRenderer.enabled = true;
            }
            else
            {
                // Log a warning if a MeshRenderer in the array is null
                Debug.LogWarning("Null MeshRenderer in the array");
            }
        }
    }

    public void UnlockItem()
    {
        if (towerIsLocked == true)
        {
            if (coins >= itemPrice)
            {
                coins -= itemPrice;

                round1TowerTwo.SetActive(true);

                towerIsLocked = false;

            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spawner();
        CodeTooUI();
        DialogueActivator();
        GameOver();
    }

    private void EnemyStats()
    {
        //Standart Enemy
        standardBehaviour.damageAmount += 1.5f;
        standardBehaviour.maxHealth += 2f;

        //Flying Enemy
        flyBehaviour.damageAmount += 1.5f;
        flyBehaviour.maxHealth += 2f;
       
        //FilmRed Spider
        spiderBehaviour.damageAmount += 1.5f;
        spiderBehaviour.maxHealth += 2f;

        //Tank Behaviour
        tankBehaviour.damageAmount += 4f;
        tankBehaviour.maxHealth += 10f;

        //And Player? lol 
        pController.attackDamage += 1f;


    }

    private void Spawner()
    {
        if (defendingPoints.spawnOne == true)
        {
            hudCanvas.SetActive(true);
            round1TowerOne.SetActive(true);
            round1TowerTwo.SetActive(true);
            round1TowerThree.SetActive(true);
            round1TowerFour.SetActive(true);
            round1TowerFive.SetActive(true);
            round1TowerSix.SetActive(true);

            if (defendingPoints.spawnTwo)
            {
                round2TowerOne.SetActive(true);
                round2TowerTwo.SetActive(true);
                round2TowerThree.SetActive(true);
                round2TowerFour.SetActive(true);
                round2TowerFive.SetActive(true);
                round2TowerSix.SetActive(true);
                round2TowerSeven.SetActive(true);
                round2TowerEight.SetActive(true);

            }
            if (defendingPoints.spawnThree)
            {
                round3TowerOne.SetActive(true);
                round3TowerTwo.SetActive(true);
                round3TowerThree.SetActive(true);
                round3TowerFour.SetActive(true);
                round3TowerFive.SetActive(true);
                rounde3TowerSix.SetActive(true);
            }
        }

    }
    private void Disability()
    {
        round1TowerOne.SetActive(false);
        round1TowerTwo.SetActive(false);
        round1TowerThree.SetActive(false);
        round1TowerFour.SetActive(false);
        round1TowerFive.SetActive(false);
        round1TowerSix.SetActive(false);

        round2TowerOne.SetActive(false);
        round2TowerTwo.SetActive(false);
        round2TowerThree.SetActive(false);
        round2TowerFour.SetActive(false);
        round2TowerFive.SetActive(false);
        round2TowerSix.SetActive(false);
        round2TowerSeven.SetActive(false);
        round2TowerEight.SetActive(false);

        round3TowerOne.SetActive(false);
        round3TowerTwo.SetActive(false);
        round3TowerThree.SetActive(false);
        round3TowerFour.SetActive(false);
        round3TowerFive.SetActive(false);
        rounde3TowerSix.SetActive(false);


    }

    private void DialogueActivator()
    {
        if (RoundManager.instance.roundNumber == 1 && RoundManager.instance.endRound == true && killCount > 1 && !hasRan)
        {
            dialougeBox[0].SetActive(true);
            hasRan = true;

        }
    }

    public void GameOver()
    {
        if (defendingPointHP1.curHealth <= 0 || defendingPointHP2.curHealth <= 0 || defendingPointsHP3.curHealth <= 0)
        {
            player.SetActive(false);
            gameOver.SetActive(true);
            pController.PlayerStop();
        }
    }

    public void RoundStart()
    {
        RoundManager.instance.StartRoundCountdown();
        RoundManager.instance.totalEnemies += RoundManager.instance.enemyIncreaseNum;
        RoundManager.instance.remainingEnemies = RoundManager.instance.totalEnemies;
        RoundManager.instance.roundNumber++;
        EnemyStats();
    }


}
