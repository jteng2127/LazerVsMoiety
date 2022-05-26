public class StagePlayState : StageState {
    public StagePlayState(StageManager stageManager) {
        StageManager = stageManager;
    }

    public override void EnemyDestroyed() {
        foreach (var react in StageManager.ReactList) {
            react.EnemyDestroyed();
        }
    }

    public override void AllyCardDestroyed() {
        foreach (var react in StageManager.ReactList) {
            react.AllyCardDestroyed();
        }
    }

    public override void Pause() {
        StageManager.changeState(new StagePauseState(StageManager));
        foreach (var react in StageManager.ReactList) {
            react.Pause();
        }
    }

    public override void GameWin() {
        StageManager.changeState(new StageOverState(StageManager));
        foreach (var react in StageManager.ReactList) {
            react.GameOver(isWin: true);
        }
    }

    public override void GameLose() {
        StageManager.changeState(new StageOverState(StageManager));
        foreach (var react in StageManager.ReactList) {
            react.GameOver(isWin: false);
        }
    }
}