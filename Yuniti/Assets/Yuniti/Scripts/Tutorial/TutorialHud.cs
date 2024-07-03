using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialHud : MonoBehaviour
{
    public static TutorialHud instance;
    public LoadingScreen loadingScreen;
    private bool[] hasActivated = new bool[5] { true, true, true, true, true };
    private bool[] hasSpoken = new bool[5] { true, true, true, true, true };
    public GameObject[] arrows;
    public GameObject mask;
    public bool startedHud = false;
    public bool buyTower = false;
    public string sceneName;
    public OrionController orionController;
    public GameObject continueText;
    // Start is called before the first frame update
    private void Start()
    {
        continueText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ArrowManager(hasActivated);
        FinalTalk(hasSpoken);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(buyTower == false)
        {
            startedHud = true;
            buyTower = true;
        }
       
    }

    private void ArrowManager(bool[] hasActivated )
    {
        if(startedHud == true)
        {
            if (hasActivated[0] == true)
            {
               
                orionController.inTutorial = true;
                arrows[0].SetActive(true);
                mask.SetActive(true);
                hasActivated[0] = false;
            }
            else if (hasActivated[1] == true)
            {
               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    continueText.SetActive(true);
                    arrows[0].SetActive(false);
                    arrows[1].SetActive(true);
                    hasActivated[1] = false;
                }
            }
            else if (hasActivated[2] == true)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    arrows[1].SetActive(false);
                    arrows[2].SetActive(true);
                    hasActivated[2] = false;
                }
            }
            else if (hasActivated[3] == true)
            {
                if( Input.GetMouseButtonDown(0))
                {
                    arrows[2].SetActive(false);
                    arrows[3].SetActive(true);
                    hasActivated[3] = false;
                }
            }
            else if (hasActivated[4] == true)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    arrows[3].SetActive(false);
                    mask.SetActive(false);
                    hasActivated[4] = false;
                    startedHud = false;
                    orionController.inTutorial = false;
                    Invoke("Deactivate", 1f);
                }
            }
        }
       
    }

    private void FinalTalk(bool[] hasSpoken)
    {
        if (RoundManager.instance.roundNumber == 1 && RoundManager.instance.endRound == true && GameManager.instance.killCount > 1)
        {
           
            if (hasSpoken[0] == true)
            {
                continueText.SetActive(true);
                orionController.inTutorial = true;
                mask.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    hasSpoken[0] = false;
                }
               
            }
            else if (hasSpoken[1] == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mask.SetActive(false);
                    hasSpoken[1] = false;
                    orionController.inTutorial = false;
                    Invoke("Deactivate", 1f);

                }
            }
           
        }
        if (RoundManager.instance.roundNumber == 3 && RoundManager.instance.endRound == true && GameManager.instance.killCount > 13)
        {
            if (hasSpoken[2] == true)
            {
                continueText.SetActive(true);
                orionController.inTutorial = true;
                //mask.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    hasSpoken[2] = false;
                }

            }
            else if (hasSpoken[3] == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //loadingScreen.LoadScene(sceneName);
                    //orionController.inTutorial = false;
                    Invoke("Deactivate", 1f);
                    Invoke("Transfer", 0.5f);
                    hasSpoken[3] = false;
                    
                }
            }
            else if (hasSpoken[4] == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    loadingScreen.LoadScene(sceneName);
                }
            }
        }


    }

    private void Transfer()
    {
        loadingScreen.LoadScene(sceneName);
    }
    private void Deactivate()
    {
        continueText.SetActive(false);
    }


}
