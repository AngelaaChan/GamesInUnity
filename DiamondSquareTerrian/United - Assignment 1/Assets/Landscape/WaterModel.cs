using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterModel : MonoBehaviour {

    public float power = 3;
    public float scale = 1;
    public float timeScale = 1;

    private float xOffest;
    private float yOffset;
    private MeshFilter mf;

	// Use this for initialization
	void Start () {
        mf = GetComponent<MeshFilter>();
        MakeNoise();
	}
	
	// Update is called once per frame
	void Update () {
        MakeNoise();
        xOffest += Time.deltaTime * timeScale;
        yOffset += Time.deltaTime * timeScale;
	}

    void MakeNoise() {
        Vector3[] verts = mf.mesh.vertices;

        for (int i = 0; i < verts.Length; i++) {
            verts[i].y = CalculateHeight(verts[i].x, verts[i].z) * power;
        }

        mf.mesh.vertices = verts;
    }

    float CalculateHeight(float x, float y) {
        
        float xCord = x * scale + xOffest;
        float yCord = y * scale + yOffset;

        return (Mathf.PerlinNoise(xCord, yCord));
    }
}
