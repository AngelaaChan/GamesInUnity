using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class Pixelate : MonoBehaviour {
    public Material material;

    // pixelate
    void OnRenderImage (RenderTexture src, RenderTexture dest) {
        Graphics.Blit (src, dest, material);
    }
}