using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolyColliderVertexSnapper : ColliderVertexSnapper {

public PolygonCollider2D poly;

  void Start(){
    if (poly == null) {
      poly = GetComponent<PolygonCollider2D>();
    }
    UpdateMesh();
  }

  public override void SnapToGrid() {
    int counter = 0;
    for (int pathIndex = 0; pathIndex < poly.pathCount; pathIndex++) {
      Vector2[] path = poly.GetPath(pathIndex);
      for (long pointIndex = 0; pointIndex < path.Length; pointIndex++) {
        counter++;
        Vector2 point = path[pointIndex];
        path[pointIndex] = SnapPointToGrid(point);
      }
      poly.points = path;
      poly.SetPath(pathIndex, path);
    }
    PrintCount(counter);
  }

  public override void SnapToMesh() {
    UpdateMesh();
    if (mesh == null) return;
    int counter = 0;
    for (int pathIndex = 0; pathIndex < poly.pathCount; pathIndex++) {
      Vector2[] path = poly.GetPath(pathIndex);
      for (long pointIndex = 0; pointIndex < path.Length; pointIndex++) {
        counter++;
        Vector3 point = path[pointIndex];
        point = SnapPointToMesh(point);
        path[pointIndex] = new Vector2(point.x, point.y);
      }
      poly.points = path;
      poly.SetPath(pathIndex, path);
    }
    PrintCount(counter);
  }
}