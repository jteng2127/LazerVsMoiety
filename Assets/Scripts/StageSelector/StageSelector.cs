using UnityEngine;
using System.Collections.Generic;

public class StageSelector : MonoSingleton<StageSelector> {
    // {StageID, StageSettingData}
    private SortedDictionary<int, StageSettingData> StageSettingData = null;

    public void SelectStage(int stageID) {
        if (StageSettingData == null) {
            InitialStageSettingData();
        }

        if (stageID >= 1 && stageID <= 10){
            StageManager
                .CreateNewStage(StageSettingData[stageID])
                .TriggerReady();
        } else if (stageID == -1) {
            GameManager.Instance.LoadScene(SceneType.StageAdjust);
        }
    }

    private void InitialStageSettingData() {
        StageSettingData = new SortedDictionary<int, StageSettingData>() {
            {
                1,
                new StageSettingData(
                    unitType: new List<int>() { 1, 2, 3 }
                )
            }, {
                2,
                new StageSettingData(
                    unitType: new List<int>() { 4, 5, 6 }
                )
            }, {
                3,
                new StageSettingData(
                    unitType: new List<int>() { 7, 8, 9, 10 },
                    enemySpawnNumberTotal: 15)
            }, {
                4,
                new StageSettingData(
                    unitType: new List<int>() { 2, 4, 6, 10 },
                    enemySpawnNumberTotal: 17,
                    enemySpawnInterval: 6.5f,
                    allyCardSpawnInterval: 4.5f)
            }, {
                5,
                new StageSettingData(
                    unitType: new List<int>() { 1, 3, 5, 7 },
                    enemySpawnNumberTotal: 20,
                    enemySpeedMultiplier: 1.2f,
                    enemySpawnInterval: 6.0f,
                    allyCardSpawnInterval: 4.2f)
            }, {
                6,
                new StageSettingData(
                    unitType: new List<int>() { 1, 2, 3, 4, 5 },
                    enemySpawnNumberTotal: 23,
                    enemySpeedMultiplier: 1.2f,
                    enemySpawnInterval: 6.0f,
                    allyCardSpawnInterval: 4.2f)
            }, {
                7,
                new StageSettingData(
                    unitType: new List<int>() { 6, 7, 8, 9, 10 },
                    enemySpawnNumberTotal: 25,
                    enemySpeedMultiplier: 1.2f,
                    enemySpawnInterval: 5.5f,
                    allyCardSpawnInterval: 4.0f)
            }, {
                8,
                new StageSettingData(
                    unitType: new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
                    enemySpawnNumberTotal: 28,
                    enemySpeedMultiplier: 1.4f,
                    enemySpawnInterval: 5.5f,
                    allyCardSpawnInterval: 3.7f)
            }, {
                9,
                new StageSettingData(
                    unitType: new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                    enemySpawnNumberTotal: 30,
                    enemySpeedMultiplier: 0.5f,
                    enemySpawnInterval: 5.5f,
                    allyCardSpawnInterval: 4.5f)
            }, {
                10,
                new StageSettingData(
                    unitType: new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                    enemySpawnNumberTotal: 40,
                    enemySpeedMultiplier: 1.7f,
                    enemySpawnInterval: 5.0f,
                    allyCardSpawnInterval: 3.5f)
            }
        };
    }
}