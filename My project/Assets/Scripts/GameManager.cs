using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    // This is a list of all the variables in the script being initialized.

    // These GameObject variables hold the prefabs for the enemies.
    // This makes it so that the script has something to summon.
    public GameObject oozeEnemyPrefab;
    public GameObject wolfEnemyPrefab;
    public GameObject skullEnemyPrefab;
    // These two Vector variables hold the spawn positions.
    // They will be called upon when summoning the monsters.
    // Which one is called will depend if the monster summoned is a flying monster or not.
    private Vector3 groundSpawnPosition;
    private Vector3 airSpawnPosition;
    // This list will hold every every enemy that currently exists in the scene.
    // It'll be used to determine when to spawn the next wave.
    public GameObject[] enemyCount;
    // This integer variable is used to count which wave the player is on.
    // It is used to determine how many monsters to spawn as well as the players score.
    // It is also used to determine when the player satisfies the win condition.
    public int waveNumber = 1;
    // This variable will be used to store the playerController script.
    // This makes it so that this script can pull from variables from that script.
    // It also allows for this script to call upon methods from that script, though this function is not used.
    private PlayerControll playerControllerScript;

    // This variable holds the object that holds the instructions text.
    // This is used to let this script edit the text.
    // It is used to change the text so that all the controls can explained without taking up an overly large amount of space.
    // It also helps make sure the intructions aren't covering up anything important.
    public TextMeshProUGUI instructionsText;
    // This float is used to count time.
    // It is used to determine when to switch the instructions text to the next part.
    public float timeWaited;
    // This integer variable is used to keep track of the player current score.
    // This is not used to determine if the player meets the win condition, though it could've been used for that.
    // The score only exist for the player to see. It serves no other purpose.
    public int score;
    // This is used to hold the score text object so that it can be changed.
    // What this is changed to is based on the score variable above.
    public TextMeshProUGUI scoreText;
    // This is used to hold the win screen text that appears when the player wins the game.
    // It is less a win screen and more just some text that tells the player that they won.
    public TextMeshProUGUI winText;
    // This is the variable that holds the lose text that appears when the player loses the game.
    public TextMeshProUGUI loseText;
    // This is used to hold the restart text object.
    // This is used to make it so when the player wins or loses the game they know how to restart the game.
    public TextMeshProUGUI restartText;
    // This is used to hold the wave number text object.
    // It is used to inform the player which wave they are currently on.
    public TextMeshProUGUI waveNumberText;

    // Start is called before the first frame update
    // This method is used to declare what positions the spawn positions are.
    // Then it'll call upon the SpawnEnemyWave method to spawn the first wave of enemies.
    // Then it'll get the PlayerControll script from the player game object.
    // The it'll set the score to zero and then call upon the UpdateScore method to set the score text to say the score is zero.
    // Then it'l set the intructions text to the first line of instructions that detail horizontal movement and shooting.
    void Start()
    {
        groundSpawnPosition = new Vector3(-9f, -3.4f, 0f);
        airSpawnPosition = new Vector3(-9f, 0f, 0f);
        SpawnEnemyWave(waveNumber);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        score = 0;
        UpdateScore(0);
        instructionsText.text = "Use A and D or the left and right arrow keys to move left to right, aim and shoot with the left mouse button.";

    }

    // This method is used to spawn the waves of enemies.
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // This is used to find the PlayerControll script from the player object.
        // This is the exact same thing that is done in the Start method.
        // It shoulnd't be necessary but removing it stops the game from working.
        // I suspect that it is because the playerControllerScript is private when it is initialized.
        // However due to time constraints I am unwilling to mess around with the code.
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        // This is an if loop. 
        // If loops are loops where the code inside of them will continue to run as long as the if loops condition is met.
        // In this case it's if the gameOver boolean from the playerControll script is false.
        if(!playerControllerScript.gameOver)
        {
            // This lines of code means that for each value of one that makes up that integer enemiesToSpawn the code inside will repeat.
            // Or to simplify the definition, it means that the code inside of this for loop will repeat a number of times.
            // That number is equal to the value of enemiesToSpawn.
            for(int i = 0; i < enemiesToSpawn; i++)
                {
                    // What this code inside of the for loop does is generate a random enemy for each repitiion of the loop.
                    // It'll first generate a random interger between one and ten.
                    // And then depending on which integer was generated a enemy will spawn.
                    // Where the monster will spawn will determine if is an air or ground based monster.
                    // There are two ground based enemies, which are the ooze and the wolf.
                    // There is one air based enemym which is the skull.
                    // There is a 40% chance of the ooze spawning, and the wolf and skull both have a 30% chance.
                    int whichEnemy = Random.Range(1, 10);
                    if(whichEnemy >= 1 && whichEnemy <= 4)
                    {
                        Instantiate(oozeEnemyPrefab, groundSpawnPosition, oozeEnemyPrefab.transform.rotation);
                    }
                    else if(whichEnemy >= 5 && whichEnemy <= 7)
                    {
                        Instantiate(wolfEnemyPrefab, groundSpawnPosition, wolfEnemyPrefab.transform.rotation);
                    }
                    else if(whichEnemy >= 8 && whichEnemy <= 10)
                    {
                        Instantiate(skullEnemyPrefab, airSpawnPosition, skullEnemyPrefab.transform.rotation);
                    }
                }
        }
    }

    // Update is called once per frame
    // All the code in the Update method is code that I want to constantly update.
    // This helps with things such as checking if something meets a condition, or updating things.
    void Update()
    {
        // This make timeWaited equal to itself plus the time between this update and the last.
        timeWaited += Time.deltaTime;
        // This makes the waveNumberText display the current wave number.
        // The reason this is done here and not when the wave changes is because then there won't be anything displayed for wave one.
        // This could be fixed by making it so that the text initially displays "Wave: 0" but this works just fine and is easier to do.
        waveNumberText.text = "Wave: " + waveNumber;

        // The following time values were based off of the average reading speed of 250 words per minute.
        // After finding the average time taken to read the text I decreased the time slightly.

        // Once five and a half seconds have passed the intsructions text will change.
        // This text describes how to climb up and down the ladder.
        if(timeWaited >= 5.5)
        {
            instructionsText.text = "You've got special arrows that automatically delete whatever they hit. Also, use W and S or the up and down arrow keys to climb up and down the ladder";
        }
        
        // After elven seconds the text will change again.
        // This describes the lose conditions.
        if(timeWaited >= 11)
        {
            instructionsText.text = "Don't let the monsters touch you or leave the map";
        }

        // After thirteen seconds the text will change to reveal the win condition.    
        if(timeWaited >= 13)
        {
            instructionsText.text = "Reach wave 30 to win!";
        }        
        
        // After fifteen seconds the text will change to become empty and nothing will be displayed.
        if(timeWaited >= 15)
        {
            instructionsText.text = "";
        }

        // This detects if the gameOver boolean from the playerControllerScript is false.
        // If it is false then the code inside will run        
        if(!playerControllerScript.gameOver)
        {
            // First the UpdateScore method will be called upon.
            // This is here because part of the score calculations involve the wave number.
            // This is not needed to be in this if condition, however it made it easier for me to understand the code while creating it.
            // Now that the code it finished it could be moved to the top of this method. 
            // However moving it may cause unforseen errors and again due to time constraints I may not have time to fix it.
            // So instead of potentially wasting time by trying to fix an error before just undoing all changes to get back to this state,
            // I shall just leave it as is.
            UpdateScore(0);
            // This will put all GameObjects with the tag "Enemy" into the enemyCount list.
            // Then if the length, which is the amount of items in it, is equal to zero then the waveNumber will increase by one.
            // Then the SpawnEnemyWave method will be called with the value of the waveNumber neing the value for enemiesToSpawn.
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemyCount.Length == 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
            }
        }
        
        // This detects if the gameOver variable in the player controller script it true.
        // If it is true then the following code will run.
        // This if function may no longer be necessary, the two inside alone may work.
        // However, again, time constraints and rick of error makes me unwilling to change it.
        if(playerControllerScript.gameOver)
        {
            // If the isWin variable from the player controller script is true then the win code will run.
            if(playerControllerScript.isWin)
            {
                // The win text will change to be "You WIN!!"
                // The the restart text will appear.
                // Then if the player presses the spacebar when the game is in this state then the RestartGame method will be called.
                winText.text = "You WIN!!";
                restartText.text = "Press space to restart";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    RestartGame();
                }
            }

            // If the isLose boolean from the player controller script is true then the lose code will run
            // The only difference between the win and lose code is the text.
            // The two could've shared one text object.
            // However I wanted different colour settings for the two texts.
            // The reason there's is a isLose variable instead of just looking for if isWin is false is that it wouldn't work.
            // In an earlier version of the code there was if not isWin here instead of what is currently here.
            // However when the game started the lose text would show up.
            // I don't understand why this happened as this was in the if gameOver loop, so it shouldn't have shown up.
            if(playerControllerScript.isLose)
            {
                // The loseText will change to display the words "you lose"
                // Then the restart text will show up.
                // And now if the player presses the spacebar the RestartGame method will be called.
                loseText.text = "you lose";
                restartText.text = "Press space to restart";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    RestartGame();
                }
            }
        }
    }

    // This method is used to update the current score displayed.
    // When it's called first it'll add to the score value equal the scoreToAdd which will be determined when the method is called.
    // Then the scoreText will change to show the score multiplied by the waveNumber to show the total score.
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score * waveNumber;
    }

    // This method is used to restart the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
