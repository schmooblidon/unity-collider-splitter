using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Surface : MonoBehaviour {

  public Vector2 point1;
  public Vector2 point2;
  public float angle;
  public float scale;
  public SurfaceType surfaceType;
  public U_Surface leftSurf = null;
  public U_Surface rightSurf = null; 

}

public enum SurfaceType {
  Ground,
  Platform,
  WallL,
  WallR,
  Ceiling
}