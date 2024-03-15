using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTower : MonoBehaviour
{
    public static EnemyTower instance;

    public float healthAmount = 0;
    public float maxHealth = 10000;
    public Slider healthBar;

    public bool towerDefeated = false;

    public GameObject enemyTower;

    public IsometricController player;

    [Header("Particals")]
    public ParticleSystem[] particleSystems;

    [Header("GameEnder for transitiops ")]
    public LoadingScreen loadingScreen;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(maxHealth);
        instance = this;
        healthAmount = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (towerDefeated == true)
        {
            Invoke("Transfer", 5f);
        }

        SetHealth(healthAmount);
        if (healthAmount <= 0)
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
                FindAnyObjectByType<AudioManager>().Play("EnemyHit");
                ActivateRandomParticleSystem();
                healthAmount -= player.attackDamage;

            }
            if (other.gameObject.CompareTag("SwordHitBox"))
            {
                FindAnyObjectByType<AudioManager>().Play("EnemyHit");
                ActivateRandomParticleSystem();
                healthAmount -= player.swordDamage;
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

    public void ActivateRandomParticleSystem()
    {
        if (particleSystems != null && particleSystems.Length > 0)
        {
            int randomIndex = Random.Range(0, particleSystems.Length);

            // Disable all particle systems in the array
            for (int i = 0; i < particleSystems.Length; i++)
            {
                if (particleSystems[i].isPlaying)
                {
                    particleSystems[i].Stop();
                }
            }

            // Activate the randomly selected particle system
            particleSystems[randomIndex].Play();
        }
        else
        {
            Debug.LogWarning("No particle systems found or added to the array!");
        }
    }

    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    private void Transfer()
    {
        loadingScreen.LoadScene(sceneName);
    }
}
