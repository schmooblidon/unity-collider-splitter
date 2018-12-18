using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeColliderVertexSnapper : ColliderVertexSnapper {

  public EdgeCollider2D edge;

  void Start(){
    if (edge == null) {
      edge = GetComponent<EdgeCollider2D>();
    }
    UpdateMesh();
  }

  public override void SnapToGrid() {
    int counter = 0;
    Vector2[] path = edge.points;
    for (long pointIndex = 0; pointIndex < edge.pointCount; pointIndex++) {
      counter++;
      Vector2 point = path[pointIndex];
      path[pointIndex] = SnapPointToGrid(point);
    }
    edge.points = path;
    PrintCount(counter);
  }

  public override void SnapToMesh() {
    UpdateMesh();
    if (mesh == null) return;
    int counter = 0;
    Vector2[] path = edge.points;
    for (long pointIndex = 0; pointIndex < edge.pointCount; pointIndex++) {
      counter++;
      Vector3 point = path[pointIndex];
      point = SnapPointToMesh(point);
      path[pointIndex] = new Vector2(point.x, point.y);
    }
    edge.points = path;
    PrintCount(counter);
  }

}