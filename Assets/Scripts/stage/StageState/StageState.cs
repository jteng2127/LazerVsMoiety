using UnityEngine;

public abstract class StageState {

    protected StageManager StageManager;
    public virtual void GameReady() {}
    public virtual void GameStart() {}
    public virtual void Pause() {}
    public virtual void Continue() {}
    public virtual void GameLose() {}
    public virtual void GameWin() {}
    public virtual void Restart() {}
    public void UnitDestroyed(GameObject go) {
        if (go.TryGetComponent<EnemyUnit>(out EnemyUnit enemyUnit)) {
            EnemyDestroyed();
        }
        if (go.TryGetComponent<AllyCard>(out AllyCard allyCard)) {
            AllyCardDestroyed();
        }
    }
    public virtual void EnemyDestroyed() {}
    public virtual void AllyCardDestroyed() {}
}