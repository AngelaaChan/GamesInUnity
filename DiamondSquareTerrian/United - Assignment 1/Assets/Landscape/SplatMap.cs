using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatMap : MonoBehaviour {
    int numDivisions;
    public float[,] heightMap;
    Vector3[] verts;
    Mesh mesh;
    Color[] colors;


    // Use this for initialization
    void Update()
    {
        //       heightMap = GameObject.Find("Terrain").GetComponent<DiamondSquareTerrain>().heightMap;
        //       numDivisions = GameObject.Find("Terrain").GetComponent<DiamondSquareTerrain>().numDivisions;
        //       mesh = GameObject.Find("Terrain").GetComponent<Mesh>();

        //       float maxHeight = 0;
        //       float minHeight = 0;

        //       for (int i = 0; i <= numDivisions; i++)
        //       {
        //           for (int j = 0; j <= numDivisions; j++)
        //           {
        //               if (heightMap[i,j] > maxHeight) {
        //                   maxHeight = heightMap[i, j];
        //               }
        //               if (heightMap[i,j] < minHeight) {
        //                   minHeight = heightMap[i, j];
        //               }
        //           }
        //       }

        //       float partsOfThree = (maxHeight + minHeight) / 3;
        //       float snow = maxHeight - partsOfThree;
        //       float rock = maxHeight - (2 * partsOfThree);
        //       float grass = minHeight;

        //       Debug.Log(maxHeight);
        //       Debug.Log(snow + " " + rock);

        //       for (int i = 0; i <= numDivisions; i++)
        //       {
        //           for (int j = 0; j <= numDivisions; j++)
        //           {
        //               if(heightMap[i,j] > snow) {
        //                   colors[i * numDivisions + j] = new Color(255,255,255);
        //               }

        //               if(heightMap[i,j] > rock) {
        //                   colors[i * numDivisions + j] = new Color(139, 69, 19);
        //               }

        //               else {
        //                   colors[i * numDivisions + j] = new Color(153, 255, 51);
        //               }
        //           }
        //       }
        //       mesh.colors = colors;
        //}
    }
    }
