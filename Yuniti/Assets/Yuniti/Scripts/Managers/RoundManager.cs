using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private float countdownTimer = 0.0f;
    private bool isCountingDown = false;
    [SerializeField]
    public bool canContinue = true;
    public bool runOnce = true;
    public bool endRound = true;

    public int totalEnemies;
    public int roundNumber = 0;
    public int enemyIncreaseNum = 4;

    [Header("UIWork")]
    public GameObject startRoundUI;
    public GameObject inActionUI;
    public GameObject enemRemainUI;

    [SerializeField]
    public int remainingEnemies;

    public int countdownTime;
    public TMPro.TMP_Text countdownDisplay;

    public GameObject countdownPanel;

    [Header("Day&&Night")]
    public GameObject day;
    public GameObject night;

    [Header("EnemyTower")]
    public ParticleSystem enemyBuild;
   
    public bool activateTower = false;
    public GameObject enemyTower;
    

    [Header("Particals")]
    public GameObject partical1;
    public GameObject partical2;
    public GameObject partical3;
    public GameObject partical4;
    public GameObject partical5;
    public GameObject partical6;
    public GameObject partical7;
    public GameObject partical8;
    public GameObject partical9;
    public GameObject partical10;
    public GameObject partical11;
    public GameObject partical12;
    public GameObject partical13;

    [Header("FollowPlayer")]
    public FollowPlayer followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
        day.SetActive(true);
        FindAnyObjectByType<AudioManager>().Play("OverWorld");
        night.SetActive(false);
        TurnOff();
        endRound = false;
        enemRemainUI.SetActive(false);
        inActionUI.SetActive(false);
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
        //remainingEnemies = totalEnemies;
        Conter();
        EndRound();
        EnemyTowers();
    }

    private void Conter()
    {
        if (isCountingDown && canContinue)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0.1f)
            {
               
                runOnce = true;
            }
          
        }
    }

    public void StartRoundCountdown()
    {
        StartCoroutine(ActivateCountdown());
    }

    IEnumerator ActivateCountdown()
    {
        countdownPanel.SetActive(true);
        isCountingDown = true;

        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            canContinue = false;
            countdownTime--;
        }
        countdownDisplay.text = "go!";
        yield return new WaitForSeconds(0.5f);
        countdownPanel.SetActive(false);
        StartRound();
        
    }

    public void ResetCountdown()
    {
        isCountingDown = false;
        countdownTime = 3;
        
    }
    public void StartRound()
    {
        day.SetActive(false);
        night.SetActive(true);
        startRoundUI.SetActive(false);
        enemRemainUI.SetActive(true);
        inActionUI.SetActive(true);
        GameManager.instance.inRound = true;
        if (!EnemySpawner.instance.factionSpawns)
        {
            EnemySpawner.instance.StartSpawning();
        }
        else
        {
            EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
            foreach(EnemySpawner s in spawners)
            {
                s.StartSpawning();
            }
        }

        endRound = false;
        TurnOn();
        FindAnyObjectByType<AudioManager>().Stop("OverWorld");
        FindAnyObjectByType<AudioManager>().Play("InBattle");
        followPlayer.stopReset = false;

    }
    public void EndRound() // This is always running and messing with the starting round. Add boolean or move it from Update.
    {
        if(remainingEnemies <= 0 && EnemySpawner.enemiesSpawned.Count == 0)
        {

            GameManager.instance.hasStarted = false;
            day.SetActive(true);
            night.SetActive(false);
            endRound = true;
            TurnOff();
            enemyBuild.Stop();
           
            GameManager.instance.inRound = false;
            if (runOnce)
            {
                
                canContinue = true;
                runOnce = false;
                FindAnyObjectByType<AudioManager>().Play("OverWorld");
                FindAnyObjectByType<AudioManager>().Stop("InBattle");

            }
            startRoundUI.SetActive(true);
            enemRemainUI.SetActive(false);
            inActionUI.SetActive(false);
            ResetCountdown();
          
        }
      
    }
    
    public void EnemyTowers()
    {
        if (roundNumber == 9 && endRound == false)
        {
            enemyBuild.Play();
           
            activateTower = true;
        }
        if (activateTower == true && endRound == true)
        {
            enemyTower.SetActive(true);
            
        }
            
    }

    public void TurnOn()
    {
        partical1.SetActive(true);
        partical2.SetActive(true);
        partical3.SetActive(true);
        partical4.SetActive(true);
        partical5.SetActive(true);
        partical6.SetActive(true);
        partical7.SetActive(true);
        partical8.SetActive(true);
        partical9.SetActive(true);
        partical10.SetActive(true);
        partical11.SetActive(true);
        partical12.SetActive(true); 
        partical13.SetActive(true);
    }
    public void TurnOff()
    {
        partical1.SetActive(false);
        partical2.SetActive(false);
        partical3.SetActive(false);
        partical4.SetActive(false);
        partical5.SetActive(false);
        partical6.SetActive(false);
        partical7.SetActive(false);
        partical8.SetActive(false);
        partical9.SetActive(false);
        partical10.SetActive(false);
        partical11.SetActive(false);
        partical12.SetActive(false);
        partical13.SetActive(false);
    }
}
