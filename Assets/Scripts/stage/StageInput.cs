using UnityEngine;

// TODO: StageInput not complete, current using old version (in StageManager.cs)
public class StageInput : StageStateReactBase {
    #region Creator
    public static StageInput Create() {
        // create new GameObject to hold the StageInput
        GameObject go = new GameObject("StageInput");
        StageInput stageInput = go.AddComponent<StageInput>();
        return stageInput;
    }
    #endregion
}