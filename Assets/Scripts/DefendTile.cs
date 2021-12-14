using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendTile : MonoBehaviour {
  public GameObject unitPrefab;
  private int unitId = 0;
  private GameObject unitObject;

  void Start() {

  }

  void Update() {

  }

  public void set_defend_unit(int _id) {
    if (unitId == 0) {
      unitId = _id;
      unitObject = Instantiate(unitPrefab,
                                transform.position,
                                Quaternion.identity);
    }
    else remove_unit();
  }

  public void remove_unit() {
    if (unitId != 0) {
      unitId = 0;
      Destroy(unitObject);
    }
  }
}
