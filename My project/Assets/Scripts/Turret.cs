using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject projectilePrefab;
    public Transform projectileTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    private PlayerControll playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControll>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && canFire)
        {
            canFire = false;
            Instantiate(projectilePrefab, projectileTransform.position, Quaternion.identity);
        }

        if(playerControllerScript.gameOver == true)
        {
            canFire = false;
            timeBetweenFiring = 2147483647;
        }
    }
}
