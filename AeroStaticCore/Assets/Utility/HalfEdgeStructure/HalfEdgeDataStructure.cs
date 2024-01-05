using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Utility.HalfEdgeStructure {
    
    [System.Serializable]

    public class HalfEdgeDataStructure {
        
        public List<HEFace> Faces = new List<HEFace>();
        public List<HalfEdge> HalfEdges = new List<HalfEdge>();
        public List<HEVertex> Vertices = new List<HEVertex>();
        
        public HalfEdgeDataStructure() {
         
        }


        public void AddTriangle(List<Vector3> VerticestoAdd) {
            HEFace newFace = new HEFace();
            Faces.Add(newFace);
            List<HEVertex> newVertices = new List<HEVertex>();
            List<HalfEdge> newHalfEdges = new List<HalfEdge>();
            //Create the vertices for the new face
            for (int i = 0; i < VerticestoAdd.Count; i++) {
                HEVertex newVertex = new HEVertex(VerticestoAdd[i], HalfEdges.Count + i * 2);
                newVertices.Add(newVertex);
            }
            //Add halfedges as pairs. Even half edges are clockwise, odd are anti clockwise
            for (int i = 0; i < 3;i++) {
                HalfEdge clockwise = new HalfEdge(,)
                var HalfEdge = HalfEdges[i];
                HalfEdge.NextIndex = i + 1  ;

            }
            //Add anti clockwise half edges
            Vertices.AddRange(newVertices);

        }
        public void AddQuad(List<Vector3> VerticestoAdd) {
            HEFace newFace = new HEFace();
            Faces.Add(newFace);
            
            for (int i = 0; i < VerticestoAdd.Count; i++) {
                HEVertex newVertex = new HEVertex(VerticestoAdd[i]);
                Vertices.Add(newVertex);
                newFace.AddVertex(Vertices.Count - 1);
                HalfEdge newHalfEdge = new HalfEdge(Faces.Count - 1);
            }

            for (int i = 0; i < 2 * VerticestoAdd.Count; i++) {
                HalfEdge outgoingHalfEdge = new HalfEdge(Faces.Count - 1);
            }
            //Add these vertices to vertex list
            //Create face from these vertices
            //Create Half Edges for this vertex
        }

        void CascadeQuadFromExistingPoints(List<int> AnchorVertices) {
            //Create face 
        }

       public  void Clear() {
            Faces.Clear();
            HalfEdges.Clear();
            Vertices.Clear();
        }
        
    }
}
