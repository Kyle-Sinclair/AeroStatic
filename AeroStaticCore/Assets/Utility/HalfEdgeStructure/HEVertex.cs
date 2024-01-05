using UnityEngine;

namespace Utility.HalfEdgeStructure {
    [System.Serializable]

    public struct HEVertex {
        public float X;
        public float Y;
        public float Z;
        public int HalfEdgeIndex;
        public HEVertex(Vector3 point) {
            X = point.x;
            Y = point.y;
            Z = point.z;
            HalfEdgeIndex = -1;
        }
        public HEVertex(Vector3 point, int halfEdgeIndex) {
            X = point.x;
            Y = point.y;
            Z = point.z;
            HalfEdgeIndex = halfEdgeIndex;
        }
    }
    
   
}
