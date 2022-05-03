using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed = 5.0f;
    public float horizontalInput;
    public float veritcalInput;
    private Rigidbody playerRb;
    public bool onLadder = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        veritcalInput = Input.GetAxis("Vertical");
        if(horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if(horizontalInput <= 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * -1);
        }
        if(onLadder)
        {
            transform.Translate(Vector3.up * veritcalInput * Time.deltaTime * speed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pee Pee Poo Poo");
        if (other.CompareTag("Ladder"))
        {
            onLadder = true;
            Debug.Log("Poo poo pee pee");
        }
    }
}
