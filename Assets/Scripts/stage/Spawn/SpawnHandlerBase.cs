using UnityEngine;

public abstract class SpawnHandlerBase : StageStateReactBase {

    private static readonly int DefaultAllyCardSpawnNumberMax = 8; // TODO: move to somewhere else

    #region Data
    protected int SpawnNumberTotal;
    protected float SpawnInterval;
    protected float SpawnIntervalDeviation;
    protected int SpawnNumberCount;
    protected float SpawnCountDown;
    protected ISpawner Spawner;
    #endregion
    
    protected int EnemyCount { get; set; } // TODO: move to GameOverHandler

    #region method
    protected float GenerateRandomDelay(float interval, float deviation) {
        return UnityEngine.Random.Range(interval - deviation, interval + deviation);
    }
    #endregion

    #region StageStateReactBase
    public override void GameLose() {}
    public override void GameWin() {}
    #endregion
}