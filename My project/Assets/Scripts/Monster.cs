using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // This float will be used to determine the number that the monsters speed value will be multiplied by.
    public float speedFactor;
    // This value is the speed at which the monster will travel at.
    public float speed;
    // This variable is used to store the playerControllerScript so variables and method from it can be used in this script.
    private PlayerControll playerControllerScript;
    
    // Start is called before the first frame update
    // Here the speedFactor will generated a random value between eight and seventeen.
    // The numbers inside the range are floats so there's more variety in the reuslts.
    // This value is generated so that the monsters will move at different speeds.
    // This is because if all of the monster move at the same speed they will all clump up.
    // When they clump up multiple enemies can be taken out with one projectile, which is bad.
    // With this you can still get multiple but it's harder.
    // After the random number has been generated then the method will find the playerControll script from the player.
    // Then it'll be stored in the playerControllerScript variable.
    void Start()
    {
        speedFactor = Random.Range(8.0f, 17.0f);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    // Update is called once per frame.
    void Update()
    {
        // With this the monster will more right towards the player every frame.
        // The monster will move a distance equal to their speed multiplied by the change in time multiplied by speed facto the divided by ten.
        // The distance is divided by ten as otherwise the monster will move to fast.
        transform.Translate(Vector3.right * Time.deltaTime * speed * speedFactor/-10);
        
        // This detects if the monster has left the bounds of the map.
        // If they have that means that the player has lost the game and the gameOver boolean is set to true.
        if(transform.position.x > 8.9)
        {
            playerControllerScript.gameOver = true;
        }

        // This detects if the gameOver boolean is true.
        // If it is then the monster will destroy it self.
        // This makes it so once the game ends there will be no more monsters on the screen.
        // This makes no difference in the functionality of the game, it is purey an aesthetic choice.
        if(playerControllerScript.gameOver == true)
        {
            Destroy(gameObject);
        }
    }
}
