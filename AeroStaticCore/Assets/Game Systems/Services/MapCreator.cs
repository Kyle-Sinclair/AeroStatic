using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using Utility;
using Random = System.Random;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreMapCreator", menuName = "Services/Core Map Creator", order = 1)]
        
    public class MapCreator : GameService {
        
        [Header("Environment Art Assets")]
        [SerializeField] private Material[] _gridMaterials;
        [SerializeField] private Material[] _planeMaterials;

        [SerializeField] private MeshGenerationFunctions.MeshGenFunctionName _meshGenFunctionName;
    
        private int _mapSize;

        [Serializable]
        public enum MapSpawnType {
            Quadrants
        };

        [Header("Map Type")]
        [SerializeField]
        private MapSpawnType _mapSpawnType = MapSpawnType.Quadrants;

        [Header("Debug Options")]
        [SerializeField] 
        private bool _debugIslandCreation = false;
        // Start is called before the first frame update
        public void CreateMap() {
            GameObject BasePlane = Resources.Load<GameObject>("Art/Boards/BoardPrefabs/BaseQuad");
            GameObject BasePlaneRef = Instantiate(BasePlane);
            BasePlaneRef.GetComponent<MeshRenderer>().material = _planeMaterials[0];
            
            BasePlaneRef.transform.SetLocalPositionAndRotation(new Vector3((float)_mapSize/2, 0f, (float)_mapSize/2), Quaternion.Euler(90f,0f,0f));
            BasePlaneRef.transform.localScale = new Vector3(_mapSize,_mapSize,1);
            
            GameObject[] islands = CarveOutPlatforms();
            
            
            ServiceLocator.Current.Get<MapManager>().RecieveMap(BasePlaneRef, islands);
        }

        private GameObject[] CarveOutPlatforms() {

            switch (_mapSpawnType) {
                case MapSpawnType.Quadrants:
                    return CarveQuadrants();
                    break;

                default: return null;
            }
            
            return null;

        }
        /*private List<HashSet<int3>> CarveOutPlatforms() {

            switch (_mapSpawnType) {
                case MapSpawnType.Quadrants:
                    CarveQuadrants();
                    break;

                default: return null;
            }
            
            return null;

        }*/

        private GameObject[] CarveQuadrants() {
            List<GameObject> islands = new List<GameObject>();
            //Make island 1 in lower left quadrant so x between 0 and half dimension,
            //z between o and half dimensions
            
            Random range = new Random();
            //TODO:Turn this into a for loop. Can probably generate the first position as a vector pointing from
            //map center to first quadrant and then rotate each one by i * 90 degrees
                int halfMapSize = _mapSize / 2;
                int xSeedCoord = range.Next(2, halfMapSize - 2);
                int ySeedCoord = 1;
                int zSeedCoord = range.Next(2, halfMapSize -2);
               
                islands.Add(GenerateMeshOfPlatform(3, xSeedCoord, ySeedCoord, zSeedCoord));
                xSeedCoord = range.Next(halfMapSize + 2, _mapSize - 2);
                zSeedCoord = range.Next(2, halfMapSize - 2);
                /*
                islands.Add(GenerateMeshOfPlatform(range.Next(3,7), xSeedCoord, ySeedCoord, zSeedCoord));
                xSeedCoord = range.Next(_mapSize/2  + 2, _mapSize - 2);
                zSeedCoord = range.Next(_mapSize /2 + 2 , _mapSize - 2);
                islands.Add(GenerateMeshOfPlatform(range.Next(3,7), xSeedCoord, ySeedCoord, zSeedCoord));  
                xSeedCoord = range.Next(2, halfMapSize - 2);
                zSeedCoord = range.Next(halfMapSize + 2 , _mapSize- 2);
                islands.Add(GenerateMeshOfPlatform(range.Next(3,7), xSeedCoord, ySeedCoord, zSeedCoord));
                */
            
                return islands.ToArray();
                
            //for(int i = )
        }

        private GameObject GenerateMeshOfPlatform(int dimensions, int xSeedCoord, int ySeedCoord, int zSeedCoord) {
            DateTime beginTime=  DateTime.Now;
            
            GameObject island = new GameObject("Tested Island");
            island.transform.position = new Vector3(xSeedCoord, ySeedCoord, zSeedCoord);
            Mesh islandMesh = new Mesh();
            
            island.AddComponent<MeshFilter>().mesh = islandMesh;
            island.AddComponent<MeshRenderer>().material = _gridMaterials[0];
            
            MeshGenerator meshGenerator = ServiceLocator.Current.Get<MeshGenerator>();
            MeshGenerationFunctions.MeshGeneratorFunction function = MeshGenerationFunctions.GetFunction(_meshGenFunctionName);
            Vector3Int sizes = new Vector3Int(dimensions, dimensions, dimensions);
            Tuple<Vector3[], Vector2[],int[]> results = meshGenerator.GenerateIsland(function, sizes, xSeedCoord, ySeedCoord, zSeedCoord);
        
            islandMesh.vertices = results.Item1;
            islandMesh.uv = results.Item2;
            islandMesh.triangles = results.Item3;
            
            islandMesh.RecalculateNormals();
            
            DateTime endTime =  DateTime.Now;
            TimeSpan taken = endTime.Subtract(beginTime);
            Debug.Log("Time taken for mesh generation was " + taken);

            return island;
            

        }

        private void EstablishBounds(int boardDimensions) {
            if (boardDimensions % 2 == 0) {
                
            }
            
        }
        /*metalayer ideas 
        
        Science, Defence, Plunder Missions
        Hero units - commanders, have specialties
        
        Do units flock to them? Bad game design, excellent programming thing
        
        Commanders should be built as a particular kind of class, maybe like a card scriptable object?
        
        Look into that
        
        Create terrain as platforms first, that's easier. 
        
        */
        public override void ConfigureService(GameInstance.GameInstance gameInstance) {
            _mapSize = gameInstance._mapConfig._mapSize;
        }
    }
}
