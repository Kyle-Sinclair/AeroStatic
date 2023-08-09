using System;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

namespace Game_Systems.Services {
    
    [System.Serializable]
    public abstract class GameService : ScriptableObject
    {
        public abstract void ConfigureService(GameInstance.GameInstance gameInstance);
    }
}
