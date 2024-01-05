using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility {
    public static class MeshGenerationFunctions {

        public delegate Tuple<Vector3[], Vector2[], int[]> MeshGeneratorFunction(Vector3Int dimensions, int xSeedCoord,
            int ySeedCoord, int zSeedCoord);

        public enum MeshGenFunctionName {
            GenerateBaseIslandMeshData,
            GenerateBoxIslandMesh
        }

        public const float cellPerturbStrength = 2f;
        public const float noiseScale = 0.003f;

        public static Texture2D NoiseSource;

        private static MeshGeneratorFunction[] _meshGenFunctions =
            { GenerateBaseIslandMeshData, GenerateBoxIslandMeshData };

        public static MeshGeneratorFunction GetFunction(MeshGenFunctionName index) {
            return _meshGenFunctions[(int)index];
        }

        public static Tuple<Vector3[], Vector2[], int[]> GenerateBaseIslandMeshData(Vector3Int sizes, int xSeedCoord,
            int ySeedCoord, int zSeedCoord) {
            int dimensions = sizes.x;
            Vector3[] vertices = new Vector3[(dimensions + 1) * (dimensions + 1)];
            int[] triangles = new int[dimensions * dimensions * 6];
            Vector2[] uv = new Vector2[vertices.Length];

            for (int i = 0, z = 0; z <= dimensions; z++) {
                for (int x = 0; x <= dimensions; x++, i++) {
                    vertices[i] = new Vector3(x, 0.1f + ySeedCoord, z);
                    uv[i] = new Vector2((float)x / dimensions, (float)z / dimensions);

                }
            }

            for (int ti = 0, vi = 0, y = 0; y < dimensions; y++, vi++) {
                for (int x = 0; x < dimensions; x++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + dimensions + 1;
                    triangles[ti + 5] = vi + dimensions + 2;
                }
            }

            return new Tuple<Vector3[], Vector2[], int[]>(vertices, uv, triangles);
        }


        //Naive implementation. Here for educational purposes!
        //Generate faces on a cube face by face, vertex by vertex
        //Faces do not share vertices, so perturbing the vertices leads to gaps forming where faces connect
        public static Tuple<Vector3[], Vector2[], int[]> GenerateBoxIslandMeshData(Vector3Int sizes, int xSeedCoord,
            int ySeedCoord, int zSeedCoord) {


            int xSize = sizes.x;
            int ySize = sizes.y;
            int zSize = sizes.z;
            int verticesLength = /*Top face */ (xSize + 1) * (zSize + 1) + /* Side faces */
                                               2 * (xSize + 1) * (ySize + 1) + 2 * (zSize + 1) * (ySize + 1);
            int trianglesLength = (xSize * ySize * 6) * 2 + (xSize * zSize * 6) * 2 + (xSize * zSize * 6);
            //By having duplicate vertices we can assemble the cube as six planes
            Vector3[] vertices = new Vector3[verticesLength];
            int[] triangles = new int[trianglesLength];
            Vector2[] uv = new Vector2[vertices.Length];

            //Topface first
            int index = 0;
            for (int z = 0; z < zSize; z++) {
                for (int x = 0; x < xSize; x++, index++) {
                    Vector3 vertex = new Vector3(x, ySize - 1, z);
                    vertices[index] = TopPerturb(vertex, xSeedCoord + vertex.x, zSeedCoord + vertex.z);
                    uv[index] = new Vector2(vertices[index].x / (xSize - 1), vertices[index].z / (zSize - 1));
                }
            }

            int ti = 0;
            int vi = 0;
            for (int z = 0; z < zSize - 1; z++, vi++) {
                for (int x = 0; x < xSize - 1; x++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize;
                    triangles[ti + 5] = vi + xSize + 1;
                }
            }

            vi = index;
            //Left face
            for (int y = 0; y < ySize; y++) {
                for (int z = 0; z < zSize; z++, index++) {
                    Vector3 vertex = new Vector3(0, y, z);
                    vertices[index] = XAxisSidePerturb(vertex, ySeedCoord + vertex.y, zSeedCoord + vertex.z);
                    //vertices[index] = new Vector3(0, y, z);
                    uv[index] = new Vector2((float)z / (zSize - 1), (float)y / (ySize - 1));
                }
            }

            for (int y = 0; y < ySize - 1; y++, vi++) {
                for (int z = 0; z < zSize - 1; z++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 1] = triangles[ti + 4] = vi + 1;
                    triangles[ti + 2] = triangles[ti + 3] = vi + zSize;
                    triangles[ti + 5] = vi + zSize + 1;
                }
            }

            //Right Face
            vi = index;
            for (int y = 0; y < ySize; y++) {
                for (int z = 0; z < zSize; z++, index++) {

                    Vector3 vertex = new Vector3(xSize - 1, y, z);
                    vertices[index] = XAxisSidePerturb(vertex, ySeedCoord + vertex.y, zSeedCoord + vertex.z);
                    //vertices[index] = new Vector3(0, y, z);
                    uv[index] = new Vector2(vertices[index].z / (zSize - 1), vertices[index].y / (ySize - 1));
                }
            }

            for (int y = 0; y < ySize - 1; y++, vi++) {
                for (int z = 0; z < zSize - 1; z++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + zSize;
                    triangles[ti + 5] = vi + zSize + 1;
                    Debug.Log("Vertex index in loop is at " + vi);
                }
            }

            //Back face
            vi = index;
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++, index++) {
                    // vertices[index] = new Vector3(x, y, zSize - 1);
                    //  uv[index] = new Vector2((float)x / (xSize - 1), (float)y / (ySize - 1));

                    Vector3 vertex = new Vector3(x, y, zSize - 1);
                    vertices[index] = ZAxisSidePerturb(vertex, xSeedCoord + vertex.x, ySeedCoord + vertex.y);
                    uv[index] = new Vector2(vertices[index].x / (zSize - 1), vertices[index].y / (ySize - 1));
                }
            }

            for (int y = 0; y < ySize - 1; y++, vi++) {
                for (int z = 0; z < zSize - 1; z++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + zSize;
                    triangles[ti + 5] = vi + zSize + 1;
                }
            }

            //Front Face
            vi = index;
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++, index++) {
                    //vertices[index] = new Vector3(x, y, 0);
                    //uv[index] = new Vector2((float)x / (xSize - 1), (float)y / (ySize - 1));

                    Vector3 vertex = new Vector3(x, y, 0);
                    vertices[index] = ZAxisSidePerturb(vertex, xSeedCoord + vertex.x, ySeedCoord + vertex.y);
                    uv[index] = new Vector2(vertices[index].x / (zSize - 1), vertices[index].y / (ySize - 1));
                }
            }

            for (int y = 0; y < ySize - 1; y++, vi++) {
                for (int z = 0; z < zSize - 1; z++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 1] = triangles[ti + 4] = vi + 1;
                    triangles[ti + 2] = triangles[ti + 3] = vi + zSize;
                    triangles[ti + 5] = vi + zSize + 1;
                }
            }




            return new Tuple<Vector3[], Vector2[], int[]>(vertices, uv, triangles);
        }

        
        public static Tuple<Vector3[], Vector2[], int[]> GenerateUnderWallIslandMeshData(Vector3Int sizes,
            int xSeedCoord,
            int ySeedCoord, int zSeedCoord) {

            int xSize = sizes.x;
            int ySize = sizes.y;
            int zSize = sizes.z;
            int verticesLength = /*Top face */ (xSize + 1) * (zSize + 1) + /* Side faces */
                                               2 * (xSize + 1) * (ySize + 1) + 2 * (zSize + 1) * (ySize + 1);
            int trianglesLength = (xSize * ySize * 6) * 2 + (xSize * zSize * 6) * 2 + (xSize * zSize * 6);
            //By having duplicate vertices we can assemble the cube as six planes
            Vector3[] vertices = new Vector3[verticesLength];
            int[] triangles = new int[trianglesLength];
            Vector2[] uv = new Vector2[vertices.Length];

            int index = 0;
            for (int z = 0; z < zSize; z++) {
                for (int x = 0; x < xSize; x++, index++) {
                    Vector3 vertex = new Vector3(x, ySize - 1, z);
                    vertices[index] = TopPerturb(vertex, xSeedCoord + vertex.x, zSeedCoord + vertex.z);
                    uv[index] = new Vector2(vertices[index].x / (xSize - 1), vertices[index].z / (zSize - 1));
                }
            }

            int ti = 0;
            int vi = 0;
            for (int z = 0; z < zSize - 1; z++, vi++) {
                for (int x = 0; x < xSize - 1; x++, ti += 6, vi++) {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize;
                    triangles[ti + 5] = vi + xSize + 1;
                }
            }
            return new Tuple<Vector3[], Vector2[], int[]>(vertices, uv, triangles);
        }
        


        #region Perlin Noise Sampler Codes
        
        #endregion

            public static Vector4 SampleNoise(float positionX, float positionY) {
                return NoiseSource.GetPixelBilinear(positionX * noiseScale, positionY * noiseScale);
            }
            
            // These functions are designed to perturb vertices in a manner appropriate ot which orientation the 
            // vertex's face should have
            private static Vector3 TopPerturb(Vector3 position, float positionX, float positionY) {
                if (NoiseSource == null) {
                    Debug.LogError("No noise source");
                }

                Debug.Log("perturbed paraemters are : " + positionX + ", " + positionY);
                Vector4 sample = SampleNoise(positionX, positionY);
                Debug.Log("perturbed sample equals : " + sample);
                position.x += (sample.x * 2f - 1f) * cellPerturbStrength;
                position.y += (sample.y * 2f - 1f);
                position.z += (sample.z * 2f - 1f) * cellPerturbStrength;
                //Debug.Log("perturbed vertex equals : " + position);
                return position;
            }

            private static Vector3 XAxisSidePerturb(Vector3 position, float positionX, float positionZ) {
                if (NoiseSource == null) {
                    Debug.LogError("No noise source");
                }
                Vector4 sample = SampleNoise(positionX, positionZ);
                //Debug.Log("perturbed sample equals : " + sample);
                position.x += (sample.x * 2f - 1f);
                position.y += (sample.y * 2f - 1f) * cellPerturbStrength;
                position.z += (sample.z * 2f - 1f) * cellPerturbStrength;
                //Debug.Log("perturbed vertex equals : " + position);
                return position;
            }

            private static Vector3 ZAxisSidePerturb(Vector3 position, float positionY, float positionZ) {
                if (NoiseSource == null) {
                    Debug.LogError("No noise source");
                }

                Vector4 sample = SampleNoise(positionY, positionZ);
                Debug.Log("perturbed sample equals : " + sample);
                position.x += (sample.x * 2f - 1f) * cellPerturbStrength;
                position.y += (sample.y * 2f - 1f) * cellPerturbStrength;
                position.z += (sample.z * 2f - 1f);
                //Debug.Log("perturbed vertex equals : " + position);
                return position;
            }
        }
    }

    
   
