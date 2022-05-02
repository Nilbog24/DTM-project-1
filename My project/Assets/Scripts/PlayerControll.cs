using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed = 5.0f;
    public float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput >= 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            }
        if(horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * -1);
            }
    }
}
