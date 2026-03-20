using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManagerScript : MonoBehaviour
{
    public GameObject Player;
    public LevelManager LevelManager;
    public float SpawnRadius = 5f;
    public float SpawnRate = 5f;
    public bool IsSpawning = true;
    public int EnemyNum = 1;
    
    public GameObject[] EnemyPrefabs;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        LevelManager = Player.GetComponent<LevelManager>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (IsSpawning)
        {
            EnemyNum = (int)Math.Round(0.9 * (Math.Log(LevelManager.currentLevel + 1) + 0.9 * LevelManager.currentLevel));
            for (int i = 0; i < EnemyNum; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle * SpawnRadius;
                Vector3 spawnPos = Player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);

                GameObject Enemy = Instantiate(EnemyPrefabs[Random.Range(1, EnemyPrefabs.Length - 1)], spawnPos,
                    Quaternion.identity);
            }
            yield return new WaitForSeconds(SpawnRate);
        }
    }
    
}
