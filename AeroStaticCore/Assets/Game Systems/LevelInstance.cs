using Game_Systems.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game_Systems {
    public class LevelInstance : MonoBehaviour
    {
        void Start() {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.Scene currentScene) {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            GenerateMap();
        }
        private void GenerateMap() {
            ServiceLocator.Current.Get<MapCreator>().CreateMap();
        }
        private void WireInputToInputHandler() {
            
        }
    }
}
