using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageManger: MonoBehaviour
{
    public int stage_id;
    public double start_time;

    public class StageData
    {
        public string stage_name;
        public string stage_description;
    }
    public class Coefficient{
        private double _min_enemy_spawn_interval;
        private double _max_enemy_spawn_interval;
        private double _min_ally_card_spawn_interval;
        private double _max_ally_card_spawn_interval;
        private double _enemy_quantity;

        public Coefficient(int level){
            level = level - 1;
            _min_enemy_spawn_interval = min(Math.Pow(1.01f, level) - 0.51f, 0.35f);
            _max_enemy_spawn_interval = min(Math.Pow(1.03f, level) + 1.97f, 2.0f);
            _min_ally_card_spawn_interval = 5.0f;
            _max_ally_card_spawn_interval = 10.0f;
            _enemy_quantity = min(Math.Pow(1.051, level) - 0.051, 12) * 15;
        }

        public double enemy_spawn_time {get {
            return _max_enemy_spawn_interval - _min_enemy_spawn_interval;
            }}

        public double enemy_quantity {get {
            return _enemy_quantity;
            }}
    }
    [SerializeField]
    public List<int> enemy_and_ally_id_list = queryEnemyAndAllyIdList(this.level);

    public List<int> queryEnemyAndAllyIdList(int level){
        load_unit_data = File.ReadAllText("../jsons/StageUnit.json");
        data_list = JsonUtility.FromJson<List<EnemyUnitData>>(load_enemy_unit_data);
        return data_list;
    }
    
    // public Coefficient coefficient = new Coefficient(checkpoint_level);
    // public Tuple<int, int> getRemainQuantity(){
    //     return new Tuple<int, int>(coefficient._wave_quantity, coefficient._fg_quantity);
    // }
     
}