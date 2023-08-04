using Game_Systems.Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance {
        [SerializeField]
        private MapCreator _mapCreatorService; 
        [SerializeField]
        private CameraManager _cameraManagerService;
        [SerializeField]
        private InputHandlerService _inputHandlerService;
        [SerializeField]
        private GameCamera _gameCamera;
        // Start is called before the first frame update
        void Awake() {
            ServiceLocator.Current.Register(_mapCreatorService);
            ServiceLocator.Current.Register(_inputHandlerService);
            ServiceLocator.Current.Register(_cameraManagerService);

            OpenMainMenu();
            
        }
        
       
        public GameInstance GetGameInstance() {
            return this;
        }
        
        
        
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() {
            ServiceLocator.Initialize();
            if (ServiceLocator.Current == null) {
                Debug.LogError($"Service Locator failed to initialize");
                return;
            }

        }
    }
}
