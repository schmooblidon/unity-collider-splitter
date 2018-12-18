using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyColliderSplitter : ColliderSplitter {

  public PolygonCollider2D poly;

  protected override bool amPoly {get { return true; }}

  public void Create() {
    poly = GetComponent<PolygonCollider2D>();
    if (poly != null) {
      verts = poly.points;
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

  protected override void MakeSurfaceEnd(U_Surface surf, int i) {
    if (!clockwise && i == verts.Length-1) {
      surf.leftSurf = firstSurf;
      firstSurf.rightSurf = surf;
    }
    else if (clockwise && i == 0) {
      surf.leftSurf = firstSurf;
      firstSurf.rightSurf = surf;
    }
  }

}