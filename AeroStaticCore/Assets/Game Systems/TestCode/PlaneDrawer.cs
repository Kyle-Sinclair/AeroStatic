using System.Collections.Generic;
using UnityEngine;

namespace Game_Systems.TestCode {
    public class PlaneDrawer : MonoBehaviour {

        [Header("Seed Plane")] [SerializeField]
        private List<Vector3> CorePlanePoints = new List<Vector3>();

        [ContextMenu("Cascade From Plane")]
        public void CascadePoints() {
            
        }
        private void OnDrawGizmos() {
            
            for (int i = 0; i < CorePlanePoints.Count - 1; i++)
            {
                Gizmos.DrawLine(CorePlanePoints[i], CorePlanePoints[i + 1]);
            }
            //Gizmos.DrawLineList(CorePlanePoints.ToArray());
           // Gizmos.DrawCube(new Vector3(0,0,0),new Vector3(1,1,1) );
        }
    }
    
    
    
}
