using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class JsonManager : MonoBehaviour {

    #region Data

    static private List<EnemyAndAllyData> _enemyAndAllyDataList;
    /*
        TODO:
        put all data into this class as static vairable
        use sign
    */

    #endregion

    #region QueryEnemyAndAlly

    [Serializable]
    public class EnemyAndAllyData {
        // functaional Group and wave
        public int enemy_unit_id;
        public int ally_unit_id;
    }

    /// <summary>
    /// Query the Enemy and Ally data list from the Json.
    /// </summary>
    /// <returns> 
    /// Return the Enemy and Ally datalist. <br/>
    /// Data list format: <br/>
    /// [ {"enemy_unit_id": 1, "ally_unit_id": 1100},...]
    /// </returns>
    static public List<EnemyAndAllyData> QueryEnemyAndAllyDataList() {
        string load_enemy_and_ally_data = File.ReadAllText("../jsons/EnemyAndAlly.json");
        List<EnemyAndAllyData> data_list = JsonUtility.FromJson<List<EnemyAndAllyData>>(load_enemy_and_ally_data);
        return data_list;
    }

    #endregion

    #region QueryEnemy

    [Serializable]
    public class EnemyUnitData {
        public int enemy_unit_id;
        public string name;
    }

    /// <summary>
    /// Query the enemy unit data from the Json.
    /// </summary>
    static public string QueryEnemyUnitData(int enemy_unit_query_id) {
        string load_enemy_unit_data = File.ReadAllText("../jsons/EnemyUnit.json");
        List<EnemyUnitData> data_list = JsonUtility.FromJson<List<EnemyUnitData>>(load_enemy_unit_data);
        return data_list[enemy_unit_query_id].name;
    }

    #endregion
}
