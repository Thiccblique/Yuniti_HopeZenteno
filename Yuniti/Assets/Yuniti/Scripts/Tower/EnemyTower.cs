using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public static EnemyTower instance;

    public float towerCurHealth = 0;
    public float towerMaxHealth = 10;

    public bool towerDefeated = false;

    public GameObject enemyTower;

    public IsometricController player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        towerCurHealth = towerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (towerCurHealth <= 0)
        {
            towerDefeated = true;
            enemyTower.SetActive(false);
            RoundManager.instance.remainingEnemies = 0;
            DespawnEnemies();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RoundManager.instance.roundNumber == 10)
        {
            if (other.gameObject.CompareTag("HitBox"))
            {
                towerCurHealth -= player.attackDamage;

            }
            if (other.gameObject.CompareTag("SwordHitBox"))
            {
                towerCurHealth -= player.swordDamage;
            }
        }
    }

    public void DespawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
