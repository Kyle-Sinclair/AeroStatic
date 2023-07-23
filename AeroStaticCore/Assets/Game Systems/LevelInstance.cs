using Game_Systems.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Systems {
    public class LevelInstance : MonoBehaviour
    {
        delegate void MyDelegate();
        MyDelegate attack;
        
        void Start()
        {
            print("Initializer active");
            ServiceLocator.Current.Get<GameInstance.GameInstance>();
            //ServiceLocator.Current.Get<MapCreator>().CreateMap();

        }

        // public static void GetService(ServiceSystem WantedService) {
        //     
        // }

    }
}
