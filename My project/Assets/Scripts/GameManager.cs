using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject oozeEnemyPrefab;
    public GameObject wolfEnemyPrefab;
    public GameObject skullEnemyPrefab;
    private Vector3 groundSpawnPosition;
    private Vector3 airSpawnPosition;
    public GameObject[] enemyCount;
    public int waveNumber = 1;
    private PlayerControll playerControllerScript;
    public int score;
    public TextMeshProUGUI instructionsText;
    public float timeWaited;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI restartText;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawnPosition = new Vector3(-9f, -3.4f, 0f);
        airSpawnPosition = new Vector3(-9f, 0f, 0f);
        SpawnEnemyWave(waveNumber);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        score = 0;
        UpdateScore(0);
    }

    async void SpawnEnemyWave(int enemiesToSpawn)
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        if(!playerControllerScript.gameOver)
        {
            for(int i = 0; i < enemiesToSpawn; i++)
                {
                    int whichEnemy = Random.Range(1, 10);
                    if(whichEnemy >= 1 && whichEnemy <= 5)
                    {
                        Instantiate(oozeEnemyPrefab, groundSpawnPosition, oozeEnemyPrefab.transform.rotation);
                    }
                    else if(whichEnemy >= 6 && whichEnemy <= 8)
                    {
                        Instantiate(wolfEnemyPrefab, groundSpawnPosition, wolfEnemyPrefab.transform.rotation);
                    }
                    else if(whichEnemy >= 9 && whichEnemy <= 10)
                    {
                        Instantiate(skullEnemyPrefab, airSpawnPosition, skullEnemyPrefab.transform.rotation);
                    }
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeWaited += Time.deltaTime;
        
        instructionsText.text = "Use A and D or the left and right arrow keys to move left to right, aim and shoot with the left mouse button.";

        if(timeWaited >= 5.5)
        {
            instructionsText.text = "Use W and S or the up and down arrow keys to climb up and down the ladder";
        }
        
        if(timeWaited >= 11)
        {
            instructionsText.text = "Don't let the monsters touch you or leave the map";
        }
        
        if(timeWaited >= 13)
        {
            instructionsText.text = "Good luck have fun";
        }        
        
        if(timeWaited >= 15)
        {
            instructionsText.text = "";
        }


        if(!playerControllerScript.gameOver)
        {
            UpdateScore(0);
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemyCount.Length == 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
            }
        }
        
        if(playerControllerScript.gameOver)
        {
            if(playerControllerScript.isWin)
            {
                winText.text = "You WIN!!";
                restartText.text = "Press space to restart";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    RestartGame();
                }
            }
        }
        
        if(playerControllerScript.gameOver)
        {
            if(playerControllerScript.isLose)
            {
                loseText.text = "you lose";
                restartText.text = "Press space to restart";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    RestartGame();
                }
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score * waveNumber;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
