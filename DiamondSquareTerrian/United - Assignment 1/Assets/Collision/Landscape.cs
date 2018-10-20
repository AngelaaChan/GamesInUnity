//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.Assertions;
//using UnityEngine;

//public class Landscape : MonoBehaviour
//{
//    // number of iterations
//    public int detail = 9;
//    // max height of terrain
//    public int height = 50;

//    /// <summary>
//    /// Terrain.
//    /// Use 2D array to store vertices which can be converted to 1D
//    /// </summary>
//    class Terrain
//    {
//        public readonly int size;
//        public readonly int max;
//        public readonly int height;
//        public readonly int detail;
//        public int iteration = 0;
//        public float[,] heightMap;

//        public Terrain(int detail, int height)
//        {
//            this.detail = detail;
//            this.size = (int)Mathf.Pow(2, detail) + 1;
//            this.max = this.size - 1; // maximum index
//            this.height = height;
//            this.heightMap = new float[size, size];
//        }

//        /// <summary>
//        /// Convert and return a one dimentional array.
//        /// Both arrays stil reference to the same vector object
//        /// Used for mesh.tris
//        /// </summary>
//        /// <returns>The array.</returns>
//        public Vector3[] GetArray()
//        {
//            Vector3[] array = new Vector3[size * size];
//            int count = 0;
//            int n_row = size;
//            for (int i = 0; i < size; i++)
//            {
//                for (int j = 0; j < size; j++)
//                {
//                    // assign vectors to the rightly 1d array place
//                    array[i * size + j] = new Vector3(i, heightMap[i, j], j);
//                    count += 1;
//                }
//            }
//            return array;
//        }

//        /// <summary>
//        /// Converts the index of the 2D array to index of the 1D array.
//        /// </summary>
//        /// <returns>The to array index.</returns>
//        /// <param name="row">Row.</param>
//        /// <param name="col">Col.</param>
//        public int ConvertToArrayIndex(int row, int col)
//        {
//            return row * this.size + col;
//        }
//    }


//    void Start()
//    {
//        Terrain land = new Terrain(detail, height);
//        DimondSquare(land);
//    }

//    /// <summary>
//    /// Dimond square algorithm
//    /// Assign value directly on Terrain object
//    /// </summary>
//    /// <param name="land">Landscape.</param>
//    void DimondSquare(Terrain land)
//    {
//        // initialize the four corners
//        land.heightMap[0, 0] = 0;
//        land.heightMap[0, land.max] = 0;
//        land.heightMap[land.max, 0] = 0;
//        land.heightMap[land.max, land.max] = 0;

//        // start diamond square algorithm
//        Diamond(land, 0, 0, land.max, land.max);
//    }

//    void Diamond(Terrain land, int midU, int midV)
//    {
//        // base case
//        if (land.iteration == land.detail)
//        {
//            return;
//        }

//        land.iteration += 1;

//        // find mid point
//        int midU = (midU + botU) / 2;
//        int midV = (midV + botV) / 2;
//        // find the average value
//        float average = (land.heightMap[midU, midV]
//                         + land.heightMap[botU, botV]
//                         + land.heightMap[botU, midV]
//                         + land.heightMap[midU, botV]) / 4.0f;
//        // assign mid point a value
//        land.heightMap[midU, midV] = average + Random.value * land.height;

//        // call square algorithm four times
//        Square(land, midU, midV, );
//        Square(land, midU, midV, );
//        Square(land, midU, midV, );
//        Square(land, midU, midV, );

//    }

//    void Square(Terrain land, int topU, int topV, int botU, int botV, int midU, int midV)
//    {
//        Assert.IsTrue(botV == topV);
//        // find the center point 
//        int centU = topU;
//        int centV = (topV + botV) / 2;
//        Assert.IsTrue(centV == midV);

//        // find the average value
//        float average = (land.heightMap[topU, topV]
//                         + land.heightMap[botU, botV]
//                         + land.heightMap[midU, midV]) / 3.0f;
//        // assign center point a value
//        land.heightMap[centU, centV] = average + Random.value * land.height;
//    }

//}