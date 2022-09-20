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

    #region Movement_variables
    public int moveSpeed;
    float x_input;
    #endregion

    #region Combat_variables
    string dir;
    #endregion

    #region Unity_functions
    void Awake()
    {
        vcamFT = vcam.GetComponentInChildren<CinemachineFramingTransposer>();

        // Set the initial direction of gravity to push down
        dir = "down";
    }

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

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
            Debug.Log("Changed gravity direction to: " + dir);
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
