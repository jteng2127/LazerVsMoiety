using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.Linq;

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
                throw new NullReferenceException("StageManager Not Exist!");
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
        GameObject go = new GameObject("StageManager", typeof(StageManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<StageManager>();
    }

    static void DestroyInstance() {
        if (s_Instance) {
            Destroy(s_Instance.gameObject);
            s_Instance = null;
        }
    }

    #endregion
    #region Data

    public class StageData {
        /// Level info
        public int Level { get; } // -1: custom
        public string StageName { get; }
        public string StageDescription { get; }
        public int GameState { get; set; } // 0: prepare, 1: gaming, 2: game over
        public bool IsLose { get; set; }

        /// Grid info
        static readonly int DefaultGridRowTotal = 5;
        static readonly int DefaultGridColumnTotal = 9;
        public readonly int GridRowTotal;
        public readonly int GridColumnTotal;
        public int CannonLeft { get; set; }

        /// Spawn info
        static readonly float DefaultEnemySpawnPositionX = 12.0f;
        static readonly float DefaultAllyCardSpawnPositionX = 8.0f;
        static readonly float DefaultAllyCardSpawnPositionY = 4.0f;
        static readonly int DefaultAllyCardSpawnNumberMax = 8;

        public List<int> EnemyType { get; }
        public int EnemySpawnNumberTotal { get; }
        public int EnemySpawnNumberLeft { get; set; }
        public int EnemyCount { get; set; }
        public float EnemySpawnInterval { get; }
        public float EnemySpawnIntervalDeviation { get; }
        public float EnemySpawnTimeLeft { get; set; }
        public float EnemySpawnPositionX { get; }

        public int AllyCardCount { get; set; }
        public int AllyCardSpawnNumberMax { get; }
        public float AllyCardSpawnInterval { get; }
        public float AllyCardSpawnIntervalDeviation { get; }
        public float AllyCardSpawnTimeLeft { get; set; }
        public float AllyCardSpawnPositionX { get; }
        public float AllyCardSpawnPositionY { get; }

        /// Unit info
        static readonly float DefaultEnemySpeed = 0.5f;
        static readonly float DefaultAllyCardSpeed = 0.8f;
        public float EnemySpeed { get; }
        public float AllyCardSpeed { get; }

        public StageData(
                List<int> enemyType,
                int level = -1,
                int enemySpawnNumberTotal = 10,
                float enemySpeedMultiplier = 1.0f,
                float enemySpawnInterval = 7.0f,
                float enemySpawnIntervalDeviation = 0.0f,
                float allyCardSpawnInterval = 4.5f,
                float allyCardSpawnIntervalDeviation = 0.0f) {

            /// Level info
            Level = level;
            GameState = 0;
            IsLose = false;
            // StageName = ""
            // StageDescription = ""

            /// Grid info
            GridRowTotal = DefaultGridRowTotal;
            GridColumnTotal = DefaultGridColumnTotal;
            CannonLeft = GridRowTotal;

            /// Spawn info
            EnemyType = enemyType;
            EnemySpawnNumberTotal = enemySpawnNumberTotal;
            EnemySpawnNumberLeft = EnemySpawnNumberTotal;
            EnemyCount = 0;
            EnemySpawnInterval = enemySpawnInterval;
            EnemySpawnIntervalDeviation = enemySpawnIntervalDeviation;
            EnemySpawnTimeLeft = EnemySpawnInterval;
            EnemySpawnPositionX = DefaultEnemySpawnPositionX;

            AllyCardCount = 0;
            AllyCardSpawnNumberMax = DefaultAllyCardSpawnNumberMax;
            AllyCardSpawnInterval = allyCardSpawnInterval;
            AllyCardSpawnIntervalDeviation = allyCardSpawnIntervalDeviation;
            AllyCardSpawnTimeLeft = AllyCardSpawnInterval;
            AllyCardSpawnPositionX = DefaultAllyCardSpawnPositionX;
            AllyCardSpawnPositionY = DefaultAllyCardSpawnPositionY;

            /// Unit info
            EnemySpeed = DefaultEnemySpeed * enemySpeedMultiplier;
            AllyCardSpeed = DefaultAllyCardSpeed;

            // TODO: check here
            // MinEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.01f, level - 1) - 0.51f, 0.35f);
            // MaxEnemySpawnInterval = Mathf.Min(Mathf.Pow(1.03f, level - 1) + 1.97f, 2.0f);
            // MinAllyCardSpawnInterval = 5.0f;
            // MaxAllyCardSpawnInterval = 10.0f;
            // EnemySpawnNumber = (int)Mathf.Min(Mathf.Pow(1.051f, level - 1) - 0.051f, 12) * 15;
        }
    }

    public StageData Data { get; set; }

    #endregion
    #region Method

    void InitialStage(StageData data) {
        GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        Data = data;
        Data.GameState = 0;
        SpawnManager.CreateNewSpawner();
        ScoreManager.NewScore();
    }

    void StageStart() {
        Instance.Data.GameState = 1;
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            SpawnManager.StartSpawn();
        };
    }

    void GameOver() {
        Data.GameState = 2;
        Debug.Log("Game Over");
        if (Data.IsLose) Debug.Log("You Lose");
        else {
            Debug.Log("You Win!!!");
            ScoreManager.Instance.GameOver();
        }
        Debug.Log("You're Score: " + ScoreManager.Instance.TotalScore);
    }

    #endregion
    #region MonoBehaviour

    void Update() {
        /// Get touch input
        if (Data.GameState == 1 && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            /// show Debug
            // Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            // Debug.Log(pos);
            // Debug.DrawRay(ray.origin, ray.direction*20);
            // Debug.Log(touch.deltaPosition);

            /// touch detect
            if (touch.phase == TouchPhase.Began) {
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                if (!hitAll.Any(hit => hit.collider.tag == "AllyCardMask")) {
                    foreach (RaycastHit2D hit in hitAll) {
                        if (hit.collider.tag == "AllyCard") {
                            hit.collider.gameObject.GetComponent<AllyCard>().CreateDragPreview();
                            break;
                        }
                    }
                }
            }
        }
    }

    #endregion
    #region Interface

    static public void StartNewStage(StageData data) {
        CreateNewInstance();
        Instance.InitialStage(data);
        Instance.StageStart();
    }

    public void TriggerPause() {

    }

    static public void CheckGameOver(bool isLose = false) {
        if (Instance.Data.GameState == 1) {
            if (Instance.Data.EnemyCount <= 0 &&
                Instance.Data.EnemySpawnNumberLeft <= 0) {
                Instance.GameOver();
            }
            if (isLose) {
                Instance.Data.IsLose = true;
                Instance.GameOver();
            }
        }
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
    // public Tuple<int, int> getRemainSpawnNumber(){
    //     return new Tuple<int, int>(coefficient._wave_quantity, coefficient._fg_quantity);
    // }
    */
}