using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public static TowerHealth instance;

    public GameObject goPanel;
    public Slider healthBar;
    public int maxHealth;
    public int curHealth = 0;

    public int damageAmount = 1;

    public GameObject defendPoint;

    void Start()
    {
        curHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = curHealth;
        goPanel.SetActive(false);
    }

    void Update()
    {
        if (curHealth == 0)
        {
            goPanel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(8))
        {
            curHealth = maxHealth - damageAmount;
            healthBar.value = curHealth;
        }
    }

}
