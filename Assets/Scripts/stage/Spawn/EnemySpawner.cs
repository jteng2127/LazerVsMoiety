using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ISpawner {

    #region default data
    private const float DefSpawnPositionX = 12.0f;
    private const float DefMovingSpeed = 0.5f;
    #endregion

    #region data
    private List<Vector3> SpawnPositions;
    private int SpawnPositionsTotal;
    private List<int> EnemyTypes;
    private int EnemyTypesTotal;
    private float MovingSpeed;
    private EntityEventHandler EntityEventHandler;
    #endregion

    #region constructor
    public EnemySpawner(
            List<int> enemyTypes,
            List<Vector3> spawnPositions,
            float movingSpeedMultiplier = 1.0f) {
        SpawnPositions = spawnPositions;
        SpawnPositionsTotal = spawnPositions.Count;
        EnemyTypes = enemyTypes;
        EnemyTypesTotal = enemyTypes.Count;
        MovingSpeed = DefMovingSpeed * movingSpeedMultiplier;
    }
    public EnemySpawner(
            List<int> enemyTypes,
            List<float> spawnPositionsY,
            float movingSpeedMultiplier = 1.0f) {
        SpawnPositions = new List<Vector3>();
        SpawnPositionsTotal = spawnPositionsY.Count;
        for (int i = 0; i < SpawnPositionsTotal; i++) {
            SpawnPositions.Add(new Vector3(DefSpawnPositionX, spawnPositionsY[i], 0.0f));
        }
        
        EnemyTypes = enemyTypes;
        EnemyTypesTotal = enemyTypes.Count;
        MovingSpeed = DefMovingSpeed * movingSpeedMultiplier;
    }
    #endregion

    #region public method

    public int Spawn() {
        int enemyID = EnemyTypes[UnityEngine.Random.Range(0, EnemyTypesTotal)];
        Vector3 spawnPosition = SpawnPositions[UnityEngine.Random.Range(0, SpawnPositionsTotal)];
        GameObject enemy = EnemyUnit.Spawn(enemyID, spawnPosition, MovingSpeed);
        StageManager.Instance.RegisterStageStateReact(enemy.GetComponent<EnemyUnit>());
        return enemyID;
    }
    #endregion
}