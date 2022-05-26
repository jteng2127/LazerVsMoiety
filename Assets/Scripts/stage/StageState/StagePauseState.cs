public class StagePauseState : StageState {
    public StagePauseState(StageManager stageManager) {
        StageManager = stageManager;
    }

    public override void Continue() {
        StageManager.changeState(new StagePlayState(StageManager));
        foreach (var react in StageManager.ReactList) {
            react.Continue();
        }
    }
}