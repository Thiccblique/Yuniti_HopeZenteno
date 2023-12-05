using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTowerButtonVisualizer : MonoBehaviour
{
    public GameObject towerGreenTT;
    public GameObject towerRedTT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (TowerSpawner.instance.spawnOne == false)
        {
            if (GameManager.instance.coins >= TowerSpawner.instance.price)
            {
                towerGreenTT.SetActive(true);
            }
            else
            {
                towerRedTT.SetActive(true);
            }
        }
       
    }

    private void OnMouseExit()
    {
        
        towerGreenTT.SetActive(false);
        towerRedTT.SetActive(false);
       
    }
}
