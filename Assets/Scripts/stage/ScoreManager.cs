using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;

// TODO: need big modify
public class ScoreManager : MonoSingleton<ScoreManager> {
    #region Interface
    static public void NewScore(EnemySpawnHandler enemySpawnHandler) {
        DestroyInstance();
        Instance.Initial(enemySpawnHandler);
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

    public void GameWin() {
        int addScore = 0;
        addScore = EnemySpawnHandler.SpawnNumberTotal * 300;
        AddScoreByKey("GameOver", addScore);
        AddScoreByKey("CannonLeft", 500 * StageManager.Instance.CannonLeft);
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
    private int _combo;
    private EnemySpawnHandler EnemySpawnHandler;
    #endregion

    #region Method
    private void Initial(EnemySpawnHandler enemySpawnHandler) {
        Scores = new Dictionary<string, int>();
        _combo = 0;
        EnemySpawnHandler = enemySpawnHandler;
    }
    private void AddScoreByKey(String key, int addScore) {
        if (StageManager.Instance.StageState.GetType() == typeof(StagePlayState)) {
            if (!Scores.ContainsKey(key)) Scores[key] = 0;
            Scores[key] += addScore;
            Log("add score " + key + ": " + addScore);
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