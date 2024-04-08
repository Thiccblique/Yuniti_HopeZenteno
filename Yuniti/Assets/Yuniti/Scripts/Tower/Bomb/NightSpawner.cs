using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSpawner : MonoBehaviour
{
    public GameObject tower;
    public GameObject justEnoughTT;
    public GameObject notEnoughTT;
    public int price;
    public GameObject priceUI;
    private bool hasBought = false;

    public CameraZoom cameraZoom;
    public GameObject locationMarker;
    public GameObject pointer;

    public GameObject stat;
    public Animator hudAnim;
    // Start is called before the first frame update
    void Start()
    {
        tower.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TowerPointer();
        
        if(GameManager.instance.inRound == false)
        {
            hasBought = false;
            tower.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.CompareTag("Player") && hasBought == false && GameManager.instance.inRound == true)
        {
            hudAnim.SetBool("open", true);
            stat.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                tower.SetActive(true);
                GameManager.instance.coins = GameManager.instance.coins - price;
                hasBought = true;
                FindAnyObjectByType<AudioManager>().Play("Buy");
                FindAnyObjectByType<AudioManager>().Play("Building");
                GameManager.instance.score += 999;

            }
            priceUI.SetActive(true);
            if (GameManager.instance.coins >= price)
            {
                justEnoughTT.SetActive(true);
            }
            else
            {
                notEnoughTT.SetActive(true);
            }

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudAnim.SetBool("open", false);
            priceUI.SetActive(false);
            justEnoughTT.SetActive(false);
            notEnoughTT.SetActive(false);
            stat.SetActive(false);
        }
    }

    private void TowerPointer()
    {
        if (!hasBought && GameManager.instance.inRound == true)
        {
            locationMarker.SetActive(true);
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
            locationMarker.SetActive(false);
        }
    }

}

