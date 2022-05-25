using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner {

    private List<Vector3> SpawnPositions;
    private int SpawnPositionsTotal;
    private List<int> EnemyTypes;
    private int EnemyTypesTotal;

    public EnemySpawner(List<Vector3> spawnPositions, List<int> enemyTypes) {
        SpawnPositions = spawnPositions;
        SpawnPositionsTotal = spawnPositions.Count;
        EnemyTypes = enemyTypes;
        EnemyTypesTotal = enemyTypes.Count;
    }

    public void Spawn() {
        int enemyID = EnemyTypes[UnityEngine.Random.Range(0, EnemyTypesTotal)];
        Vector3 spawnPosition = SpawnPositions[UnityEngine.Random.Range(0, SpawnPositionsTotal)];
        EnemyUnit.Spawn(enemyID, spawnPosition);
    }
}