using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject oozeEnemyPrefab;
    public GameObject wolfEnemyPrefab;
    public GameObject skullEnemyPrefab;
    private Vector3 groundSpawnPosition;
    private Vector3 airSpawnPosition;
    public GameObject[] enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawnPosition = new Vector3(-9f, -3.4f, 0f);
        airSpawnPosition = new Vector3(-9f, 0f, 0f);
        SpawnEnemyWave(waveNumber);
    }

    async void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
            {
                int whichEnemy = Random.Range(1, 10);
                Debug.Log(whichEnemy);
                if(whichEnemy >= 1 && whichEnemy <= 5)
                {
                    Instantiate(oozeEnemyPrefab, groundSpawnPosition, oozeEnemyPrefab.transform.rotation);
                }
                else if(whichEnemy >= 6 && whichEnemy <= 8)
                {
                    Instantiate(wolfEnemyPrefab, groundSpawnPosition, wolfEnemyPrefab.transform.rotation);
                }
                else if(whichEnemy == 9 && whichEnemy == 10)
                {
                    Instantiate(skullEnemyPrefab, groundSpawnPosition, skullEnemyPrefab.transform.rotation);
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyCount.Length == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }
}
