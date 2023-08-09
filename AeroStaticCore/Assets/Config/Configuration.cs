using UnityEngine;

namespace Config {
    [CreateAssetMenu(fileName = "CoreConfiguration",menuName = "Create new map Configuration")]
    public class Configuration : ScriptableObject {
        [Header("Map Variables")] 
        [SerializeField,Range(10,100)]
        public int _mapSize;
        
        [SerializeField,Range(1,3)]
        public int _numberOfBuildableLayers;
    }
}
