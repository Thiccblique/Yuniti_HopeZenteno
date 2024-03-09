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
    // Start is called before the first frame update
    void Start()
    {
        tower.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceUI.SetActive(true);
            if (GameManager.instance.coins >= price)
            {
                justEnoughTT.SetActive(true);
            }
            else
            {
                notEnoughTT.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                tower.SetActive(true);
                GameManager.instance.coins = GameManager.instance.coins - price;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceUI.SetActive(false);
            justEnoughTT.SetActive(false);
            notEnoughTT.SetActive(false);
        }
    }

}

