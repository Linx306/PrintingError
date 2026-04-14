using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    [Header("Enemigos")]
    public GameObject[] enemyPrefabs; // A_1, B_1

    [Header("Spawn")]
    public Transform[] spawnPoints;
    public int maxEnemiesAlive = 2;

    [Header("Boss")]
    public int bossLife = 10;
    public int totalEnemiesToSpawn = 10;

    [Header("UI")]
    public Slider healthBar;

    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        // Inicializar barra de vida
        if (healthBar != null)
        {
            healthBar.maxValue = bossLife;
            healthBar.value = bossLife;
        }

        // Spawn inicial
        while (enemiesAlive < maxEnemiesAlive && enemiesSpawned < totalEnemiesToSpawn)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemiesSpawned >= totalEnemiesToSpawn) return;

        // Elegir spawn point aleatorio
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Elegir enemigo aleatorio (A_1 o B_1)
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(prefab, spawn.position, Quaternion.identity);

        // Conectar enemigo con el boss
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.boss = this;
        }

        enemiesSpawned++;
        enemiesAlive++;
    }

    // 🔴 Se llama cuando un enemigo muere
    public void OnEnemyKilled()
    {
        enemiesAlive--;
        bossLife--;

        // Actualizar barra de vida
        if (healthBar != null)
        {
            healthBar.value = bossLife;
        }

        Debug.Log("Boss vida: " + bossLife);

        if (bossLife <= 0)
        {
            Die();
            return;
        }

        // 🔥 Mantener siempre el número máximo de enemigos vivos
        while (enemiesAlive < maxEnemiesAlive && enemiesSpawned < totalEnemiesToSpawn)
        {
            SpawnEnemy();
        }
    }

    void Die()
    {
        Debug.Log("BOSS DERROTADO");

        // Aquí puedes agregar:
        // animación
        // sonido
        // abrir puerta
        // activar portal

        Destroy(gameObject);
    }
}