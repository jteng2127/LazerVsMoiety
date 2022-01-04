using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGridTile : MonoBehaviour {
    #region Data

    public int CurrentAllyId { get; protected set; }
    public GameObject CurrentAlly { get; protected set; }

    #endregion
    #region Interface

    public GameObject SetAllyUnit(int id) {
        if (CurrentAllyId != -1) {
            CurrentAlly.GetComponent<AllyUnit>().DestroySelf();
        }
        CurrentAllyId = id;
        CurrentAlly = AllyUnit.Spawn(id, transform);
        return CurrentAlly;
    }

    #endregion
    #region MonoBehaviour

    void Awake() {
        CurrentAllyId = -1;
    }

    #endregion
}
