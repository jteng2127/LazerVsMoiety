using UnityEngine;

public abstract class SpawnHandlerBase : StageStateReactBase {

    private static readonly int DefaultAllyCardSpawnNumberMax = 8; // TODO: move to somewhere else

    #region Data
    public int SpawnNumberTotal { get; protected set; }
    public float SpawnInterval { get; protected set; }
    public float SpawnIntervalDeviation { get; protected set; }
    public int SpawnNumberCount { get; protected set; }
    public float SpawnCountDown { get; protected set; }
    public float MovingSpeedMultiplier { get; protected set; }
    protected ISpawner Spawner;
    #endregion
    
    protected int EnemyCount { get; set; } // TODO: move to GameOverHandler

    #region method
    protected float GenerateRandomDelay(float interval, float deviation) {
        return UnityEngine.Random.Range(interval - deviation, interval + deviation);
    }
    #endregion
}