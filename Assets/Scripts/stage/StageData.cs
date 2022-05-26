using System.Collections.Generic;

// TODO: StageData deprecated, leave it for future stage info
public class StageData {
    /// Level info
    public readonly int LevelID; // -1: custom
    public readonly string StageName;
    public readonly string StageDescription;

    // constructor
    public StageData(int levelID = -1) {
        /// Level info
        LevelID = levelID;
        // StageName = ""
        // StageDescription = ""
    }
}
