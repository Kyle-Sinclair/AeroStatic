using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "CoreMapCreator", menuName = "Services/Core Map Creator", order = 1)]
        
    public class MapCreator : GameService {
        
        [SerializeField] private Material[] _gridMaterials;
        [SerializeField] private Material[] _planeMaterials;
        // Start is called before the first frame update
        public void CreateMap() {
            GameObject BasePlane = Resources.Load<GameObject>("Art/Boards/BoardPrefabs/BaseQuad");
            GameObject BasePlaneRef =  MonoBehaviour.Instantiate(BasePlane);
            BasePlaneRef.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.Euler(90f,0f,0f));
            BasePlaneRef.transform.localScale = new Vector3(10,10,1);
        }

       
    }
}
