using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStats : MonoBehaviour
{
    private bool[] hasActivated = new bool[4] { true, true, true, true};
    public GameObject[] arrows;
    public GameObject mask;
    public bool startedHud = false;
    private bool buyTower = false;
    public TowerSpawner defendingPoints;
    public OrionController orionController;
    public GameObject continueText;


    private void Start()
    {
        continueText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        ArrowManager(hasActivated);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(defendingPoints.spawnOne == true && FindAnyObjectByType<GameManager>().inRound == false)
        {
            startedHud = true;
        }
      
    }

    private void ArrowManager(bool[] hasActivated)
    {
        if (startedHud == true)
        {
            if (hasActivated[0] == true)
            {
                continueText.SetActive (true);
                orionController.inTutorial = true;
                arrows[0].SetActive(true);
                mask.SetActive(true);
                hasActivated[0] = false;
            }
            else if (hasActivated[1] == true)
            {
                if ((Input.GetMouseButtonDown(0)))
                {
                    arrows[0].SetActive(false);
                    arrows[1].SetActive(true);
                    hasActivated[1] = false;
                }
            }
            else if (hasActivated[2] == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    arrows[1].SetActive(false);
                    arrows[2].SetActive(true);
                    hasActivated[2] = false;
                }
            }
            else if (hasActivated[3] == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    arrows[2].SetActive(false);
                    mask.SetActive(false);
                    hasActivated[3] = false;
                    orionController.inTutorial = false;
                    Invoke("Deactivate", 1f);
                }
            }
            
        }

    }

    private void Deactivate()
    {
        continueText.SetActive(false);
    }
}
