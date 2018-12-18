using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EdgeColliderVertexSnapper))]
public class EdgeColliderVertexSnapperEditor : Editor {

  public override void OnInspectorGUI() {  
    DrawDefaultInspector();

    EdgeColliderVertexSnapper snapper = (EdgeColliderVertexSnapper)target;
    if (GUILayout.Button("Snap Points To Grid")) {
      snapper.SnapToGrid();
    }
    if (GUILayout.Button("Snap Points To Mesh")) {
      snapper.SnapToMesh();
    }
  }
}

[CustomEditor(typeof(PolyColliderVertexSnapper))]
public class PolyColliderVertexSnapperEditor : Editor {

  public override void OnInspectorGUI() {  
    DrawDefaultInspector();

    PolyColliderVertexSnapper snapper = (PolyColliderVertexSnapper)target;
    if (GUILayout.Button("Snap Points To Grid")) {
      snapper.SnapToGrid();
    }
    if (GUILayout.Button("Snap Points To Mesh")) {
      snapper.SnapToMesh();
    }
  }
}