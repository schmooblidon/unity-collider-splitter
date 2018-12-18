using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EdgeColliderSplitter))]
public class EdgeColliderSplitterEditor : Editor {

  public override void OnInspectorGUI() {  
    DrawDefaultInspector();

    EdgeColliderSplitter splitter = (EdgeColliderSplitter)target;
    if (GUILayout.Button("Create Surfaces")) {
      splitter.Create();
    }
  }
}

[CustomEditor(typeof(PolyColliderSplitter))]
public class PolyColliderSplitterEditor : Editor {

  public override void OnInspectorGUI() {  
    DrawDefaultInspector();

    PolyColliderSplitter splitter = (PolyColliderSplitter)target;
    if (GUILayout.Button("Create Surfaces")) {
      splitter.Create();
    }
  }
}