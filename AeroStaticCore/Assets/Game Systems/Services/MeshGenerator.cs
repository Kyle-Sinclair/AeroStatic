using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Utility;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreMeshGenerator", menuName = "Services/Core Mesh Generator", order = 5)]

    public class MeshGenerator : GameService
    {
        //Method that generates vertices for a particular square
        //Method that adds those vertices in the correct order to the vertices list
        //method that adds the trianges correctly for a squarew
        
        //TODO: Add an enum for island size that influences the row length of island tops.
        public Tuple<Vector3[], Vector2[], int[]> GenerateIsland(MeshGenerationFunctions.MeshGeneratorFunction function, Vector3Int dimensions, int xSeedCoord, int ySeedCoord, int zSeedCoord) {
            
            return function(dimensions,xSeedCoord,ySeedCoord,zSeedCoord);
        }
        
        
        public override void ConfigureService(GameInstance.GameInstance gameInstance) {
            
        }
    }
}
