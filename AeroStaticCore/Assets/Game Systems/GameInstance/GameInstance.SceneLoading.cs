using System.Collections.Generic;
using Game_Systems.Services;
using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

namespace Game_Systems.GameInstance {
    public partial class GameInstance : MonoBehaviour {
        // Partial class file for Game Instance that handles all scene opening and closing


       
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
            MapCreator mc = new MapCreator();
            ServiceLocator.Current.Register(mc);
            //Hook up any connections between these entites as needed
            //Load the scene
            //when loading is done, drop the curtain
            SceneManager.UnloadSceneAsync("MainMenu");
            StartCoroutine(OpenMapSceneAsync());

        }
        private IEnumerator OpenMapSceneAsync() {
          
            //Hook up any connections between these entites as needed
            //Load the scene
            //when loading is done, drop the curtain
            AsyncOperation LoadOp = SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Additive);
            LoadOp.allowSceneActivation = false;
            while (!LoadOp.isDone) {
                if(LoadOp.progress !< 90){
                    if (Input.GetKey(KeyCode.Space)) {
                        LoadOp.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }

        private void CloseScene() {


        }
    }
}
