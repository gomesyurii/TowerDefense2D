using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    private EnemySpawner enemySpawnerInstance;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        enemySpawnerInstance = FindObjectOfType<EnemySpawner>();
    }

    public void TakeDamage(float damage)
    {
        int currentWave = enemySpawnerInstance.GetCurrentWave();
        currentHealth -= damage;
        if (currentHealth <= 0f && !isDead)
        {
            EnemySpawner.OnEnemyDestroy.Invoke();
            isDead = true;
            Destroy(this.gameObject);
            LevelManager.main.IncreaseCurrency(1 * currentWave);  
        }
    }
}
