using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlane : MonoBehaviour
{

    public int numDivisions = 256; //number of faces (squares)

    Vector3[] verts; //holds all vertices for the terrain
    int[] tris; //hold vertices of triangles of the terrain
    int numVertices; //number of vertices
    float partsOfThree, minHeight;

    // Use this for initialization
    void Start()
    {
        CreateTerrain();

    }

    private void Update()
    {
        partsOfThree = GameObject.Find("Terrain").GetComponent<DiamondSquareTerrain>().partsOfThree;
        minHeight = GameObject.Find("Terrain").GetComponent<DiamondSquareTerrain>().minHeight;
        this.transform.position = new Vector3(transform.position.x, minHeight + (partsOfThree * 0.5f), transform.position.z);
    }

    void CreateTerrain()
    {
        //As for 4 divisions (4 squares), 
        //there are 5 vertices, hence the total will be (div + 1)^2 
        numVertices = (numDivisions + 1) * (numDivisions + 1);
        verts = new Vector3[numVertices]; //Initializing verts Vector3 array

        //times 2 because two triangles make up a square, 
        //and times 3 because 3 vetices make up a triangle
        tris = new int[numDivisions * numDivisions * 2 * 3];

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int trianglesCounter = 0; //keeps count of the number of vertices while building triangles (helps for the loop)

        //Creating a 2D grid with numDivision rows and cols
        for (int i = 0; i <= numDivisions; i++)
        {

            for (int j = 0; j <= numDivisions; j++)
            {

                //Inititlizing the vertices in verts as (0,0,0), (1,0,0)....(0,0,1), (1,0,1)....
                verts[i * (numDivisions + 1) + j] = new Vector3(j, 0, i);
                //if statement to avoid the last vertices in every row
                if (i < (numDivisions - 1) && j < (numDivisions - 1))
                {

                    //Defining the topLeft, topRight, botLeft and botRight for each suare in the grid
                    int botLeftCorner = i * (numDivisions + 1) + j;
                    int botRightCorner = i * (numDivisions + 1) + (j + 1);
                    int topLeftCorner = (i + 1) * (numDivisions + 1) + j;
                    int topRightCorner = (i + 1) * (numDivisions + 1) + (j + 1);

                    //Building one square and then updating the triangles counter
                    tris[trianglesCounter] = botLeftCorner;
                    tris[trianglesCounter + 1] = topLeftCorner;
                    tris[trianglesCounter + 2] = botRightCorner;

                    tris[trianglesCounter + 3] = botRightCorner;
                    tris[trianglesCounter + 4] = topLeftCorner;
                    tris[trianglesCounter + 5] = topRightCorner;

                    trianglesCounter += 6;
                }
            }
        }
        mesh.vertices = verts;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
