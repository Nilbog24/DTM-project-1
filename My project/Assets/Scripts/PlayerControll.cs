using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    // These two variables determine the speed at which the player moves around the world.
    // The first is for horizontal movement.
    // The second is used when the player is climing the ladder.
    public float speed = 2.5f;
    public float climbSpeed = 3.0f;
    // These two variables get the horizontal and vertical inputs respectfully.
    // These are used to make it so that the player only moves when thye are pressing a movement key.
    public float horizontalInput;
    public float veritcalInput;
    // This gets the player's rigidbody so that the script can edit it's values.
    private Rigidbody playerRb;
    // This is used to determine whether or not the player is on the ladder.
    // This is used to make sure the player can only move up and down on the ladder.
    public bool onLadder = false;
    // This is used to determine when the game is over.
    public bool gameOver;
    // This gets the gamemanager object so that variables and methods from it can be used in this script.
    private GameManager gameManager;
    // These two booleans are used to determine whether the player wins or loses the game when the game ends.
    public bool isWin;
    public bool isLose;

    // Start is called before the first frame update
    // First this method gets the Rigidbody component from the player object and puts in in the playerRb variable.
    // Then it sets onLadder to false. Even though it is set as false when it is initialized it still needs to be set as false here.
    // The reason for this is that if the player dies on the ladder the can start the game with this set to true.
    // This means that until the player touches the ladder they can essentially fly.
    // Then the gameManager variable is assigned the GameManager script from the Game Manager object.
    // Then isWin is set to false because the game could sometimes start with it on and cause issues.
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        onLadder = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isWin = false;

    }

    // Update is called once per frame
    void Update()
    {
        // This is used to get the mouse's position.
        // This isn't used in this script, it's used in the turret code to determine where to shoot.
        Vector3 mousePosition = Input.mousePosition;

        // This is what is used to give theses variables values.
        // These are used below in the movement code.
        horizontalInput = Input.GetAxis("Horizontal");
        veritcalInput = Input.GetAxis("Vertical");

        // Here if horizonatal input if greater than zero and the game isn't over it will move right.
        // The distance will be eaual to speed multiplied by the horizontal inout multiplied by the change in time.
        // Before that though the player character will be rotated to face right.
        if(horizontalInput > 0 && !gameOver)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        // If the horizontal input is less or equal to one  and the game isn't over the the player will rotate to face left and move left.
        // The player's movement left shares the same equation as it's movement right.
        // The only differnce is that in the moving left equation there's a multiplied by negative one.
        // This is because due to the rotation without the times negative one the player would move right instead.
        // So the negative one is needed to make the player move left.
        if(horizontalInput <= 0 && !gameOver)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * -1);
        }
        // If the player is on the ladder and the game isn't over then they can climb the ladder.
        // The player will move a distance equal to the vertical input multiplied by the change of time multiplied by the climb speed.
        if(onLadder && !gameOver)
        {
            transform.Translate(Vector3.up * veritcalInput * Time.deltaTime * climbSpeed);
        }

        // If the waveNumber variable in the gameManager script is equal to 31 the gameOver and the isWin booleans will become true.
        // Then the game has been won.
        if(gameManager.waveNumber == 31)
        {
            gameOver = true;
            isWin = true;
        }
    }
    
    // This method is used to detect when the player collides with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Here if the player collides with the ladder then the onLadder boolean will become true.
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
        }
        // If the player collides with an enemy then the gameOver and isLose booleans becomes true and they have lost the game.
        if(other.CompareTag("Enemy"))
        {
            gameOver = true;
            isLose = true;            
        }
    }

    // This detects when the player stops colliding with something.
    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object that the player has stopped colliding with is the ladder then onLadder will become false.
        // This is used to make it so that the player can't move up and down after they've gotten off the ladder.
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}
