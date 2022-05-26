using UnityEngine;
using System.Diagnostics;

// TODO: need to observe the stage state
public abstract class StageStateReactBase : MonoBehaviour {
    protected bool IsActivated = false;
    protected bool IsPaused = false;
    public void GameStart() {
        IsActivated = true;
    }
    public void Pause() {
        IsPaused = true;
    }
    public void Continue() {
        IsPaused = false;
    }
    public void GameOver(bool isWin = true) {
        IsActivated = false;
        if (isWin) {
            GameWin();
        }
        else {
            GameLose();
        }
    }
    public virtual void EnemyDestroyed() {}
    public virtual void AllyCardDestroyed() {}
    protected virtual void GameLose() {}
    protected virtual void GameWin() {}
    public virtual void Restart() {}
}