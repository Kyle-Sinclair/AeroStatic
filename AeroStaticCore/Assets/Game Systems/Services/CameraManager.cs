using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreCameraManager", menuName = "Services/Core Camera Manger", order = 2)]
        
    public class CameraManager : GameService {
        
        [SerializeField] private GameObject _gameCameraPrefab;
        [SerializeField] private GameObject _appCamera; 
        
        private GameCamera _inGameCamera = null;
        private ApplicationCamera _inMenuAppCamera = null;
        
        // Start is called before the first frame update
        public void CreateGameCamera() {
            GameObject obj =  Instantiate(_gameCameraPrefab);
            GameCamera gc = obj.GetComponent<GameCamera>();
            if (!gc) {
                Debug.LogError("The game camera prefab has the incorrect type - " +
                                        "make sure you have dragged a game camera object into the correct camera service field");
            }

            _inGameCamera = gc;
        }
        
        public void CreateAppCamera() {
            GameObject obj =  Instantiate(_appCamera);
            ApplicationCamera ac = obj.GetComponent<ApplicationCamera>();
            if (!ac) {
                Debug.LogError("The app camera prefab has the incorrect type - " +
                               "make sure you have dragged an app camera object into the correct camera service field");
            }
            _inMenuAppCamera = ac;
        }

        public void SwapToGameCamera() {
            _inGameCamera.EnableCamera();
            _inMenuAppCamera.DisableCamera(); 
        }

        public void SwapToAppCamera() {
            _inGameCamera.DisableCamera();
            _inMenuAppCamera.EnableCamera();
        }
       
    }
}
