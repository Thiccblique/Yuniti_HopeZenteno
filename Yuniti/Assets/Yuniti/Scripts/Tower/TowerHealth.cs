using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public static TowerHealth instance;

   
    public Slider healthBar;
    public int maxHealth = 10;
    public float curHealth = 0;

    //public int damageAmount = 1;
  
    /*public GameObject defendPoint;*/

    void Start()
    {
        curHealth = maxHealth;
        SetMaxHealth(maxHealth);
       
    }

    void Update()
    {
        SetHealth(curHealth);
        HealthBack();
       
    }
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    private float GetCurHealth()
    {
        return curHealth;
    }

    private void HealthBack()
    {
        float realTime = Time.deltaTime;
        if (RoundManager.instance.endRound == true && curHealth < maxHealth)
        {
           curHealth += realTime;
        }
        else
        {
            realTime = 0;
        }
       
        
       
      
    }



}
