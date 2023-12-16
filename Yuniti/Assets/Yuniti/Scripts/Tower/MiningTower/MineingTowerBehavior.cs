using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineingTowerBehavior : MonoBehaviour
{
    [Header("MiniLM")]
    public GameObject miniLM;


    public GameObject miniTower1;
    // Start is called before the first frame update
    void Start()
    {
        miniLM.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                miniTower1.SetActive(true);
            }
        }
    }


}
