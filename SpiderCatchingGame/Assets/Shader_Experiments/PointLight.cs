using System.Collections;
using UnityEngine;

public class PointLight : MonoBehaviour {

    public Color color;

    public Vector3 GetWorldPosition () {
        return this.transform.position;
    }
}

