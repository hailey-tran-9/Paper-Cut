using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Vector3 playerPos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 direction = playerPos - transform.position;
        Vector3 rotation = transform.position - playerPos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    #endregion
    
    #region State_functions
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    #endregion

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
