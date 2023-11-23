using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Scripts")]
    public TowerSpawner defendingPoints;
    
    [Header("HUD")]
    public GameObject hudCanvas;

    [SerializeField]
    public bool readyForUpgradeII = false;
    public bool readyForUpgradeIII = false;
    public bool inRound = true;

    [Header("FirstRound")]
    public GameObject round1TowerOne;
    public GameObject round1TowerTwo;

    [Header("SecondRound")]
    public GameObject round2TowerOne;
    public GameObject round2TowerTwo;
    public GameObject round2TowerThree;
    public GameObject round2TowerFour;
    public GameObject round2TowerFive;
    public GameObject round2TowerSix;
    public GameObject round2TowerSeven;

    [Header("ThirdRound")]
    public GameObject round3TowerOne;
    public GameObject round3TowerTwo;
    /*public GameObject round3TowerThree;
    public GameObject round3TowerFour;
    public GameObject round3TowerFive; */

    [Header("Money")]
    public int coins = 5;
    public int itemPrice = 2;
    public bool towerIsLocked = true;
    public TMPro.TMP_Text coinsAmount;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Disability();
        CheckCoins();
        inRound = false;
    }

    public void CheckCoins()
    {
        coinsAmount.text = coins.ToString();
    }

    public void DisplayItemPrice()
    {
        
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
        CheckCoins();
        DisplayItemPrice();
    }

    private void Spawner()
    {
        if(defendingPoints.spawnOne == true)
        {
            hudCanvas.SetActive(true);
            round1TowerOne.SetActive(true);
            round1TowerTwo.SetActive(true);

                if (defendingPoints.spawnTwo)
                {
                    round2TowerOne.SetActive(true);
                    round2TowerTwo.SetActive(true);
                    round2TowerThree.SetActive(true);
                    round2TowerFour.SetActive(true);
                    round2TowerFive.SetActive(true);
                    round2TowerSix.SetActive(true);
                    round2TowerSeven.SetActive(true);
                  
                }
                if (defendingPoints.spawnThree)
                {
                    round3TowerOne.SetActive(true);
                    round3TowerTwo.SetActive(true);
                }
                

            
        }

    }
    private void Disability()
    {
        round1TowerOne.SetActive(false);       
        round1TowerTwo.SetActive(false);

        round2TowerOne.SetActive(false);
        round2TowerTwo.SetActive(false);
        round2TowerThree.SetActive(false);
        round2TowerFour.SetActive(false);
        round2TowerFive.SetActive(false);
        round2TowerSix.SetActive(false);
        round2TowerSeven.SetActive(false);
      
        round3TowerOne.SetActive(false);
        round3TowerTwo.SetActive(false);
        /*round3TowerThree.SetActive(false);
        round3TowerFour.SetActive(false);
        round3TowerFive.SetActive(false);*/


    }

    public void RoundStart()
    {
        RoundManager.instance.StartRoundCountdown();
        RoundManager.instance.totalEnemies = 12;
        RoundManager.instance.remainingEnemies = RoundManager.instance.totalEnemies;
       
        
    }
}
