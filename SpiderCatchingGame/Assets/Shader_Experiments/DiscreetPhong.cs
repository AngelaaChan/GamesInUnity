using System.Collections;
using UnityEngine;

public class CellShader : MonoBehaviour {
    public PointLight pointLight;

    // Called each frame
    void Update () {
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer> ();

        // Pass updated light positions to shader
        renderer.material.SetColor ("_PointLightColor", this.pointLight.color);
        renderer.material.SetVector ("_PointLightPosition", this.pointLight.GetWorldPosition ());
    }
}