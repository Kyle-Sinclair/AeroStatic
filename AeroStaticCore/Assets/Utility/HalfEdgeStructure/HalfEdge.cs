using Unity.Mathematics;

namespace Utility.HalfEdgeStructure {
    [System.Serializable]

    public struct HalfEdge {
        public int NextIndex;
        public int PairIndex;
        public int FaceIndex;
        public int StartVertexIndex;


        public HalfEdge(int faceIndex) {
            FaceIndex = faceIndex;
            NextIndex = -1;
            PairIndex = -1;
            StartVertexIndex = -1;
        } 
        public HalfEdge(int faceIndex, int startVertexIndex, int nextIndex, int pairIndex  ) {
            FaceIndex = faceIndex;
            NextIndex = -1;
            PairIndex = -1;
            StartVertexIndex = -1;
        }

        public static bool operator ==(HalfEdge first, HalfEdge second) {
            return true;
        }

        public static bool operator !=(HalfEdge first, HalfEdge second) {
            return !(first == second);
        }
    }
    
}
