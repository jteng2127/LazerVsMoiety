using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyUnit : Unit
{
    public int enemy_unit_id;
    public int blood;

    [SerializeField]
    private class EnemyUnitData
    {
        public int enemy_unit_id;
        public string name;
    }

    /// <summary>
    /// Query the enemy unit data from the Json.
    /// </summary>
    private string _queryEnemyUnitData(int enemy_unit_query_id)
    {
        string load_enemy_unit_data = File.ReadAllText("../jsons/EnemyUnit.json");
        List<EnemyUnitData> data_list = JsonUtility.FromJson<List<EnemyUnitData>>(load_enemy_unit_data);
        return data_list[enemy_unit_query_id].name;
    }

    /// <summary>
    /// Enemy unit move left
    /// </summary>
    public void moveLeft()
    {
        // moveAToBbySpeed();

        // if the enemy arrive the left edge, then it must call mower.
    }
}