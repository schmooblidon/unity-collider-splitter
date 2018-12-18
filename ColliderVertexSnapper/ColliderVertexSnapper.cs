using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderVertexSnapper : MonoBehaviour {

  public GameObject[] meshObjs;
  protected GameObject[] meshObjsFull;
  protected Mesh[] mesh;
  public float gridSize = 0.1f;
  public int objLimit = 500;

  public virtual void SnapToGrid() {

  }

  public virtual void SnapToMesh() {

  }

  protected void FillMeshObjs() {
    meshObjsFull = new GameObject[objLimit];
    int n = 0;
    if (meshObjs != null) {
      for (int i=0;i<meshObjs.Length;i++) {
        if (meshObjs[i] != null) {
          Transform[] trans = meshObjs[i].GetComponentsInChildren<Transform>();
          for (int j=0;j<trans.Length;j++) {
            meshObjsFull[n] = trans[j].gameObject;
            n++;
            if (n >= objLimit) return;
          }
        }
      }
    }
    for (int m=n;m<objLimit;m++) {
      meshObjsFull[m] = null;
    }
  }

  protected void UpdateMesh() {
    FillMeshObjs();
    if (meshObjsFull != null) {
      mesh = new Mesh[meshObjsFull.Length];
      for (int i=0;i<meshObjsFull.Length;i++) {
        if (meshObjsFull[i] != null) {
          MeshFilter meshF = meshObjsFull[i].GetComponent<MeshFilter>();
          if (meshF != null) {
            mesh[i] = meshF.sharedMesh;
          }
        }
        else {
          mesh[i] = null;
        }
      }
    }
  }

  protected Vector2 SnapPointToGrid(Vector2 point) {
    return new Vector2(
      Mathf.Round(point.x / gridSize) * gridSize,
      Mathf.Round(point.y / gridSize) * gridSize);
  }

  protected Vector3 SnapPointToMesh(Vector2 point) {
    // find nearest vertex
    float closest = 1000000f;
    point = transform.TransformPoint(point);
    Vector3 closestPoint = point;
    for (int n=0;n<mesh.Length;n++) {
      if (mesh[n] == null) continue;
      for (int i=0;i<mesh[n].vertices.Length;i++) {
        Vector3 vt = meshObjsFull[n].transform.TransformPoint(mesh[n].vertices[i]);
        if (Vector3.Distance(point, vt) < closest) {
          closest = Vector3.Distance(point, vt);
          closestPoint = vt;
        }
      }
    }
    return transform.InverseTransformPoint(closestPoint);
  }

  protected void PrintCount(int count) {
    print("Snapped " + count + " points.");
  }
}