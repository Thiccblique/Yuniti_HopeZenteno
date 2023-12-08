using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTowerSpawner : MonoBehaviour
{
    public static MultiTowerSpawner instance;

    public Hud hud;
    public Animator hudAnim;
    public GameObject tower1Hud;
    public GameObject tower2Hud;
    public GameObject buildingLocationPrim;
    private bool bought = true;

    [Header("Towers")]
    public GameObject kunaiTower;
    public GameObject fireworkTower;
    public GameObject bambooTower;
    public GameObject mineingTower;

    

    public bool multiDeactivate = true;

    // Start is called before the first frame update
    void Start()
    {
        tower1Hud.SetActive(false);
        tower2Hud.SetActive(false);
        multiDeactivate = false;
        bought = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudAnim.SetBool("open", true);
            tower1Hud.SetActive(true);
            tower2Hud.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudAnim.SetBool("open", false);
            tower1Hud.SetActive(false);
            tower2Hud.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (multiDeactivate)
        {
            Deactivate();
        }
        MakeMarkerGoByeBye();
    }

    public void Deactivate()
    {
        tower1Hud.SetActive(false);
        tower2Hud.SetActive(false);
        bought = true;
    }
    private void MakeMarkerGoByeBye()
    {
            if (GameManager.instance.inRound)
            {
                buildingLocationPrim.SetActive(false);
            }
            else if (GameManager.instance.inRound == false && !bought) 
            {
                buildingLocationPrim.SetActive(true);
            }
    }
    public void KunaiTowerClick()
    {
        kunaiTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
    public void FireworkTowerClick()
    {
        fireworkTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
    public void BambooTowerClick()
    {
        bambooTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
    public void MineingTowerClick()
    {
        mineingTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
}
