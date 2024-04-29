using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public static TowerHealth instance;
    private Animator towerAnim;
   
    public Slider healthBar;
    public int maxHealth = 10;
    public float curHealth = 0;
    public bool died = false;

    //public int damageAmount = 1;
  
    /*public GameObject defendPoint;*/

    void Start()
    {
        curHealth = maxHealth;
        SetMaxHealth(maxHealth);
        towerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        SetHealth(curHealth);
        HealthBack();

        if (curHealth <= 0)
        {
            StartCoroutine(DestroyAnimation());
        }
       
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
           curHealth += (realTime * 2);
        }
        else
        {
            realTime = 0;
        }
       
    }

    IEnumerator DestroyAnimation()
    {
        
        towerAnim.SetBool("Destroy", true);
        yield return new WaitForSeconds(3.5f);
        towerAnim.SetBool("Destroy", false);
        //yield return null;
        died = true;
        
        
    }
}
