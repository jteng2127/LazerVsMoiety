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

    static SpawnManager CreateNewInstance() {
        Log("Create");
        if (s_Instance) {
            Log("Destroy last instance");
            Destroy(s_Instance.gameObject);
        }
        GameObject go = new GameObject("SpawnManager", typeof(SpawnManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<SpawnManager>();
        return s_Instance;
    }

    #endregion

    #region Data

    float _enemySpawnInterval;
    float _enemySpawnIntervalDeviation;
    float _allyCardSpawnInterval;
    float _allyCardSpawnIntervalDeviation;

    float _enemySpawnDelay;
    float _allyCardSpawnDelay;

    bool _isSpawning = false;

    #endregion

    #region Method

    void InitialSpawner(float enemySpawnInterval,
                        float enemySpawnIntervalDeviation,
                        float allyCardSpawnInterval,
                        float allyCardSpawnIntervalDeviation) {
        _enemySpawnInterval = enemySpawnInterval;
        _enemySpawnIntervalDeviation = enemySpawnIntervalDeviation;
        _allyCardSpawnInterval = allyCardSpawnInterval;
        _allyCardSpawnIntervalDeviation = allyCardSpawnIntervalDeviation;
        _isSpawning = false;
    }

    void SpawnStart(){
        _isSpawning = true;
        _enemySpawnDelay = 0.0f;
        _allyCardSpawnDelay = 0.0f;
    }

    #endregion

    #region Interface

    static public void StartNewSpawner( float enemySpawnInterval,
                                        float enemySpawnIntervalDeviation,
                                        float allyCardSpawnInterval,
                                        float allyCardSpawnIntervalDeviation) {
        CreateNewInstance().InitialSpawner(
            enemySpawnInterval,
            enemySpawnIntervalDeviation,
            allyCardSpawnInterval,
            allyCardSpawnIntervalDeviation
        );
        Instance.SpawnStart();
    }

    #endregion

    #region Monobehaviour

    void Update(){
        if(_isSpawning){
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