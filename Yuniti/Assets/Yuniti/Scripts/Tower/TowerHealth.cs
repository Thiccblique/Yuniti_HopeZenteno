using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public static TowerHealth instance;

    public GameObject goPanel;
    public Slider healthBar;
    public int maxHealth = 5;
    public int curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
}
