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
        Clear();
        CurrentAllyId = id;
        CurrentAlly = AllyUnit.Spawn(id, transform);
        return CurrentAlly;
    }

    public void Clear(bool isInit = false){
        if(!isInit && CurrentAlly != null) Destroy(CurrentAlly);
        CurrentAllyId = -1;
        CurrentAlly = null;
    }

    #endregion
    #region MonoBehaviour

    void Awake() {
        Clear(true);
    }

    #endregion
}
