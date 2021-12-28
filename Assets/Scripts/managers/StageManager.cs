using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

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

    static void CreateDefault() {
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

    private class StageData {
        public List<int> EnemyType;
        public int EnemyQuantity;
        public float EnemySpeedMultiplier; // TODO: add a default speed, change to EnemySpeed = defalut*mul
        public int Level; // -1: custom

        public string StageName;
        public string StageDescription;
        private double _minEnemySpawnInterval;
        private double _maxEnemySpawnInterval;
        private double _minAllyCardSpawnInterval;
        private double _maxAllyCardSpawnInterval;
        private double _enemyQuantity;

        public StageData(List<int> enemyType,
                         int enemyQuantity,
                         float enemySpeedMultiplier,
                         int level = -1) {
            EnemyType = enemyType;
            EnemyQuantity = enemyQuantity;
            EnemySpeedMultiplier = enemySpeedMultiplier;
            Level = level;

            _minEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.01f, level - 1) - 0.51f, 0.35f);
            _maxEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.03f, level - 1) + 1.97f, 2.0f);
            _minAllyCardSpawnInterval = 5.0f;
            _maxAllyCardSpawnInterval = 10.0f;
            _enemyQuantity = Mathf.Min(Mathf.Pow(1.051f, level - 1) - 0.051f, 12) * 15;
        }
    }

    StageData _data { get; set; }

    bool _isPlaying;

    #endregion

    #region Method

    void InitialStage(  List<int> enemyType,
                        int enemyQuantity,
                        float enemySpeedMultiplier,
                        int level = -1) {
        GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        _data = new StageData(
            enemyType,
            enemyQuantity,
            enemySpeedMultiplier,
            level
        );

        _isPlaying = false;
        StageStart();
    }

    void StageStart(){

    }

    #endregion

    #region Interface

    static public void StartNewStage(   List<int> enemyType,
                                        int enemyQuantity,
                                        float enemySpeedMultiplier,
                                        int level = -1) {
        CreateDefault();
        Instance.InitialStage(
            enemyType,
            enemyQuantity,
            enemySpeedMultiplier,
            level = -1
        );
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