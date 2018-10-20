using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float upSpeed = 100;

    // save rigid body reference
    private new Rigidbody rigidbody;

    private void Start()
    {
        // get rigid body referece
        rigidbody = GetComponent<Rigidbody>();
    }


    // while in contact drift up
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
    }

    // stop drifting 
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("left");
    }

}
