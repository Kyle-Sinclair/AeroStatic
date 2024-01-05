using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.HalfEdgeStructure;

public class HalfEdgeTestGenerator : MonoBehaviour {
    
    public HalfEdgeDataStructure HEDS = new HalfEdgeDataStructure();
    // Start is called before the first frame update

    [ContextMenu("Generate Half Edge Structure")]
    // Update is called once per frame
    void GenerateHalfEdge() {
        HEDS.Clear();

        GameObject island = new GameObject("Tested Island");

        island.transform.position = new Vector3(0,0,0);
        Mesh islandMesh = new Mesh();
        island.AddComponent<MeshFilter>().mesh = islandMesh;
        island.AddComponent<MeshRenderer>().material = default;

        List<Vector3> VectorsToAdd = new List<Vector3>();
        
        VectorsToAdd.Add(new Vector3(0,0,0));
        VectorsToAdd.Add(new Vector3(1,0,0));
        VectorsToAdd.Add(new Vector3(0,1,0));
        HEDS.AddTriangle(VectorsToAdd);
       

    }
}
