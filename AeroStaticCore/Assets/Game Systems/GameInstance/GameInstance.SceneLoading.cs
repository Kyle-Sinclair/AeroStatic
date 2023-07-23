using System.Collections.Generic;
using Game_Systems.Services;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance : MonoBehaviour {
        // Partial class file for Game Instance that handles all scene opening and closing

        public int LoadProgress { get; set; }
        //public int TotalLoadingSteps { get; set; }

        public delegate void InitializeOnceLevelSetActive();
        InitializeOnceLevelSetActive _onSceneSetAsActive;

        private void OpenMainMenu() {
            if (SceneManager.GetSceneByName("MapScene").IsValid()) {
                SceneManager.UnloadSceneAsync("MapScene");
            }

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }

        public void StartNewMap() {
            OpenMapScene();
        }
        
        private void OpenMapScene() {
            //Raise a black curtain
            //Initialize and register any services the coming scene will require - So a Map Creator, a map manager, an agent manager, a narrative manager
            LoadProgress = 0;
          
            //Hook up any connections between these entites as needed
            //Load the scene
            //when loading is done, drop the curtain
            SceneManager.UnloadSceneAsync("MainMenu");
            StartCoroutine(OpenMapSceneAsync());

        }
        private IEnumerator OpenMapSceneAsync() {

            //
            //Hook up any connections between these entites as needed
            //Load the scene
            //when loading is done, drop the curtain
            AsyncOperation LoadOp = SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Additive);
            LoadOp.allowSceneActivation = false;

            while (!LoadOp.isDone) {
                if (LoadOp.progress !< 90) {
                    LoadOp.allowSceneActivation = true;
                }
                yield return null;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MapScene"));
            LoadProgress++;
            Debug.Log(LoadProgress);
        }
        private void CloseScene() {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            throw new System.NotImplementedException();
        }
    }
}
