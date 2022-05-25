using System.Collections.Generic;
using UnityEngine;

public class AllyCardSpawnHandler : SpawnHandlerBase {
    #region data
    private int SpawnSpaceLeft;
    #endregion

    #region creator
    public static AllyCardSpawnHandler Create(
            List<int> allyCardTypes) {
        // create new GameObject to hold the AllyCardSpawnHandler
        GameObject go = new GameObject("AllyCardSpawnHandler");
        AllyCardSpawnHandler allyCardSpawnHandler = go.AddComponent<AllyCardSpawnHandler>();
        allyCardSpawnHandler.Initial(allyCardTypes);
        return allyCardSpawnHandler;
    }
    private void Initial(List<int> allyCardTypes) {
        Spawner = new AllyCardSpawner(allyCardTypes);
        SpawnNumberTotal = int.MaxValue;
        SpawnInterval = 4.5f;
        SpawnIntervalDeviation = 0.3f;
        SpawnNumberCount = 0;
        SpawnCountDown = 0.0f;
        SpawnSpaceLeft = 8;
    }
    #endregion

    #region method
    public void FreeSpawnSpace(int space = 1) {
        SpawnSpaceLeft += space;
    }
    #endregion

    #region MonoBehaviour
    void Update() {
        if (IsActivated && !IsPaused) {
            if (SpawnCountDown <= 0.0f) {
                if (SpawnSpaceLeft > 0) {
                    SpawnCountDown = GenerateRandomDelay(SpawnInterval, SpawnIntervalDeviation);
                    SpawnNumberCount++;
                    Spawner.Spawn();
                    SpawnSpaceLeft--;
                }
            }
            SpawnCountDown -= Time.deltaTime;
        }
    }
    #endregion
}