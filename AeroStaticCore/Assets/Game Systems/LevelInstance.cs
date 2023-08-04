using Game_Systems.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game_Systems {
    public class LevelInstance : MonoBehaviour
    {
        
        
        void Start()
        {
            print("Initializer active");
            SceneManager.activeSceneChanged += OnActiveSceneChanged;

        }


        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.Scene currentScene) {
            Debug.Log("The map scene should be active at this point");
            
            
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
