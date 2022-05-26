using UnityEngine.SceneManagement;

public class StageOverState : StageState {
    public StageOverState(StageManager stageManager) {
        StageManager = stageManager;
    }

    public override void Restart() {
        foreach (var react in StageManager.ReactList) {
            react.Restart();
        }
        SceneManager.sceneLoaded -= StageManager.Instance.OnStageSceneLoaded;
        GameManager.Instance.LoadScene(SceneType.StageAdjust);
    }
}