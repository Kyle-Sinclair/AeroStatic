using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
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
        [SerializeField] private int _gridSelection;

        [SerializeField] private Material[] _gridMaterials;

        [SerializeField] private Material[] _planeMaterials;
        [SerializeField] private int _islandSize = 0;
        [SerializeField] private MeshGenerationFunctions.MeshGenFunctionName _meshGenFunctionName;
    
        
        public Texture2D NoiseSource;

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

        private Random _range;

        public void CreateMap() {
            _range = new Random();
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
            
            int halfMapSize = _mapSize / 2;
            Vector3[] spawnPoints = new Vector3[4];
            int ySeedCoord = 0;

            spawnPoints[0] = new Vector3(_range.Next(2, halfMapSize - 2),ySeedCoord,_range.Next(2, halfMapSize - 2));
            spawnPoints[1] = new Vector3(_range.Next(halfMapSize + 2, _mapSize - 2),ySeedCoord,_range.Next(2, halfMapSize - 2));
            spawnPoints[2] = new Vector3(_range.Next(_mapSize/2  + 2, _mapSize - 2),ySeedCoord,_range.Next(_mapSize /2 + 2 , _mapSize - 2));
            spawnPoints[3] = new Vector3(_range.Next(2, halfMapSize - 2),ySeedCoord,_range.Next(halfMapSize + 2 , _mapSize- 2));

            Vector3 spawnPoint;
            
            for (int i = 0; i < 4; i++) {
                    spawnPoint = spawnPoints[i];
                    if (_islandSize == 0) {
                        islands.Add(GenerateMeshOfPlatform(_range.Next(3,10), spawnPoint));
                    }
                    else {
                        islands.Add(GenerateMeshOfPlatform(_islandSize, spawnPoint));

                    }
            }

                
            
            return islands.ToArray();
                
        }

        private GameObject GenerateMeshOfPlatform(int dimensions,Vector3 spawnPoint) {
            DateTime beginTime=  DateTime.Now;
            
            GameObject island = new GameObject("Tested Island");
            island.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            Mesh islandMesh = new Mesh();
            
            island.AddComponent<MeshFilter>().mesh = islandMesh;
            island.AddComponent<MeshRenderer>().material = _gridMaterials[_gridSelection];
            
            MeshGenerator meshGenerator = ServiceLocator.Current.Get<MeshGenerator>();
            MeshGenerationFunctions.MeshGeneratorFunction function = MeshGenerationFunctions.GetFunction(_meshGenFunctionName);
            Vector3Int sizes = new Vector3Int(dimensions, dimensions, dimensions);
            Tuple<Vector3[], Vector2[],int[]> results = meshGenerator.GenerateIsland(function, sizes, (int)spawnPoint.x, (int)spawnPoint.y, (int)spawnPoint.z);
        
            islandMesh.vertices = results.Item1;
            islandMesh.uv = results.Item2;
            islandMesh.triangles = results.Item3;
            
            islandMesh.RecalculateNormals();
            List<Vector3> vertices = new List<Vector3>();
            islandMesh.GetVertices(vertices);
         
            DateTime endTime =  DateTime.Now;
            TimeSpan taken = endTime.Subtract(beginTime);
            Debug.Log("Time taken for mesh generation was " + taken);
            return island;
        }
        private void EstablishBounds(int boardDimensions) {
            if (boardDimensions % 2 == 0) {
                
            }
        }
        
       
        /*
        metalayer ideas 
        Science, Defence, Plunder Missions
        Hero units - commanders, have specialties
        Do units flock to them? Bad game design, excellent programming thing
        Commanders should be built as a particular kind of class, maybe like a card scriptable object?
        Look into that    
        Create terrain as platforms first, that's easier. 
        */
        public override void ConfigureService(GameInstance.GameInstance gameInstance) {
            _mapSize = gameInstance._mapConfig._mapSize;
            MeshGenerationFunctions.NoiseSource = this.NoiseSource;
        }
        
        void OnEnable () {
            Debug.Log("Updating noise source");
            MeshGenerationFunctions.NoiseSource =  this.NoiseSource;
        }

    }
}
