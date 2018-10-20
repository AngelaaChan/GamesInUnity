using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use mouse to control panning, and keyboard to control rigid body movement
/// </summary>
public class Movement : MonoBehaviour
{

    // public variable to change speed
    public float movementSpeed = 5;
    public float rotationSpeed = 0.5f;
    public float PADDING = 10;
    public float maximumSpeed = 50;


    // remeber previous mouse position
    private Vector3 mousePrevPos;
    private bool isPanning;

    // save rigid body reference
    private new Rigidbody rigidbody;

    // check if it is colliding
    private bool colliding = false;

    // Use this for initialization
    void Start()
    {

        // grab the rigid body reference
        rigidbody = GetComponent<Rigidbody>();
        // no rotation on contact
        rigidbody.freezeRotation = true;
        // do not pan at the start
        isPanning = false;
        // continous collsion detection
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
            this.transform.Rotate(d_yaw * yawAxis * rotationSpeed * Time.deltaTime);
            this.transform.Rotate(d_pitch * pitchAxis * rotationSpeed * Time.deltaTime);
            // reorientate yawn
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            isPanning = false;
        }

        // do not move while on collision
        if (colliding)
        {
            return;
        }


        // Change transformation based on key pressed
        Vector3 dx = Vector3.zero, dz = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
            dx += transform.right.normalized;
        if (Input.GetKey(KeyCode.A))
            dx -= transform.right.normalized;
        if (Input.GetKey(KeyCode.W))
            dz += transform.forward.normalized;
        if (Input.GetKey(KeyCode.S))
            dz -= transform.forward.normalized;


        // set velocity
        this.rigidbody.velocity += (dx + dz) * movementSpeed;

        // restrict maximum velocity to prevent tunneling
        float x = this.rigidbody.velocity.x;
        float y = this.rigidbody.velocity.y;
        float z = this.rigidbody.velocity.z;

        if (x > maximumSpeed)
        {
            x = maximumSpeed;
        }

        if (y > maximumSpeed)
        {
            y = maximumSpeed;
        }

        if (z > maximumSpeed)
        {
            z = maximumSpeed;
        }

        if (x < -maximumSpeed)
        {
            x = -maximumSpeed;
        }

        if (y < -maximumSpeed)
        {
            y = -maximumSpeed;
        }

        if (z < -maximumSpeed)
        {
            z = -maximumSpeed;
        }

        this.rigidbody.velocity = new Vector3(x, y, z);
    }

    void LateUpdate()
    {
        // avoid moving outside the terrain
        // x and z cannot be negetive 
        int WIDTH = GameObject.Find("Terrain").GetComponent<DiamondSquareTerrain>().numDivisions;
        int MINIMUM = 1;

        if (transform.position.x < MINIMUM)
        {
            this.transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }
        if (transform.position.x > WIDTH)
        {
            this.transform.position = new Vector3(WIDTH, transform.position.y, transform.position.z);
        }
        if (transform.position.z < MINIMUM)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, MINIMUM);
        }
        if (transform.position.z > WIDTH)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, WIDTH);
        }
        int z = (int)this.transform.position.z;
        int x = (int)this.transform.position.x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
    }

    // On collision stay move up a little bit
    // since on collision enter already done in physics material
    private void OnCollisionStay(Collision collision)
    {

        this.transform.position += Vector3.up * PADDING;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
