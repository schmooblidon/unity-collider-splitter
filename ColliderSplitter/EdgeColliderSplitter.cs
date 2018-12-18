using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderSplitter : ColliderSplitter {

  public EdgeCollider2D edge;

  public void Create() {
    edge = GetComponent<EdgeCollider2D>();
    if (edge != null) {
      verts = edge.points;
      vertsW = new Vector3[verts.Length];
      if (clockwise) {
        for (int i=verts.Length-1;i>=0;i--) {
          MakeSurface(i);
        }
      }
      else {
        for (int i=0;i<verts.Length;i++) {
          MakeSurface(i);
        }
      }
    }

    if (autoLedge) {
      MakeLedges();
    }

  }

}