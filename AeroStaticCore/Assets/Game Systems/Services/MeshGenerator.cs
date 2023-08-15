using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreMeshGenerator", menuName = "Services/Core Mesh Generator", order = 5)]

    public class MeshGenerator : GameService
    {

        //Method that generates vertices for a particular square
        //Method that adds those vertices in the correct order to the vertices list
        //method that adds the trianges correctly for a squarew
        
        public Tuple<List<Vector3>, List<int>> GenerateIslandMeshData(int xSeedCoord, int ySeedCoord, int zSeedCoord) {
            //TODO: Add an enum for island size that influences the row length of island tops.
            
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
       
        Vector3 v1 = new Vector3(0, 0, 0);
        Vector3 v2 = new Vector3(0, 0,  1);
        Vector3 v3 = new Vector3( 1, 0, 0);
        Vector3 v4 = new Vector3( 1, 0, 0 + 1);
        Debug.Log(v1);
        Debug.Log(v2);
        Debug.Log(v3);
        Debug.Log(v4);
        AddSquareTriangles(ref vertices, ref triangles, v1, v2, v3, v4);
        
        return new Tuple<List<Vector3>, List<int>>(vertices,triangles);
        }
        
        //TODO: Add an enum for island size that influences the row length of island tops.
        public Tuple<List<Vector3>, List<int>> GenerateIslandMeshData(int dimensions, int xSeedCoord, int ySeedCoord, int zSeedCoord) { 
            
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            int z = 0;
            int x = 0;
            for (int i = 0; i < dimensions * dimensions; i++) {

                z = i / 3; 
                x = i % 3;
                Vector3 v1 = new Vector3(x, 0, z);
                Vector3 v2 = new Vector3(x, 0, z + 1);
                Vector3 v3 = new Vector3(x + 1, 0, z);
                Vector3 v4 = new Vector3(x + 1, 0, z + 1);
                AddSquareTriangles(ref vertices, ref triangles, v1, v2, v3, v4); 
                
            }

        return new Tuple<List<Vector3>, List<int>>(vertices,triangles);
        }
        void AddSquareTriangles (ref List<Vector3> vertices,ref List<
        int> triangles, Vector3 v1, Vector3 v2, Vector3 v3,Vector3 v4) {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 3);
            
            
        }
        
        
        

        public override void ConfigureService(GameInstance.GameInstance gameInstance) {
            
        }
    }
}
