using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector3 playerPos;
    private Vector3 enemyPos;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyPos = transform.position;
    }
    private void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyPos = transform.position;

        if (enemyPos.x > playerPos.x)
        {
            sr.flipX = false;
        }
        else if (enemyPos.x < playerPos.x)
        {
            sr.flipX = true;
        }
    }
}