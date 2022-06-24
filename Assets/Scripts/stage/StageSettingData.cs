using System.Collections.Generic;

public class StageSettingData {
    
    #region stage data
    public int StageID { get; private set; }
    #endregion

    #region spawn info
    public List<int> UnitType { get; private set; }
    public int EnemySpawnNumberTotal { get; private set; }
    public float EnemySpawnInterval { get; private set; }
    public float EnemySpawnIntervalDeviation { get; private set; }
    public float AllyCardSpawnInterval { get; private set; }
    public float AllyCardSpawnIntervalDeviation { get; private set; }

    #region unit info
    public float EnemySpeedMultiplier { get; private set; }
    #endregion

    #endregion

    #region constructor
    public StageSettingData(
            int stageID = -1,
            List<int> unitType = null,
            int enemySpawnNumberTotal = 10,
            float enemySpeedMultiplier = 1.0f,
            float enemySpawnInterval = 7.0f,
            float enemySpawnIntervalDeviation = 0.3f,
            float allyCardSpawnInterval = 4.5f,
            float allyCardSpawnIntervalDeviation = 0.3f) {

        /// stage data
        this.StageID = stageID;

        /// Spawn info
        if (unitType != null) UnitType = unitType;
        else UnitType = new List<int>() { 1, 2, 3 };
        EnemySpawnNumberTotal = enemySpawnNumberTotal;
        EnemySpawnInterval = enemySpawnInterval;
        EnemySpawnIntervalDeviation = enemySpawnIntervalDeviation;
        AllyCardSpawnInterval = allyCardSpawnInterval;
        AllyCardSpawnIntervalDeviation = allyCardSpawnIntervalDeviation;

        /// Unit info
        EnemySpeedMultiplier = enemySpeedMultiplier;
    }
    #endregion
}