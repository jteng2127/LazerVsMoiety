using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoleManager: MonoBehaviour
{
    public RoleType role_type;
    public readonly string picture_src; // picture Src
    public double transparency; // picture transparency
    // public vector3 position;

    [SerializeField]
    private class EnemyAndAllyData
    {
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
    private List<EnemyAndAllyData> _queryEnemyAndAllyDataList()
    {
        load_enemy_and_ally_data = File.ReadAllText("../jsons/EnemyAndAlly.json");
        data_list = JsonUtility.FromJson<List<EnemyAndAllyData>>(load_enemy_and_ally_data);
        return data_list;
    }

    /// <summary>
    /// Query the Enemy with Ally.
    /// </summary>
    /// <returns>
    /// -1, if the Ally is not found.
    /// </returns>
    public int queryEnemyUnitId(int ally_query_id){
        List<EnemyAndAllyData> data_list = _queryEnemyAndAllyDataList();
        foreach (EnemyAndAllyData data in data_list)
        {
            if (data.ally_unit_id == ally_query_id)
            {
                return data.enemy_unit_id;
            }
        }
        return -1;
    }

    /// <summary>
    /// Query the Ally with Enemy.
    /// </summary>
    public int getAllyUnitId(int enemy_query_id){
        List<EnemyAndAllyData> data_list = _queryEnemyAndAllyDataList();
        // Beacuse the enemy_query_id is index at the same time, 
        // so we can't use foreach to iterate it.
        return data_list[enemy_query_id].ally_unit_id;
    }
    
    /// <summary>
    /// GameObject move to the target position.
    /// </summary>
    private void _move(Vector3 destination, double time = 0)
    {
        // object move(x, y)
        transform.position = 
            Vector3.MoveTowards(
                transform.position, 
                destination, 
                time
            );
    }

    /// <summary>
    /// GameObject move by Time.
    /// </summary>
    public void moveAToBbyTime()
    {
        // position A, position B, time
        Vector3 posY = new vector3(B, y, 0);
        InvokeRepeating("_move", 0, time);
        _move(posY, time);
    }

    /// <summary>
    /// move position A to position B by speed
    /// </summary>
    /// <param name="direction">
    /// Direction, 1 is right, -1 is left
    /// </param>
    /// <param name="speed">speed</param>
    public void moveAToBbySpeed(int direction, int speed)
    {
        // position A, position B, speed
        Vector3 posY = new vector3(B, direction * y, 0);
        InvokeRepeating("_move", 0, speed);
        _move(posY, speed);
    }

    /// <summary>
    /// Change Gameobject's transparency.
    /// </summary>
    private void _transparent(double alpha = 1.0f)
    {
        // change transparency
        transparency = alpha;
        var render = gameObject.GetComponentInChildren(Renderer);    
        render.material.color.a = alpha;
    }

    /// <summary>
    /// Let GameObject show.
    /// </summary>
    public void show()
    {
        _transparent(1.0f);
    }

    /// <summary>
    /// Let GameObject hide.
    /// </summary>
    public void hide()
    {
        _transparent(0.0f);
    }

    /// <summary>
    /// Let GameObject destory.
    /// </summary>
    private void destory()
    {
        _destory();
    }
}
