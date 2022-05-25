using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : SpawnHandlerBase {
    #region data
    AllyCardSpawnHandler AllyCardSpawnHandler;
    #endregion

    #region creator
    public static EnemySpawnHandler Create(
            List<int> enemyTypes,
            List<float> spawnPositionsY,
            AllyCardSpawnHandler allyCardSpawnHandler,
            float movingSpeedMultiplier = 1.0f) {
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
        SpawnNumberTotal = 10;
        SpawnInterval = 7.0f;
        SpawnIntervalDeviation = 0.3f;
        SpawnNumberCount = 0;
        SpawnCountDown = 0.0f;
    }
    #endregion

    #region method
    public bool IsEnemyAllSpawned() {
        return SpawnNumberCount >= SpawnNumberTotal;
    }
    #endregion

    #region MonoBehaviour
    void Update() {
        if (IsActivated && !IsPaused) {
            if (SpawnCountDown <= 0.0f) {
                if (SpawnNumberCount < SpawnNumberTotal) {
                    SpawnCountDown = GenerateRandomDelay(SpawnInterval, SpawnIntervalDeviation);
                    SpawnNumberCount++;
                    Spawner.Spawn();
                }
            }
            SpawnCountDown -= Time.deltaTime;
        }
    }
    #endregion
}