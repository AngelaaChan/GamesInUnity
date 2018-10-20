using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

    public int speed = 100;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //rotate around the zero point
        //rotate around the right axis
        //rotate speed
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        //teo of the components are facing each other
        transform.LookAt(Vector3.zero);
    }
}
