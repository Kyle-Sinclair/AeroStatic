

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Utility.HalfEdgeStructure {
    
    [System.Serializable]

    // ReSharper disable once InconsistentNaming
    public class HEFace {
        public int HalfEdgeIndex;
        public List<int> VertexIndices = new List<int>(3);
        public HEFace(){
           
        }
        public HEFace(List<Vector3> VertexList){
            foreach(Vector3 vector in VertexList) {
                
            }
        }

        public void AddVertex(int vertexIndex) {
            VertexIndices.Add(vertexIndex); 
        }
    }
  
    
}
