using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class StageManager : MonoBehaviour {

    #region Debug

    protected static void Log(string s) {
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType + ": " + s);
    }

    #endregion

    #region Singleton

    protected static StageManager s_Instance;

    public static StageManager Instance {
        get {
            if (s_Instance == null) {
                throw new NullReferenceException();
            }
            return s_Instance;
        }
    }

    static StageManager CreateNewInstance() {
        Log("Create");
        if (s_Instance) {
            Log("Destroy last instance");
            Destroy(s_Instance.gameObject);
        }
        GameObject go = new GameObject("StageManager", typeof(StageManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<StageManager>();
        return s_Instance;
    }

    #endregion

    #region Data

    private class StageData {
        public string StageName;
        public string StageDescription;

        public List<int> EnemyType;
        public int EnemyQuantity;
        public float EnemySpeedMultiplier; // TODO: add a default speed, change to EnemySpeed = defalut*mul
        public int Level; // -1: custom
        public float EnemySpawnInterval;
        public float EnemySpawnIntervalDeviation;
        public float AllyCardSpawnInterval;
        public float AllyCardSpawnIntervalDeviation;

        public StageData(   List<int> enemyType,
                            int enemyQuantity = 10,
                            float enemySpeedMultiplier = 1.0f,
                            float enemySpawnInterval = 7.0f,
                            float enemySpawnIntervalDeviation = 0.0f,
                            float allyCardSpawnInterval = 6.5f,
                            float allyCardSpawnIntervalDeviation = 0.0f,
                            int level = -1) {
            EnemyType = enemyType;
            EnemyQuantity = enemyQuantity;
            EnemySpeedMultiplier = enemySpeedMultiplier;
            EnemySpawnInterval = enemySpawnInterval;
            EnemySpawnIntervalDeviation = enemySpawnIntervalDeviation;
            AllyCardSpawnInterval = allyCardSpawnInterval;
            AllyCardSpawnIntervalDeviation = allyCardSpawnIntervalDeviation;
            Level = level;

            // TODO: check here
            // MinEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.01f, level - 1) - 0.51f, 0.35f);
            // MaxEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.03f, level - 1) + 1.97f, 2.0f);
            // MinAllyCardSpawnInterval = 5.0f;
            // MaxAllyCardSpawnInterval = 10.0f;
            // EnemyQuantity = (int)Mathf.Min(Mathf.Pow(1.051f, level - 1) - 0.051f, 12) * 15;
        }
    }

    StageData _data { get; set; }

    bool _isPlaying;

    #endregion

    #region Method

    void InitialStage(StageData data) {
        GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        _data = data;
        _isPlaying = false;
    }

    void StageStart(){

    }

    #endregion

    #region Interface

    static public void StartNewStage(   List<int> enemyType,
                                        int enemyQuantity = 10,
                                        float enemySpeedMultiplier = 1.0f,
                                        float enemySpawnInterval = 7.0f,
                                        float enemySpawnIntervalDeviation = 0.0f,
                                        float allyCardSpawnInterval = 6.5f,
                                        float allyCardSpawnIntervalDeviation = 0.0f,
                                        int level = -1) {
        CreateNewInstance().InitialStage(new StageData(
            enemyType,
            enemyQuantity,
            enemySpeedMultiplier,
            enemySpawnInterval,
            enemySpawnIntervalDeviation,
            allyCardSpawnInterval,
            allyCardSpawnIntervalDeviation,
            level
        ));
        Instance.StageStart();
    }

    public void TriggerPause() {

    }

    public void GameOver() {
        Destroy(gameObject);
    }

    #endregion

    #region Monobehaviour

    void FixedUpdate(){
        Log("asf");
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