using UnityEngine;

public class StageSelectButton : MonoBehaviour {
    public int StageID = -1;

    public void Click() {
        StageSelector.Instance.SelectStage(StageID);
    }
}