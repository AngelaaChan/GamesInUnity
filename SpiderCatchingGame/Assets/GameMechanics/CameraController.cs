using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed = 1;
    private Vector3 finalPos = new Vector3 (0.6420407f, 3, -1);
    private Quaternion finalAngle = Quaternion.Euler (new Vector3 (78, 0, 0));
    private Vector3 inialPos;
    private Quaternion intialAngle;

    void Start () {
        // get the inital position
        inialPos = this.transform.localPosition;
        // get the intial angle
        intialAngle = this.transform.rotation;
    }

    void Update () {
        //Move the camera from current view to top view of the computer
        Vector3 movement = Vector3.Lerp (transform.localPosition, finalPos, Time.deltaTime * speed);
        transform.localPosition = movement;
        // rotate
        Quaternion rotation = Quaternion.Lerp (transform.rotation, finalAngle, Time.deltaTime * speed);
        transform.rotation = rotation;
    }

}