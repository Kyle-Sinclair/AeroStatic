using System;
using System.Collections.Generic;
using Config;
using Game_Systems.Services;
using Unity.Collections;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;


namespace Game_Systems {
    [CreateAssetMenu(fileName = "CoreMapManager", menuName = "Services/Core Map Manager", order = 2)]

    public class MapManager : GameService {
        private int _dimensions;
        private int _numberOfBuildableLayers;

        public GameObject BaseBoard { get; private set; }
        public GameObject[] Islands { get; private set; }
        [SerializeField] private bool DEBUG_NATIVE_SETS = false;

        //private enum biome
        [Serialize] private NativeHashSet<int3> _passableTiles;

        [Serialize] public int3[] _debugPassableTiles;

        [Serialize] private NativeHashSet<int3> _unpassableTiles;

        //private NativeHashMap<Buildings> _buildings;
        public override void ConfigureService(GameInstance.GameInstance gameInstance) {
            Configuration config = gameInstance._mapConfig;
            _dimensions = config._mapSize;
            _numberOfBuildableLayers = config._numberOfBuildableLayers;
            _passableTiles = new NativeHashSet<int3>(_dimensions * _dimensions, Allocator.Persistent);
            _unpassableTiles = new NativeHashSet<int3>(_dimensions * _dimensions, Allocator.Persistent);

            if (DEBUG_NATIVE_SETS) {
                _debugPassableTiles = new int3[_passableTiles.Count];
            }
            else {
                _debugPassableTiles = Array.Empty<int3>();
            }

        }

        public void RecieveMap(GameObject incomingBaseBoard, GameObject[] islands ){
            BaseBoard = incomingBaseBoard;
            Islands = islands;
        }

        private void OnDisable() {
            _passableTiles.Dispose();
            _unpassableTiles.Dispose();
        }
        
        [ContextMenu("Recreate Map")]
        private void RecreateMap() {
            Destroy(BaseBoard);
            foreach (var island in Islands) {
                Destroy(island);
            }

            Islands = null;
            _passableTiles.Dispose();
            _unpassableTiles.Dispose();
            _passableTiles = new NativeHashSet<int3>(_dimensions * _dimensions, Allocator.Persistent);
            _unpassableTiles = new NativeHashSet<int3>(_dimensions * _dimensions, Allocator.Persistent);

            ServiceLocator.Current.Get<MapCreator>().CreateMap();
        }
    }
}
