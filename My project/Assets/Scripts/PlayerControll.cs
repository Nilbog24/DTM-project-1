using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed = 2.5f;
    public float climbSpeed = 3.0f;
    public float horizontalInput;
    public float veritcalInput;
    private Rigidbody playerRb;
    public bool onLadder = false;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        onLadder = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        veritcalInput = Input.GetAxis("Vertical");

        Vector3 mousePosition = Input.mousePosition;
        if(horizontalInput > 0 && !gameOver)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if(horizontalInput <= 0 && !gameOver)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * -1);
        }
        if(onLadder && !gameOver)
        {
            transform.Translate(Vector3.up * veritcalInput * Time.deltaTime * climbSpeed);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
        }
        if(other.CompareTag("Enemy"))
        {
            gameOver = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}
