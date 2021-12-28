using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    static void CreateNewInstance() {
        Log("Create");
        if (s_Instance) {
            Log("Destroy last instance");
            Destroy(s_Instance.gameObject);
        }
        GameObject go = new GameObject("StageManager", typeof(StageManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<StageManager>();
    }

    #endregion

    #region Data

    public class StageData {
        public string StageName { get; }
        public string StageDescription { get; }

        public int Level { get; } // -1: custom
        public List<int> EnemyType { get; }
        public int EnemyQuantity { get; }
        public float EnemySpeedMultiplier { get; } // TODO: add a default speed, change to EnemySpeed = defalut*mul
        public float EnemySpawnInterval { get; }
        public float EnemySpawnIntervalDeviation { get; }
        public float EnemySpawnPositionX { get; }
        public float AllyCardSpawnInterval { get; }
        public float AllyCardSpawnIntervalDeviation { get; }
        public float AllyCardSpawnPositionX { get; }
        public int GridRowTotal { get; }
        public int GridColumnTotal { get; }

        public StageData(   List<int> enemyType,
                            int enemyQuantity,
                            float enemySpeedMultiplier,
                            float enemySpawnInterval,
                            float enemySpawnIntervalDeviation,
                            float enemySpawnPositionX,
                            float allyCardSpawnInterval,
                            float allyCardSpawnIntervalDeviation,
                            float allyCardSpawnPositionX,
                            int level,
                            int gridRowTotal,
                            int gridColumnTotal) {
            EnemyType = enemyType;
            EnemyQuantity = enemyQuantity;
            EnemySpeedMultiplier = enemySpeedMultiplier;
            EnemySpawnInterval = enemySpawnInterval;
            EnemySpawnIntervalDeviation = enemySpawnIntervalDeviation;
            EnemySpawnPositionX = enemySpawnPositionX;
            AllyCardSpawnInterval = allyCardSpawnInterval;
            AllyCardSpawnIntervalDeviation = allyCardSpawnIntervalDeviation;
            AllyCardSpawnPositionX = allyCardSpawnPositionX;
            Level = level;
            GridRowTotal = gridRowTotal;
            GridColumnTotal = gridColumnTotal;
            // TODO: check here
            // MinEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.01f, level - 1) - 0.51f, 0.35f);
            // MaxEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.03f, level - 1) + 1.97f, 2.0f);
            // MinAllyCardSpawnInterval = 5.0f;
            // MaxAllyCardSpawnInterval = 10.0f;
            // EnemyQuantity = (int)Mathf.Min(Mathf.Pow(1.051f, level - 1) - 0.051f, 12) * 15;
        }
    }

    public StageData Data { get; set; }

    bool _isPlaying;

    #endregion

    #region Method

    void InitialStage(StageData data) {
        GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        Data = data;
        _isPlaying = false;
    }

    void StageStart(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        SpawnManager.StartNewSpawner();
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    #endregion

    #region Interface

    static public void StartNewStage(   List<int> enemyType,
                                        int enemyQuantity = 10,
                                        float enemySpeedMultiplier = 1.0f,
                                        float enemySpawnInterval = 7.0f,
                                        float enemySpawnIntervalDeviation = 0.0f,
                                        float enemySpawnPositionX = 12.0f,
                                        float allyCardSpawnInterval = 6.5f,
                                        float allyCardSpawnIntervalDeviation = 0.0f,
                                        float allyCardSpawnPositionX = 8.0f,
                                        int level = -1,
                                        int gridRowTotal = 5,
                                        int gridColumnTotal = 9) {
        CreateNewInstance();
        Instance.InitialStage(new StageData(
            enemyType,
            enemyQuantity,
            enemySpeedMultiplier,
            enemySpawnInterval,
            enemySpawnIntervalDeviation,
            enemySpawnPositionX,
            allyCardSpawnInterval,
            allyCardSpawnIntervalDeviation,
            allyCardSpawnPositionX,
            level,
            gridRowTotal,
            gridColumnTotal
        ));
        Instance.StageStart();
    }

    public void TriggerPause() {

    }

    public void GameOver() {
        Destroy(gameObject);
    }

    #endregion

    #region MonoBehaviour

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