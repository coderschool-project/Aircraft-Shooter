using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnRate = 5.0f;
    public float padding = 0.8f;
    float minX;
    float maxX;
    public int SpawnCount = 10;

    public GameController gameController;

    private bool lastEnemySpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        FindBouderies();
        StartCoroutine(SpawnContinuously());
    }
    void FindBouderies()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastEnemySpawned && FindAnyObjectByType<BossBulletScript>()== null)
        {
            StartCoroutine(gameController.LevelComplete());
        }

    }
    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        int randomX = Random.Range((int)minX, (int)maxX);
        Vector2 spawnPosition = new(randomX, transform.position.y);
        Instantiate(enemies[randomIndex], spawnPosition, Quaternion.identity);
    }
    IEnumerator SpawnContinuously()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
        lastEnemySpawned = true;
    }
}
