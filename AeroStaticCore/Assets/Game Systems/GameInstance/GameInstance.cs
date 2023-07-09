using Game_Systems.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance : MonoBehaviour {
        // Start is called before the first frame update
        void Awake() {

            OpenMainMenu();
        }

       
        public GameInstance GetGameInstance() {
            return this;
        }
        
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize() {
            ServiceLocator.Initialize();

            if (ServiceLocator.Current == null) {
                Debug.LogError($"Service Locator failed to initialize");

            }
        }
    }
}
