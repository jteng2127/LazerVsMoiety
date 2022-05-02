using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;

// TODO: need big modify
public class ScoreManager : MonoBehaviour {
    #region Interface

    static public void NewScore() {
        CreateNewInstance();
        Instance.Initial();
    }

    public int TotalScore {
        get {
            int total = 0;
            foreach (KeyValuePair<String, int> score in Scores) {
                total += score.Value;
            }
            return total;
        }
    }

    public Dictionary<String, int> GetScoreDict() {
        return Scores;
    }

    public void ModifyScore(int addScore = 0, int target = -1) {
        AddScoreByKey("Other", addScore);
        if (target != -1) {
            AllClear();
        }
    }

    public void DefeatEnemy() {
        int addScore = 100 + (100 / 2 * Math.Min(_combo, 8));
        AddScoreByKey("DefeatEnemy", addScore);
        _combo++;
    }

    public void GameOver() {
        int addScore = 0;
        if (!StageManager.Instance.Data.IsLose) {
            addScore = StageManager.Instance.Data.EnemySpawnNumberTotal * 300;
            AddScoreByKey("GameOver", addScore);
            AddScoreByKey("CannonLeft", 500 * StageManager.Instance.Data.CannonLeft);
        }
    }

    public void ComboBreak() {
        _combo = 0;
    }

    public void AllClear() {
        Scores = new Dictionary<String, int>();
    }

    #endregion
    #region Data

    /// Score type
    public Dictionary<String, int> Scores { get; protected set; }

    int _combo;

    #endregion
    #region Method

    void Initial() {
        Scores = new Dictionary<string, int>();
        _combo = 0;
    }

    void AddScoreByKey(String key, int addScore) {
        if (StageManager.Instance.Data.GameState == 1) {
            if (!Scores.ContainsKey(key)) Scores[key] = 0;
            Scores[key] += addScore;
            Log("add " + key + ": " + addScore);
        }
    }

    #endregion
    #region Debug

    protected static void Log(string s) {
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType + ": " + s);
    }

    #endregion
    #region Singleton

    protected static ScoreManager s_Instance;

    public static ScoreManager Instance {
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
        GameObject go = new GameObject("ScoreManager", typeof(ScoreManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<ScoreManager>();
    }

    static void DestroyInstance() {
        if (s_Instance) {
            Log("Destroy instance");
            Destroy(s_Instance.gameObject);
        }
    }

    #endregion

    /* TODO: check up
    public int score;

    /// <summary>
    /// Calculate the score of the player.
    /// </summary>
    public int calcScore(){
        int score = 0;
        // cost_time = stage.coefficient.enemy_spawn_time;
        // num = stage.coefficient.enemy_quantity;
        // count = 0; // count the number of ally
        // mower = 0; // count the number of mower
        // score = max(num * cost_time * 4 - time, 2) * 100 +
        //         120 * max(num * 1.5 - count, -10) + 
        //         mower * 300;
        return score;
    }

    /// <summary>
    /// Using firebase to upload the score.
    /// </summary>
    public void uploadScore(){

    }

    // public void shareScore(){
        
    // }
    */
}