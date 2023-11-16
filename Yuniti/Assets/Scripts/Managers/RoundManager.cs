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

    public int totalEnemies = 12;
    public GameObject startRoundUI;
    
    [SerializeField]
    public int remainingEnemies;

    public int countdownTime;
    public TMPro.TMP_Text countdownDisplay;

    public GameObject countdownPanel;

    [Header("Day&&Night")]
    public GameObject day;
    public GameObject night;

    // Start is called before the first frame update
    void Start()
    {
        remainingEnemies = totalEnemies;
        day.SetActive(true);
        night.SetActive(false);
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
        Conter();
        EndRound();
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
        GameManager.instance.inRound = true;
    }
    public void EndRound()
    {
        if(remainingEnemies <= 0)
        {

            day.SetActive(true);
            night.SetActive(false);
            GameManager.instance.inRound = false;
            if (runOnce)
            {
                canContinue = true;
                runOnce = false;
            }
            startRoundUI.SetActive(true);
            ResetCountdown();
          
        }
      
    }
}
