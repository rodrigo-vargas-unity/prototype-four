using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    private int waveNumber = 0;
    private int enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        GenerateNewWave();
    }

    private void GenerateNewWave()
    {
        waveNumber++;

        Debug.Log($"Spawning wave #{waveNumber}");

        for (var x = 0; x < waveNumber; x++)
        {
            Instantiate(
                enemyPrefab,
                GenerateSpawnPosition(),
                enemyPrefab.transform.rotation
            );
        }

        Instantiate(
            powerupPrefab,
            GenerateSpawnPosition(),
            powerupPrefab.transform.rotation
        );
    }

    private Vector3 GenerateSpawnPosition()
    {
        var spawnPositionX = Random.Range(-spawnRange, spawnRange);
        var spawnPositionY = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPositionX, 0, spawnPositionY);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            GenerateNewWave();
        }
    }
}
