using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSplitter : MonoBehaviour {

  public static string SurfacePrefabName = "Collision/Surface";
  public static string LedgePrefabName = "Collision/Ledge";

  protected virtual bool amPoly {get { return false; }}

  public bool clockwise = true;
  public bool platform = false;
  public bool autoLedge = true;

  public float minGroundAng = 315f;
  public float maxGroundAng = 45f;
  public float minCeilAng = 135f;
  public float maxCeilAng = 225f;
  public float minWallLAng = 45f;
  public float maxWallLAng = 135f;
  public float minWallRAng = 225f;
  public float maxWallRAng = 315f;

  protected U_Surface prevSurf = null;
  protected U_Surface firstSurf = null;

  protected Vector2[] verts;
  // verts in world space
  protected Vector3[] vertsW;


  protected void MakeSurface(int i) {
    vertsW[i] = transform.TransformPoint(verts[i]);
    int prev = i-1;
    if (clockwise) prev = i+1;
    if (prev < 0)  {
      if (amPoly) {
        prev = verts.Length-1;
        vertsW[prev] = transform.TransformPoint(verts[prev]);
      }
      else return;
    }
    if (prev > verts.Length-1) {
      if (amPoly) {
        prev = 0;
        vertsW[prev] = transform.TransformPoint(verts[prev]);
      }
      else return;
    }
    if (verts[i] == verts[prev]) return;

    Vector3 center = (vertsW[i] + vertsW[prev]) / 2f;
    center.z = 0;
    Vector3 diff = vertsW[prev] - vertsW[i];
    float length = Mathf.Sqrt(Mathf.Pow(diff.x, 2f) + Mathf.Pow(diff.y, 2f));
    float angle = Mathf.Atan2(diff.y, diff.x);
    GameObject surfObj = Instantiate((GameObject)Resources.Load(SurfacePrefabName), center, new Quaternion());
    surfObj.transform.localScale = new Vector3(length, 0.002f, 1f);
    surfObj.transform.localEulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
    U_Surface surf = surfObj.GetComponent<U_Surface>();
    surf.angle = angle;
    surf.point1 = vertsW[i];
    surf.point2 = vertsW[prev];
    surf.surfaceType = GetSurfaceType(Mathf.Rad2Deg * angle);

    if (prevSurf != null) {
      surf.rightSurf = prevSurf;
      prevSurf.leftSurf = surf;
    }
    if (firstSurf == null) {
      firstSurf = surf;
    }
    MakeSurfaceEnd(surf, i);
    prevSurf = surf;
    surf.transform.parent = transform;
  }

  protected virtual void MakeSurfaceEnd(U_Surface surf, int i) {

  }

  protected void MakeLedges() {
    for (int i=0;i<transform.childCount;i++) {
      GameObject child = transform.GetChild(i).gameObject;
      if (child != null) {
        U_Surface surf = child.GetComponent<U_Surface>();
        if (surf != null) {
          if (surf.surfaceType == SurfaceType.Ground) {
            MakeLedge(surf);
          }
        }
      }
    }
  }

  protected void MakeLedge(U_Surface surf) {
    if (surf.leftSurf != null) {
      if (surf.leftSurf.surfaceType == SurfaceType.WallL || (surf.leftSurf.surfaceType == SurfaceType.Ceiling && surf.leftSurf.point1.y < surf.point2.y)) {
        MakeLedgeObj(surf.point1, LedgeType.LedgeL);
      }
    }
    if (surf.rightSurf != null) {
      if (surf.rightSurf.surfaceType == SurfaceType.WallR || (surf.rightSurf.surfaceType == SurfaceType.Ceiling && surf.rightSurf.point2.y < surf.point1.y)) {
        MakeLedgeObj(surf.point2, LedgeType.LedgeR);
      }
    }
  }

  protected void MakeLedgeObj(Vector2 position, LedgeType type) {
    GameObject ledgeObj = Instantiate((GameObject)Resources.Load(LedgePrefabName), position, new Quaternion());
    U_Ledge ledge = ledgeObj.GetComponent<U_Ledge>();
    ledge.position = position;
    ledge.type = type;
    ledge.transform.parent = transform;
  }

  protected SurfaceType GetSurfaceType(float ang) {
    if (ang < 0) ang += 360f;
    if (ang >= minGroundAng || ang <= maxGroundAng) return platform ? SurfaceType.Platform : SurfaceType.Ground;
    if (ang >= minCeilAng && ang <= maxCeilAng) return SurfaceType.Ceiling;
    if (ang >= minWallLAng && ang <= maxWallLAng) return SurfaceType.WallL;
    if (ang >= minWallRAng && ang <= maxWallRAng) return SurfaceType.WallR;
    return SurfaceType.Ground;
  }

}