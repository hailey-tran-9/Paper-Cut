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

    #region Gravity_functions
    public IEnumerator ChangeGravity()
    {
        string currDir = PlayerController.dir;
        if (currDir == "down") {
            rb.velocity = Vector2.down * force;
        } else if (currDir == "left") {
            rb.velocity = Vector2.left * force;
        } else if (currDir == "up") {
            rb.velocity = Vector2.up * force;
        } else if (currDir == "right") {
            rb.velocity = Vector2.right * force;
        }

        yield return new WaitForSeconds(duration);

        rb.velocity = Vector2.zero;
    }
    #endregion

    #region Unity_functions
    void Awake()
    {
        // Set rb to the object's Rigidbody2D component
        rb = gameObject.GetComponent<Rigidbody2D>();
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
    }

    // void OnCollisionExit2D(Collision2D other)
    // {
    //     GameObject otherGO = other.gameObject;

    //     // If the bullet comes into contact with the object, change moved to false
    //     if (otherGO.tag == "Bullet") {
    //         moved = false;
    //     }
    // }

    #endregion
}
