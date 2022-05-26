using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : SpawnHandlerBase {
    #region data
    public List<int> EnemyTypes { get; protected set; }
    private AllyCardSpawnHandler AllyCardSpawnHandler;
    private int EnemyDeadCount;
    #endregion

    #region creator
    public static EnemySpawnHandler Create(
            AllyCardSpawnHandler allyCardSpawnHandler,
            List<float> spawnPositionsY,
            List<int> enemyTypes = null,
            float movingSpeedMultiplier = 1.0f) {
        if (enemyTypes == null) enemyTypes = new List<int>() {1, 2, 3};
        // create new GameObject to hold the EnemySpawnHandler
        GameObject go = new GameObject("EnemySpawnHandler");
        EnemySpawnHandler enemySpawnHandler = go.AddComponent<EnemySpawnHandler>();
        enemySpawnHandler.Initial(enemyTypes, spawnPositionsY, allyCardSpawnHandler, movingSpeedMultiplier);
        return enemySpawnHandler;
    }
    private void Initial(
            List<int> enemyTypes,
            List<float> spawnPositionsY,
            AllyCardSpawnHandler allyCardSpawnHandler,
            float movingSpeedMultiplier = 1.0f) {
        Spawner = new EnemySpawner(enemyTypes, spawnPositionsY, movingSpeedMultiplier);
        EnemyTypes = enemyTypes;
        AllyCardSpawnHandler = allyCardSpawnHandler;

        SpawnNumberTotal = 10;
        SpawnInterval = 7.0f;
        SpawnIntervalDeviation = 0.3f;
        SpawnNumberCount = 0;
        SpawnCountDown = 0.0f;
        EnemyDeadCount = 0;
    }
    #endregion

    #region setter
    public EnemySpawnHandler SetSpawnNumberTotal(int spawnNumberTotal) {
        SpawnNumberTotal = spawnNumberTotal;
        return this;
    }
    public EnemySpawnHandler SetSpawnInterval(float spawnInterval) {
        SpawnInterval = spawnInterval;
        return this;
    }
    public EnemySpawnHandler SetSpawnIntervalDeviation(float spawnIntervalDeviation) {
        SpawnIntervalDeviation = spawnIntervalDeviation;
        return this;
    }
    #endregion

    #region method
    public bool IsEnemyAllDead() {
        bool isAllDead = EnemyDeadCount >= SpawnNumberTotal;
        return isAllDead;
    }
    #endregion

    #region StageStateReactBase
    public override void EnemyDestroyed() {
        EnemyDeadCount++;
    }
    #endregion

    #region MonoBehaviour
    void Update() {
        if (IsActivated && !IsPaused) {
            if (SpawnCountDown <= 0.0f) {
                if (SpawnNumberCount < SpawnNumberTotal) {
                    SpawnCountDown = GenerateRandomDelay(SpawnInterval, SpawnIntervalDeviation);
                    SpawnNumberCount++;
                    int spawnID = Spawner.Spawn();
                    AllyCardSpawnHandler.SetPrioritySpawn(spawnID);
                }
            }
            SpawnCountDown -= Time.deltaTime;
        }
    }
    #endregion
}