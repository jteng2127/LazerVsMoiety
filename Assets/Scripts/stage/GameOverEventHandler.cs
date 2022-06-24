using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameOverEventHandler : StageStateReactBase {
    #region data
    private int? StageID;
    private Dictionary<int, string> EnemyIDToName;
    private EnemySpawnHandler EnemySpawnHandler;
    private Transform GameOverScreen;
    private Text GameOverText;
    private Text StageDataText;
    private Button RestartButton;
    #endregion

    #region Creator
    public static GameOverEventHandler Create(
            EnemySpawnHandler enemySpawnHandler,
            int? stageID = null) {
        // create new GameObject to hold the GameOverEventHandler
        GameObject go = new GameObject("GameOverEventHandler");
        GameOverEventHandler gameOverEventHandler = go.AddComponent<GameOverEventHandler>();
        gameOverEventHandler.Initial(enemySpawnHandler, stageID);
        return gameOverEventHandler;
    }
    private void Initial(EnemySpawnHandler enemySpawnHandler, int? stageID) {
        EnemySpawnHandler = enemySpawnHandler;
        StageID = stageID;

        EnemyIDToName = new Dictionary<int, string>{
            {1, "C-O"},
            {2, "C-N"},
            {3, "O=O"},
            {4, "C=C"},
            {5, "C=O"},
            {6, "C≡C"},
            {7, "C≡N"},
            {8, "C-H"},
            {9, "N-H"},
            {10, "O-H"}
        };

        Transform gameOverCanvas = GameObject.Find("GameOverCanvas").transform;
        GameOverScreen = gameOverCanvas.Find("GameOverScreen");
        GameOverText = GameOverScreen.Find("GameOverText").GetComponent<Text>();
        RestartButton = GameOverScreen.Find("RestartButton").GetComponent<Button>();
        StageDataText = GameOverText.transform.Find("StageDataText").GetComponent<Text>();
    }
    #endregion

    #region method
    protected override void GameWin() {
        ScoreManager.Instance.GameWin();
        AudioManager.Instance.Play("victory", 0.6f, true, true);
        ShowGameOverScreen("You Win!!!", ScoreManager.Instance.TotalScore);
        if(StageID.HasValue) {
            if(DataManager.instance.userStagesData.ContainStageData(StageID.Value)) {
                DataManager.instance.userStagesData.UpdateStageData(StageID.Value, ScoreManager.Instance.TotalScore, 0);
            } else {
                DataManager.instance.userStagesData.AddStageData(StageID.Value, ScoreManager.Instance.TotalScore, 0);
            }
            FireStoreManager.Instance.UpdateUserStagesData();
            PlayerPrefs.SetInt("Stage" + StageID + " Best Score", ScoreManager.Instance.TotalScore);
        }
        RestartButton.onClick.AddListener(StageManager.Instance.TriggerRestart);
    }

    protected override void GameLose() {
        AudioManager.Instance.Play("defeat", 0.6f, true, true);
        ShowGameOverScreen("You Lose...", ScoreManager.Instance.TotalScore);
        if(StageID.HasValue) {
            if(DataManager.instance.userStagesData.ContainStageData(StageID.Value)) {
                DataManager.instance.userStagesData.UpdateStageData(StageID.Value, ScoreManager.Instance.TotalScore, 0);
            } else {
                DataManager.instance.userStagesData.AddStageData(StageID.Value, ScoreManager.Instance.TotalScore, 0);
            }
            FireStoreManager.Instance.UpdateUserStagesData();
            PlayerPrefs.SetInt("Stage" + StageID + " Best Score", ScoreManager.Instance.TotalScore);
        }
        RestartButton.onClick.AddListener(StageManager.Instance.TriggerRestart);
    }

    public void ShowGameOverScreen(String title, int score) {
        String gameOverMessage = title;
        gameOverMessage += "\nScore: " + score;

        String stageDataMessage = "敵人種類： ";
        int count = 0;
        bool isFirst = true;
        foreach (int id in EnemySpawnHandler.EnemyTypes) {
            if(!isFirst) stageDataMessage += ", ";
            if(count == 6) stageDataMessage += "\n　　　　　 ";
            stageDataMessage += EnemyIDToName[id];
            count++;
            isFirst = false;
        }
        stageDataMessage += "\n敵人量： " + EnemySpawnHandler.SpawnNumberTotal;
        stageDataMessage += "\n敵人移動速度： " + EnemySpawnHandler.MovingSpeedMultiplier;
        stageDataMessage += "\n敵人出現間隔： " + EnemySpawnHandler.SpawnInterval;

        GameOverScreen.gameObject.SetActive(true);
        GameOverText.text = gameOverMessage;
        StageDataText.text = stageDataMessage;
    }
    #endregion
}