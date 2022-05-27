using UnityEngine;

public class StageSelectButton : MonoBehaviour {
    public int StageID = -1;

    public void Click() {
        Debug.Log("[StageSelectButton] Clicked");
        StageSelector.Instance.SelectStage(StageID);
    }
}