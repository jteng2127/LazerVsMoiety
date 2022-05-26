using UnityEngine.SceneManagement;

public class StageReadyState : StageState {

    public StageReadyState(StageManager stageManager) {
        StageManager = stageManager;
    }

    public override void GameStart() {
        StageManager.changeState(new StagePlayState(StageManager));
        foreach (var react in StageManager.ReactList) {
            react.GameStart();
        }
    }
}