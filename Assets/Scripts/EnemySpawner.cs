using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWavesInSeconds = 5f;
    [SerializeField] private float difficultScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private bool isSpawning = false;
    public int enemiesAlive;
    private int enemiesLeftToSpawn;


    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void Awake()
    {
        OnEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultScalingFactor));
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= 1f / enemiesPerSecond && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(); 
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
       // Debug.Log("Spawning enemy");
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }


}
