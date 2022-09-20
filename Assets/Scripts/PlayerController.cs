using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class PlayerController : MonoBehaviour
{
    #region Camera_variables
    public CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer vcamFT;
    #endregion

    #region Player_variables
    private Rigidbody2D rb;
    #endregion

    #region Movement_variables
    public int moveSpeed;
    public float jumpForce;
    public float jumpTime;
    float x_input;
    bool isGrounded;
    bool isJumping;
    float jumpTimeCounter;
    #endregion

    #region Combat_variables
    public int hp;
    string dir;
    #endregion

    #region Combat_functions
    // Decrease the player's health
    void LoseHp(int dmg)
    {
        hp -= dmg;

        // Check whether or not the player dies
        if (hp <= 0) {
            Die();
        }
    }

    // Destroy the player and transition to the Lose Scene
    void Die()
    {
        // Destroy the player
        Destroy(this.gameObject);

        // Transition to Lose Scene
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
    }
    #endregion

    #region Unity_functions
    void Awake()
    {
        // Set the Cinemachine Framing Transposer
        vcamFT = vcam.GetComponentInChildren<CinemachineFramingTransposer>();

        // Grab the player's Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Set the initial direction of gravity to push down
        dir = "down";

        // Set the initial jump variables
        isJumping = false;
    }

    // Determine if the player is touching the ground
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    // Change isGround to false when the player's collider leaves the ground
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        x_input = Input.GetAxisRaw("Horizontal");
        // int y_input = Input.GetAxisRaw("Vertical"); <-- We don't need to worry about jumping(?)

        // Move left or right depending on the key the player is pressing
        if (x_input == -1) {
            // Move left
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime),
                transform.position.y, 10);
        } else if (x_input == 1) {
            // Move right
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime),
                transform.position.y, 10);
        }

        if (isGrounded == true && Input.GetKeyDown("space")) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping == true && Input.GetKey("space")) {
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp("space")) {
            isJumping = false;
        }

        // Change the direction of gravity when the player hits left shift
        if (Input.GetKeyDown("left shift")) {
            if (dir == "down") {
                dir = "left";
            } else if (dir == "left") {
                dir = "up";
            } else if (dir == "up") {
                dir = "right";
            } else if (dir == "right") {
                dir = "down";
            }
            // Debug.Log("Changed gravity direction to: " + dir);
        }
    }

    void FixedUpdate()
    {
        // Reposition the camera
        if (x_input == -1) {
            // Position the camera to face left
            vcamFT.m_TrackedObjectOffset.x = -5f;
        } else if (x_input == 1) {
            // Position the camera to face right
            vcamFT.m_TrackedObjectOffset.x = 5f;
        }
    }
    #endregion
}