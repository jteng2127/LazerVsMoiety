using System.Collections.Generic;

public class StageData {
    /// Level info
    public readonly int LevelID; // -1: custom
    public readonly string StageName;
    public readonly string StageDescription;
    public int GameState { get; set; } // 0: prepare, 1: gaming, 2: game over
    public bool IsLose { get; set; }

    /// Grid info
    private static readonly int DefaultGridRowTotal = 5;
    private static readonly int DefaultGridColumnTotal = 9;
    public readonly int GridRowTotal;
    public readonly int GridColumnTotal;
    public int CannonLeft { get; set; }

    /// Spawn info
    private static readonly float DefaultEnemySpawnPositionX = 12.0f;
    private static readonly float DefaultAllyCardSpawnPositionX = 8.0f;
    private static readonly float DefaultAllyCardSpawnPositionY = 4.0f;
    private static readonly int DefaultAllyCardSpawnNumberMax = 8;

    public List<int> EnemyType { get; set; }
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
    static public readonly float DefaultEnemySpeed = 0.5f;
    static public readonly float DefaultAllyCardSpeed = 0.8f;
    public float EnemySpeed { get; }
    public float AllyCardSpeed { get; }

    // constructor
    public StageData(
            List<int> enemyType = null,
            int levelID = -1,
            int enemySpawnNumberTotal = 10,
            float enemySpeedMultiplier = 1.0f,
            float enemySpawnInterval = 7.0f,
            float enemySpawnIntervalDeviation = 0.5f,
            float allyCardSpawnInterval = 4.5f,
            float allyCardSpawnIntervalDeviation = 0.5f) {

        /// Level info
        LevelID = levelID;
        GameState = 0;
        IsLose = false;
        // StageName = ""
        // StageDescription = ""

        /// Grid info
        GridRowTotal = DefaultGridRowTotal;
        GridColumnTotal = DefaultGridColumnTotal;
        CannonLeft = GridRowTotal;

        /// Spawn info
        if (enemyType != null) EnemyType = enemyType;
        else EnemyType = new List<int>() { 1, 2, 3 };
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
