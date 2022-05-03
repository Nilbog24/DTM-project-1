using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed = 5.0f;
    public float horizontalInput;
    public float veritcalInput;
    private Rigidbody playerRb;



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
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            playerRb.AddForce(transform.up * veritcalInput * speed);
        }
    }
}
