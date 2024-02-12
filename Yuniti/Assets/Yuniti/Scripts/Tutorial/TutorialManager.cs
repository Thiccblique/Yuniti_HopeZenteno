using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    private bool[] hasActivated = new bool[5] { true, true, true, true, true };
    public GameObject[] arrows;
    public GameObject mask;
    public bool startedHud = false;
    private bool buyTower = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ArrowManager(hasActivated);
    }

    private void OnTriggerEnter(Collider other)
    {
        startedHud = true;
    }

    private void ArrowManager(bool[] hasActivated )
    {
        if(startedHud == true)
        {
            if (hasActivated[0] == true)
            {
                arrows[0].SetActive(true);
                mask.SetActive(true);
                hasActivated[0] = false;
            }
            else if (hasActivated[1] == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
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
                    
                    
                }
            }
        }
       
    }

   


}
