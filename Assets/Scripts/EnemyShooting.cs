using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private Camera mainCam;
    public Transform Player;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public int detectionRadius;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Player.position;

        Vector3 rotation = playerPos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring && Vector3.Distance(playerPos, transform.position) <= detectionRadius)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }

    public bool canFire()
    {
        return canFire;
    }
}
