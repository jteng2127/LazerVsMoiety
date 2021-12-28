using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    #region Debug

    protected static void Log(string s){
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType + ": " + s);
    }

    #endregion

    #region Singleton

    protected static StageManager s_Instance;

    public static StageManager Instance
    {
        get
        {
            if(s_Instance == null)
            {
                throw new NullReferenceException();
            }
            return s_Instance;
        }
    }

    static void CreateDefault(){
        Log("Create");
        if(s_Instance){
            Log("Destroy last instance");
            Destroy(s_Instance.gameObject);
        }
        GameObject go = new GameObject("StageManager", typeof(StageManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<StageManager>();
    }

    void OnEnable()
    {
        Log("Enable");
    }

    void OnDestroy(){
        Log("Destroy");
    }

    #endregion

    private class StageData {
        public List<int> EnemyType;
        public int EnemyQuantity;
        public float EnemySpeedMultiplier;
        public int Level;

        public string StageName;
        public string StageDescription;
        private double _minEnemySpawnInterval;
        private double _maxEnemySpawnInterval;
        private double _minAllyCardSpawnInterval;
        private double _maxAllyCardSpawnInterval;
        private double _enemyQuantity;

        public StageData(
            List<int> enemyType,
            int enemyQuantity,
            float enemySpeedMultiplier,
            int level = -1
        ) {
            level = level - 1;
            _minEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.01f, level) - 0.51f, 0.35f);
            _maxEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.03f, level) + 1.97f, 2.0f);
            _minAllyCardSpawnInterval = 5.0f;
            _maxAllyCardSpawnInterval = 10.0f;
            _enemyQuantity = Mathf.Min(Mathf.Pow(1.051f, level) - 0.051f, 12) * 15;
        }
    }

    int _level; // -1: custom
    double _startTime;
    StageData _data;

    static public void StartNewStage(
        List<int> enemyType,
        int enemyQuantity,
        float enemySpeedMultiplier,
        int level = -1
    ) {
        GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        CreateDefault();
        Instance._data = new StageData(
            enemyType,
            enemyQuantity,
            enemySpeedMultiplier,
            level
        );
    }

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

}