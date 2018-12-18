using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Ledge : MonoBehaviour {

  public Vector2 position;
  public LedgeType type;

}

public enum LedgeType {
  LedgeL,
  LedgeR
}