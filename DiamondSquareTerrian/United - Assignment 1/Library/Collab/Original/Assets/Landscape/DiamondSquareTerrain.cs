using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquareTerrain : MonoBehaviour {
    
    public int numDivisions = 512; //number of faces (squares)
    public float terrainHeight = 50; //max height of terrain

    Vector3[] verts; //holds all vertices for the terrain
    int[] tris; //hold vertices of triangles of the terrain
    float[,] heightMap;
    int numVertices; //number of vertices

	// Use this for initialization
	void Start () {
		CreateTerrain();
	}

    void CreateTerrain()
    {
        //Random.seed = (int)(terrainHeight - 10);
        //As for 4 divisions (4 squares), 
        //there are 5 vertices, hence the total will be (div + 1)^2 
        numVertices = (numDivisions + 1) * (numDivisions + 1);
        verts = new Vector3[numVertices]; //Initializing verts Vector3 array
        heightMap = new float[numDivisions + 1, numDivisions + 1];

        //times 2 because two triangles make up a square, 
        //and times 3 because 3 vetices make up a triangle
        tris = new int[numDivisions * numDivisions * 2 * 3];

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int trianglesCounter = 0; //keeps count of the number of vertices while building triangles (helps for the loop)

        //Creating a 2D grid with numDivision rows and cols
        for (int i = 0; i <= numDivisions; i++) {
            
            for (int j = 0; j <= numDivisions; j++) {

                //Inititlizing the vertices in verts as (0,0,0), (1,0,0)....(0,0,1), (1,0,1)....
                verts[i * (numDivisions + 1) + j] = new Vector3(j, 0, i);
                //heightMap[i, j] = 0;

                //if statement to avoid the last vertices in every row
                if (i<(numDivisions - 1) && j<(numDivisions - 1)) {

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

        //Initializing the height of the four corners of the grid to a random value
        verts[0].y = Random.Range(-terrainHeight, terrainHeight);
        verts[numDivisions].y = Random.Range(-terrainHeight, terrainHeight);
        verts[verts.Length - 1].y = Random.Range(-terrainHeight, terrainHeight);
        verts[verts.Length - 1 - numDivisions].y = Random.Range(-terrainHeight, terrainHeight);

        //number of iterations (one iteration is a combination of a diamond and square step) required
        int numOfIterations = (int)Mathf.Log(numDivisions, 2);
        int numOfSquares = 1; //Initially there is only one square which is the entire grid
        int divisionsInEachSquare = numDivisions; //Initially the entire drid which had numDivision divisions

        for (int i = 0; i < numOfIterations; i++) {

            int row = 0;
            for (int j = 0; j < numOfSquares; j++) {

                int col = 0;
                for (int k = 0; k < numOfSquares; k++) {
                    
                    //Half the number of divisions in each square
                    int halfDivisionsInEachSquare = (int)(divisionsInEachSquare * 0.5f);

                    //Defining the mid point of the grid in the current iteration
                    int mid = (row + halfDivisionsInEachSquare) * (numDivisions + 1) + (col + halfDivisionsInEachSquare);

                    //Defining the topLeft, topRight, botLeft and botRight for each suare in the current iteration
                    int botLeftCorner = row * (numDivisions + 1) + col;
                    int botRightCorner = row * (numDivisions + 1) + (col + divisionsInEachSquare);
                    int topLeftCorner = (row + divisionsInEachSquare) * (numDivisions + 1) + col;
                    int topRightCorner = (row + divisionsInEachSquare) * (numDivisions + 1) + (col + divisionsInEachSquare);

                    //Averaging out the topLeft, botLeft, topRight and botRight corners to find the mid.y (Diamond step)
                    verts[mid].y = ((verts[topLeftCorner].y + verts[topRightCorner].y + verts[botLeftCorner].y + verts[botRightCorner].y) / 4) + Random.Range(-terrainHeight, terrainHeight);

                    //Averaging out different points to find the botMid, topMid, rightMid and leftMid points (Square step)
                    verts[topLeftCorner + halfDivisionsInEachSquare].y = ((verts[topLeftCorner].y + verts[mid].y + verts[topRightCorner].y) / 3) + Random.Range(-terrainHeight, terrainHeight); 
                    verts[botLeftCorner + halfDivisionsInEachSquare].y = ((verts[botLeftCorner].y + verts[mid].y + verts[botRightCorner].y) / 3) + Random.Range(-terrainHeight, terrainHeight);
                    verts[mid + halfDivisionsInEachSquare].y = ((verts[topRightCorner].y + verts[mid].y + verts[botRightCorner].y) / 3) + Random.Range(-terrainHeight, terrainHeight);
                    verts[mid - halfDivisionsInEachSquare].y = ((verts[topLeftCorner].y + verts[mid].y + verts[botLeftCorner].y) / 3) + Random.Range(-terrainHeight, terrainHeight);

                    col += divisionsInEachSquare;
                }

                row += divisionsInEachSquare;
            }

            numOfSquares *= 2;
            divisionsInEachSquare /= 2;
            terrainHeight *= 0.5f;
        }

        for (int i = 0; i <= numDivisions; i++) {
            for (int j = 0; j <= numDivisions; j++) {
                heightMap[i, j] = verts[i * (numDivisions + 1) + j].y;
                //Debug.Log(i + " " + j + " " + heightMap[i, j]);
            }
        }

          mesh.vertices = verts;
          mesh.triangles = tris;

          mesh.RecalculateBounds();
          mesh.RecalculateNormals();
    }
}