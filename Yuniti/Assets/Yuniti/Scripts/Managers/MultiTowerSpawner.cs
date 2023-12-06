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
    public GameObject kunaiTower;
    public GameObject fireworkTower;
    public GameObject buildingLocationPrim;

    public bool multiDeactivate = true;

    // Start is called before the first frame update
    void Start()
    {
        tower1Hud.SetActive(false);
        tower2Hud.SetActive(false);
        multiDeactivate = false;
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
    }

    public void Deactivate()
    {
        tower1Hud.SetActive(false);
        tower2Hud.SetActive(false);
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
}
