using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region Movement_variables
    Rigidbody2D rb;
    #endregion

    #region Gravity_variables
    public float force;
    public float duration;
    #endregion

    #region Combat_variables
    public int dmg;
    #endregion

    #region Gravity_functions
    IEnumerator ChangeGravity()
    {
        string currDir = PlayerController.dir;
        if (currDir == "down") {
            rb.gravityScale = force;
        } else if (currDir == "left") {
            rb.velocity = Vector2.left * force;
            yield return new WaitForSeconds(duration);
            rb.gravityScale = 0;
        } else if (currDir == "up") {
            rb.gravityScale = -force;
        } else if (currDir == "right") {
            rb.velocity = Vector2.right * force;
            yield return new WaitForSeconds(duration);
            rb.gravityScale = 0;
        }
        rb.velocity = Vector2.zero;
    }
    #endregion

    #region Unity_functions
    void Awake()
    {
        // Set rb to the object's Rigidbody2D component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    IEnumerator OnCollisionHelper(GameObject go)
    {
        // Deal damage
        go.GetComponent<EnemyController>().LoseHp(dmg);

        // Knockback
        Rigidbody2D enemyRb = go.GetComponent<Rigidbody2D>();
        Vector2 diff = transform.position - enemyRb.transform.position;
        diff = diff.normalized * 5;
        rb.AddForce(diff, ForceMode2D.Impulse);
        rb.gravityScale = -2;
        yield return new WaitForSeconds(0.35f);

        // Destroy this object
        Destroy(gameObject);
    }

    // Handle collisions
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGO = other.gameObject;

        // If the bullet comes into contact with the object, change the object's gravity
        if (otherGO.tag == "Bullet") {
            StartCoroutine(ChangeGravity());

            // Ask Sean how he's making the bullets disappear when it's off-screen,
            // and if we want to keep destroying the bullet uniform
            // (Here I could just do Destroy(otherGO or other.gameObject))
            otherGO.GetComponent<BulletScript>().OnBecameInvisible();
        }

        // If the object comes into contact with an enemy, do damage and destroy itself
        if (otherGO.tag == "Enemy") {
            StartCoroutine(OnCollisionHelper(otherGO));
        }

        // If the object comes into contact with an obstacle, set gravityScale to 0
        if (otherGO.tag == "Ground" || otherGO.tag == "Boundary") {
            rb.gravityScale = 0;
        }
    }
    #endregion
}
