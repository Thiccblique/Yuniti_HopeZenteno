using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public static TowerHealth instance;

    public int curHealth = 0;
    public int maxHealth = 100;

    // Start is called before the first frame update
    void Awake()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
        {
            curHealth = 0;
        }
    }
}
