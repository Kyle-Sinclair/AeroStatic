using Game_Systems.GameInstance;
using UnityEngine;

namespace MainMenuUI.Scripts {
    public class MainMenuController : MonoBehaviour {
    
        [SerializeField] private GameInstance _gameInstance;
        // Start is called before the first frame update
        void Start() {
            _gameInstance = FindObjectOfType<GameInstance>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StartNewGame() {
            _gameInstance.StartNewMap();
        }
    }
}
