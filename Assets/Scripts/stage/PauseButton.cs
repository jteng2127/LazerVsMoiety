using UnityEngine;

public class PauseButton : MonoBehaviour {
    private GameObject StagePausingScreen;
    void Awake() {
        StagePausingScreen = GameObject.Find("PauseCanvas").transform
            .Find("StagePausingScreen").gameObject;
    }
    public void Click() {
        if (StageManager.Instance.StageState.GetType() == typeof(StagePlayState)) {
            StagePausingScreen.SetActive(true);
            StageManager.Instance.TriggerPause();
        }
        else if (StageManager.Instance.StageState.GetType() == typeof(StagePauseState)) {
            StagePausingScreen.SetActive(false);
            StageManager.Instance.TriggerContinue();
        }
    }
}