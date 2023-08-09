using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreMapCreator", menuName = "Services/Core Map Creator", order = 1)]
        
    public class MapCreator : GameService {
        
        [Header("Environment Art Assets")]
        [SerializeField] private Material[] _gridMaterials;
        [SerializeField] private Material[] _planeMaterials;
    
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
            
            BasePlaneRef.transform.SetLocalPositionAndRotation(new Vector3((float)_mapSize/2, 0f, (float)_mapSize/2), Quaternion.Euler(90f,0f,0f));
            BasePlaneRef.transform.localScale = new Vector3(_mapSize,_mapSize,1);
            
            CarveOutPlatforms();
            ServiceLocator.Current.Get<MapManager>().RecieveMap(BasePlaneRef);
        }

        private List<HashSet<int3>> CarveOutPlatforms() {

            switch (_mapSpawnType) {
                case MapSpawnType.Quadrants:
                    CarveQuadrants();
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

        private List<HashSet<int3>> CarveQuadrants() {
            List<HashSet<int3>> QuadrantIslands = new List<HashSet<int3>>();
            
            //Make island 1 in lower left quadrant so x between 0 and half dimension,
            //z between o and half dimensions
            Random range = new Random();
            int xSeedCoord = range.Next(0, _mapSize / 2);
            int ySeedCoord = 0;
            int zSeedCoord = range.Next(0, _mapSize / 2);
            if (_debugIslandCreation) {
                Debug.Log("Creating island at x-coord: " + xSeedCoord + ", z-coord: " + zSeedCoord);
            }

            //Instantiate( GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(xSeedCoord, 0, zSeedCoord), Quaternion.identity);
            GenerateMeshOfPlatform(xSeedCoord, ySeedCoord, zSeedCoord);
            //for(int i = )
            return null;
        }

        private void GenerateMeshOfPlatform(int xSeedCoord, int ySeedCoord, int zSeedCoord) {

            GameObject island = new GameObject("Tested Island");
            island.transform.position = new Vector3(xSeedCoord, ySeedCoord, zSeedCoord);
            Mesh islandMesh = new Mesh();
            
            island.AddComponent<MeshFilter>().mesh = islandMesh;
            island.AddComponent<MeshRenderer>().material = _gridMaterials[0];
            
            MeshGenerator meshGenerator = ServiceLocator.Current.Get<MeshGenerator>();
            Tuple<List<Vector3>, List<int>> results = meshGenerator.GenerateIslandMeshData(xSeedCoord, ySeedCoord, zSeedCoord);
            
            Debug.Log("Vertex list contains " + results.Item1.Count);
            Debug.Log("Triangles list contains " + results.Item2.Count);
            islandMesh.vertices = results.Item1.ToArray();
            islandMesh.triangles = results.Item2.ToArray();
            islandMesh.RecalculateNormals();
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
