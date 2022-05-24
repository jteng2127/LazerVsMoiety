using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class SpawnManager : MonoSingleton<SpawnManager> {
    
    #region Data

    private StageManager.StageData _data;
    private StageGrid _stageGrid;

    private bool _isSpawning = false;

    #endregion

    #region private method

    private void Initial() {
        _data = StageManager.Instance.Data;
        _isSpawning = false;
    }

    private float GenerateRandomDelay(float interval, float deviation) {
        return UnityEngine.Random.Range(interval - deviation, interval + deviation);
    }

    /// <param name="type">0: Enemy, 1: AllyCard</param>
    private void SpawnUnit(int type) {
        Vector3 spawnPosition;
        int id;

        List<int> enemyType = StageManager.Instance.Data.EnemyType;
        int randomIndex = UnityEngine.Random.Range(0, enemyType.Count);
        id = enemyType[randomIndex];

        if (type == 0) {
            int spawnRow = UnityEngine.Random.Range(0, StageManager.Instance.Data.GridRowTotal);
            spawnPosition = new Vector3(
                StageManager.Instance.Data.EnemySpawnPositionX,
                _stageGrid.RowYList[spawnRow],
                0.0f
            );
            EnemyUnit.Spawn(id, spawnPosition);
        }

        if (type == 1) {
            spawnPosition = new Vector3(
                StageManager.Instance.Data.AllyCardSpawnPositionX,
                StageManager.Instance.Data.AllyCardSpawnPositionY,
                0.0f
            );
            AllyCard.Spawn(id, spawnPosition);
        }
    }

    private void CheckAndSpawn() {
        if (_data.EnemySpawnTimeLeft <= 0.0f &&
            _data.EnemySpawnNumberLeft > 0) {
            _data.EnemySpawnTimeLeft = GenerateRandomDelay(
                StageManager.Instance.Data.EnemySpawnInterval,
                StageManager.Instance.Data.EnemySpawnIntervalDeviation
            );
            SpawnUnit(0);
            _data.EnemySpawnNumberLeft--;
            _data.EnemyCount++;
        }
        if (_data.AllyCardSpawnTimeLeft <= 0.0f &&
            _data.AllyCardCount < _data.AllyCardSpawnNumberMax) {
            _data.AllyCardSpawnTimeLeft = GenerateRandomDelay(
                StageManager.Instance.Data.AllyCardSpawnInterval,
                StageManager.Instance.Data.AllyCardSpawnIntervalDeviation
            );
            SpawnUnit(1);
            _data.AllyCardCount++;
        }
    }

    #endregion

    #region MonoBehaviour

    void Start() {
    }

    void Update() {
        if (_data.GameState == 1) {
            _data.EnemySpawnTimeLeft -= Time.deltaTime;
            _data.AllyCardSpawnTimeLeft -= Time.deltaTime;
            CheckAndSpawn();
        }
    }

    #endregion

    #region Interface

    static public void CreateNewSpawner() {
        DestroyInstance();
        Instance.Initial();
    }

    static public void StartSpawn() {
        if (Instance == null) CreateNewSpawner();
        Instance._isSpawning = true;
        Instance._stageGrid = GameObject.Find("StageGrid").GetComponent<StageGrid>();
    }

    static public void DestroyUnit(GameObject go) {
        if (go.TryGetComponent<EnemyUnit>(out EnemyUnit enemyUnit)) {
            Instance._data.EnemyCount--;
            AudioManager.Instance.Play("kill", 0.2f);
            StageManager.CheckGameOver();
        }
        if (go.TryGetComponent<AllyCard>(out AllyCard allyCard)) {
            Instance._data.AllyCardCount--;
        }
        Destroy(go);
    }

    public void TriggerPause() {

    }

    #endregion
}