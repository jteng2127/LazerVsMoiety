using UnityEngine.SceneManagement;

public class StageInitialState : StageState {

    public StageInitialState(StageManager stageManager) {
        StageManager = stageManager;
    }

    public override void GameReady() {
        StageManager.changeState(new StageReadyState(StageManager));
        SceneManager.sceneLoaded += StageManager.Instance.OnStageSceneLoaded;
        GameManager.Instance.LoadScene(SceneType.Stage);
    }
}