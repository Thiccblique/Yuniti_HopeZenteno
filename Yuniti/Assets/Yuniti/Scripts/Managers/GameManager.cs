using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;
using System.IO.Compression;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    private int scoreLength = 4;
    public int score;
    public TMPro.TMP_Text[] scoreCount;


    [Header("CircleFade")]
    public GameObject circleFade;

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
    public PlayerHealth orion;
    public PlayerHealth gamble;
    //private EnemyBehaviour enemy;

    [Header("Enemys")]
    public EnemyBehaviour standardBehaviour;
    public EnemyBehaviour flyBehaviour;
    public SpiderBehaviour spiderBehaviour;
    public EnemyBehaviour tankBehaviour;
    public CatapultBehaviour catapultBehaviour;
    public RomanBehaviour[] romanBehaviours;

    [Header("HUD")]
    public GameObject hudCanvas;
    public GameObject gameOver;

    [SerializeField]
    public bool readyForUpgradeII = false;
    public bool readyForUpgradeIII = false;
    public bool inRound = true;
    public GameObject player;
    public bool hasStarted = true;

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
    public TMPro.TMP_Text[] coinsCount;
    public TMPro.TMP_Text[] enemyCount;
    public TMPro.TMP_Text[] roundCount;
    

    [Header("Money")]
    public float coins = 5f;
    private int itemPrice = 2;
    public bool towerIsLocked = true;
    

    [Header("Dialouge")]
    public GameObject[] dialougeBox;

    [HideInInspector]
    public int killCount;
    public bool hasRan = true;
    public bool isRan = true;

    [Header("Filters")]
    public GameObject dayPP;
    public GameObject nightPP;
    public GameObject darkPost;

    private bool end = false;

    private KeyCode[] konamiCodeSequence = {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A

    };
    public int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        //enemy = GetComponent<EnemyBehaviour>();
        CirclePlay();
        Invoke("CircleDeactive", 2.0f);
        Disability();
        CodeTooUI();
        hudCanvas.SetActive(false);
        hasRan = false;
        isRan = false;
        hasStarted = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(konamiCodeSequence[currentIndex]))
        {
            KonamiCode();
        }
        else if (Input.anyKeyDown != Input.GetKeyDown(konamiCodeSequence[currentIndex]))
        {
            currentIndex = 0;
        }

        Spawner();
        CodeTooUI();
        DialogueActivator();
        GameOver();
        if(end == false)
        {
            ScoreTooUI();
        }
        

    }

    public void CodeTooUI()
    {
        
        coinsCount[0].text = coins.ToString();
        enemyCount[0].text = RoundManager.instance.remainingEnemies.ToString();
        roundCount[0].text = RoundManager.instance.roundNumber.ToString();
        /****************************************************************************/
        coinsCount[1].text = coins.ToString();
        enemyCount[1].text = RoundManager.instance.remainingEnemies.ToString();
        roundCount[1].text = RoundManager.instance.roundNumber.ToString();
        /****************************************************************************/
        roundCount[2].text = RoundManager.instance.roundNumber.ToString();
    }

    private void ScoreTooUI()
    {
        if (score <= 9)
        {
            scoreCount[0].text = "0000000" + score.ToString();
            scoreCount[1].text = "0000000" + score.ToString();
        }
        else if (score <= 99)
        {
            scoreCount[0].text = "000000" + score.ToString();
            scoreCount[1].text = "000000" + score.ToString();
        }
        else if (score <= 999)
        {
            scoreCount[0].text = "00000" + score.ToString();
            scoreCount[1].text = "00000" + score.ToString();
        }
        else if (score <= 9999)
        {
            scoreCount[0].text = "0000" + score.ToString();
            scoreCount[1].text = "0000" + score.ToString();
        }
        else if (score <= 99999)
        {
            scoreCount[0].text = "000" + score.ToString();
            scoreCount[1].text = "000" + score.ToString();
        }
        else if (score <= 999999)
        {
            scoreCount[0].text = "00" + score.ToString();
            scoreCount[1].text = "00" + score.ToString();
        }
        else if (score <= 9999999)
        {
            scoreCount[0].text = "0" + score.ToString();
            scoreCount[1].text = "0" + score.ToString();
        }
        else if (score <= 99999999)
        {
            scoreCount[0].text = "" + score.ToString();
            scoreCount[1].text = "" + score.ToString();
        }
       
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


    private void EnemyStats()
    {
        //Standart Enemy
        standardBehaviour.damageAmount += 1f;
        standardBehaviour.maxHealth += 2f;

        //Flying Enemy
        flyBehaviour.damageAmount += 1f;
        flyBehaviour.maxHealth += 2f;

        //FilmRed Spider
        spiderBehaviour.damageAmount += 1f;
        spiderBehaviour.maxHealth += 2f;

        //Tank Behaviour
        tankBehaviour.damageAmount += 3f;  
        tankBehaviour.maxHealth += 10f;

        //And Player? lol 
        pController.attackDamage += 1f;
        pController.swordDamage += 0.7f;

        //Catapult
        //catapultBehaviour.damageAmount += 1.5f;
        //catapultBehaviour.maxHealth += 3f;

        foreach (RomanBehaviour romanBehaviour  in romanBehaviours)
        {
            if(romanBehaviour != null)
            {
                romanBehaviour.damageAmount += 1f;
                romanBehaviour.maxHealth += 2f;
            }
            else
            {
                Debug.Log("Don't have anything set");
            }
        }


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
        if (dialougeBox[1] != null)
        {
            if (defendingPoints.spawnOne == true)
            {
                dialougeBox[1].SetActive(true);
            }
        }
        if (dialougeBox[2] != null)
        {
            if (RoundManager.instance.roundNumber == 3 && RoundManager.instance.endRound == true && killCount > 13 && !isRan)
            {
                dialougeBox[2].SetActive(true);
                isRan = true;
            }
        }
    }

    public void GameOver()
    {
        if (gamble.curHealth <= 0 || orion.curHealth <= 0 || defendingPointHP1.curHealth <= 0 || defendingPointHP2.curHealth <= 0 || defendingPointsHP3.curHealth <= 0)
        {
            player.SetActive(false);
            gameOver.SetActive(true);
            pController.PlayerStop();
            end = true;
        }
    }

    public void RoundStart()
    {
        if (!hasStarted)
        {
            RoundManager.instance.StartRoundCountdown();
            RoundManager.instance.totalEnemies += RoundManager.instance.enemyIncreaseNum;
            RoundManager.instance.remainingEnemies = RoundManager.instance.totalEnemies;
            RoundManager.instance.roundNumber++;
            score += 10000;
            EnemyStats();
            hasStarted = true;
        }

    }

    public void KonamiCode()
    {
        currentIndex++;

        if (currentIndex >= konamiCodeSequence.Length)
        {
            coins = 9999999999999999999999f;
            Debug.Log("Konami Code activated!");
            currentIndex = 0;
        }
    }

    private void CirclePlay()
    {
        Animator animator = circleFade.GetComponent<Animator>();
        animator.Play("Fadeout");
        
    }
    private void CircleDeactive()
    {
        circleFade.SetActive(false);
    }

    public void DarkProcess()
    {
        if (darkPost.activeInHierarchy == false)
        {
            darkPost.SetActive(true);
        }
        else
        {
            darkPost.SetActive(false);
        }

    }
}
