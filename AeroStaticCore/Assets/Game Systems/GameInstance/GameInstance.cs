using System;
using Config;
using Game_Systems.Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance {
        
        [Header("Core Services")]

        [SerializeField]
        private MapCreator _mapCreatorService; 
        [SerializeField]
        private CameraManager _cameraManagerService;
        [SerializeField]
        private InputHandlerService _inputHandlerService;
        [SerializeField]
        private MapManager _mapManagerService; 
        [SerializeField]
        private MeshGenerator _meshGeneratorService;
        
        [Header("Core Prefabs")]

        [SerializeField]
        private GameCamera _gameCamera;

        [Header("Configurable Values")]
        [SerializeField]
        public Configuration _mapConfig;

        // Start is called before the first frame update
        void Awake() {
            RegisterServices();
            InitializeServices();
            OpenMainMenu();
        }
        public GameInstance GetGameInstance() {
            return this;
        }

        void RegisterServices() {
            ServiceLocator.Current.Register(_mapCreatorService);
            ServiceLocator.Current.Register(_mapManagerService);
            ServiceLocator.Current.Register(_meshGeneratorService);
            ServiceLocator.Current.Register(_inputHandlerService);
            ServiceLocator.Current.Register(_cameraManagerService);
        }

        void InitializeServices() {
            ServiceLocator.Current.ConfigureServices(this);
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

        private void OnValidate() {
            if (_mapCreatorService == null) {
                throw new Exception();
            }
            if (_cameraManagerService == null) {
                throw new Exception();
            }
            if (_inputHandlerService == null) {
                throw new Exception();
            }
            if (_gameCamera == null) {
                throw new Exception();
            }
            if (_mapConfig == null) {
                throw new Exception();
            }
            if (_mapManagerService == null) {
                throw new Exception();
            }
            if (_meshGeneratorService == null) {
                throw new Exception();
            }
        }
    }
}
