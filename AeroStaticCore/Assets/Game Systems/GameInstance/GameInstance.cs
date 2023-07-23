using Game_Systems.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance {
        [SerializeField]
        private MapCreator _mapCreatorService;
        [SerializeField]
        private InputHandlerService _inputHandlerService;

        // Start is called before the first frame update
        void Awake() {
            ServiceLocator.Current.Register(_mapCreatorService);
            ServiceLocator.Current.Register(_inputHandlerService);

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
