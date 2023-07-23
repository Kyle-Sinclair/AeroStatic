using UnityEngine;

namespace Game_Systems.Services {
    public class MapCreator : IGameService {
        
        [SerializeField] private Material[] _gridMaterials;
        [SerializeField] private Material[] _planeMaterials;
        // Start is called before the first frame update
        public void CreateMap() {
            GameObject BasePlane = Resources.Load<GameObject>("Art/Boards/BoardPrefabs/BaseQuad");
            MonoBehaviour.Instantiate(BasePlane);
            BasePlane.transform.localScale = new Vector3(10,10,1);
        }
        
    }
}
