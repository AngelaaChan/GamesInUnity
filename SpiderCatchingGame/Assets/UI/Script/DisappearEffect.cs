using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour {

    // stage 2 material
    public Material disappearMaterial;

    public float cutoff = -3;
    public float rate = 0.01f;

    // Use this for initialization
    void Start () {
        // Swap material to disappear material
        GetComponent<MeshRenderer> ().material = Instantiate<Material> (disappearMaterial);;
    }

    // Update is called once per frame
    void Update () {
        // gradually increase cut off
        cutoff += rate;
        GetComponent<MeshRenderer> ().material.SetFloat ("_CutOff", cutoff);
    }

}