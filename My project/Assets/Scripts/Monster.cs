using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speedFactor;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speedFactor = Random.Range(8, 17);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed * speedFactor/-10);
    }
}
