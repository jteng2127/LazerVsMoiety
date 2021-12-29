using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class SpawnManager : MonoBehaviour {

    #region Debug

    protected static void Log(string s) {
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType + ": " + s);
    }

    #endregion

    #region Singleton

    protected static SpawnManager s_Instance;

    public static SpawnManager Instance {
        get {
            if (s_Instance == null) {
                throw new NullReferenceException();
            }
            return s_Instance;
        }
    }

    static void CreateNewInstance() {
        Log("Create new instance");
        if (s_Instance) {
            Log("Destroy last instance");
            Destroy(s_Instance.gameObject);
        }
        GameObject go = new GameObject("SpawnManager", typeof(SpawnManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<SpawnManager>();
    }

    static void DestroyInstance(){
        if (s_Instance) {
            Log("Destroy instance");
            Destroy(s_Instance.gameObject);
        }
    }

    #endregion

    #region Data

    StageGrid _stageGrid;
    float _enemySpawnDelay;
    float _allyCardSpawnDelay;

    bool _isSpawning = false;

    #endregion

    #region Method

    void SpawnStart() {
        _isSpawning = true;
        _enemySpawnDelay = 0.0f;
        _allyCardSpawnDelay = 0.0f;
    }

    float GenerateRandomDelay(float interval, float deviation) {
        return UnityEngine.Random.Range(interval - deviation, interval + deviation);
    }

    /// <param name="type">0: Enemy, 1: AllyCard</param>
    void Spawn(int type){
        int spawnRow = UnityEngine.Random.Range(0, StageManager.Instance.Data.GridRowTotal);
        Debug.Log(
            "Spawn " + type + " at: " + 
            StageManager.Instance.Data.EnemySpawnPositionX + ", " +
            _stageGrid.RowYList[spawnRow]
        );
        // TODO: complete Unit class then back here to spawn
    }

    void CheckAndSpawn() {
        if (_enemySpawnDelay <= 0.0f) {
            _enemySpawnDelay = GenerateRandomDelay(
                StageManager.Instance.Data.EnemySpawnInterval,
                StageManager.Instance.Data.EnemySpawnIntervalDeviation
            );
            Spawn(0);
        }
        if (_allyCardSpawnDelay <= 0.0f) {
            _allyCardSpawnDelay = GenerateRandomDelay(
                StageManager.Instance.Data.AllyCardSpawnInterval,
                StageManager.Instance.Data.AllyCardSpawnIntervalDeviation
            );
            Spawn(1);
        }
    }

    #endregion

    #region Interface

    static public void StartNewSpawner() {
        CreateNewInstance();
        Instance.SpawnStart();
    }

    static public void EndSpawn(){
        DestroyInstance();
    }

    public void TriggerPause() {

    }

    #endregion

    #region MonoBehaviour

    void Start() {
        _stageGrid = GameObject.Find("StageGrid").GetComponent<StageGrid>();
    }

    void Update() {
        if (_isSpawning) {
            _enemySpawnDelay -= Time.deltaTime;
            _allyCardSpawnDelay -= Time.deltaTime;
            CheckAndSpawn();
        }
    }

    void OnEnable() {
        Log("Enable");
    }

    void OnDestroy() {
        Log("Destroy");
    }

    #endregion


    /* TODO: wait to check up
    double _startTime;

    [SerializeField]
    public List<int> enemy_and_ally_id_list; // = queryEnemyAndAllyIdList(5);

    // TODO: add JsonManager
    public List<int> queryEnemyAndAllyIdList(int level) {
        // string load_unit_data = File.ReadAllText("../jsons/StageUnit.json");
        // List<EnemyUnitData> data_list = JsonUtility.FromJson<List<EnemyUnitData>>(load_enemy_unit_data);
        // return data_list;
        return new List<int>();
    }

    // public Coefficient coefficient = new Coefficient(checkpoint_level);
    // public Tuple<int, int> getRemainQuantity(){
    //     return new Tuple<int, int>(coefficient._wave_quantity, coefficient._fg_quantity);
    // }
    */
}