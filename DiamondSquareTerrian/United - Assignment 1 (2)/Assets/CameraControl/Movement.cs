using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // public variable to change speed
    public float movementSpeed = 10;
    public float rotationSpeed = 0.001f;

    // remeber previous mouse position
    private Vector3 mousePrevPos;
    private bool isPanning;

    // Use this for initialization
    void Start()
    {
        isPanning = false;
    }

    // Update is called once per frame
    void Update()
    {
        // mouse controls the rotation
        Vector3 yawAxis = Vector3.up;
        Vector3 pitchAxis = Vector3.right;
        Vector3 mousePos = Input.mousePosition;
        float d_yaw, d_pitch;
        // Only rotate if right mouse is held down
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // start panning
            if (!isPanning)
            {
                mousePrevPos = mousePos;
                isPanning = true;
            }

            // calculate difference in position
            d_yaw = mousePos.x - mousePrevPos.x;
            d_pitch = mousePrevPos.y - mousePos.y;

            // transform
            this.transform.Rotate(d_yaw * yawAxis * rotationSpeed);
            this.transform.Rotate(d_pitch * pitchAxis * rotationSpeed);

        }
        else
        {
            isPanning = false;
        }

        // keyboard controls the movement
        float dx = 0.0f, dz = 0.0f;
        if (Input.GetKey(KeyCode.D))
            dx += 1.0f;
        if (Input.GetKey(KeyCode.A))
            dx -= 1.0f;
        if (Input.GetKey(KeyCode.W))
            dz += 1.0f;
        if (Input.GetKey(KeyCode.S))
            dz -= 1.0f;

        this.transform.localPosition += new Vector3(dx, 0.0f, dz) * movementSpeed * Time.deltaTime;
    }
}
