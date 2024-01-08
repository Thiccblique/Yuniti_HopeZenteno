using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public GameObject pointer;
    public CameraZoom cameraZoom;

    [Header("Towers")]
    public GameObject kunaiTower;
    public GameObject fireworkTower;
    public GameObject bambooTower;
    public GameObject mineingTower;
    public GameObject dojo;
    public GameObject archer;

    

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
        if (other.CompareTag("Player") && GameManager.instance.inRound == false)
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
        TowerPointer();
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
    private void TowerPointer()
    {
        if (!bought && GameManager.instance.inRound == false)
        {
            if (cameraZoom.zoom <= cameraZoom.minZom)
            {
                pointer.SetActive(false);
            }
            else
            {
                pointer.SetActive(true);
            }
        }
        else
        {
            pointer.SetActive(false);
        }
    }
    public void KunaiTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        kunaiTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;

    }
    public void FireworkTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        fireworkTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
    public void BambooTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        bambooTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
    public void MineingTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        mineingTower.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }

    public void DojoTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        dojo.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }

    public void ArcherTowerClick()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
        archer.SetActive(true);
        buildingLocationPrim.SetActive(false);
        multiDeactivate = true;
    }
}
