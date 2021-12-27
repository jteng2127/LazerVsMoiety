using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AllyUnit : RoleManager
{
    /// <summary>
    /// ally = -1 means that this the mower
    /// </summary>
    public int ally_unit_id { get; }

    public AllyUnit()
    {
        role_type = new RoleType("AllyUnit");
    }

    /// <summary>
    /// AllyUnit move right
    /// </summary>
    public void moveRight(int ally_unit_id, double speed = 1.0f)
    {
        moveAToBbySpeed(1, speed);
        
        // if the mower arrive right edge, then it must be destoryed
    }

    /// <summary>
    /// Ally collision with Emeny
    /// </summary>
    public void collision()
    {
        if(this.ally_unit_id == -1)
        {
            // mower collision
            // do something 
            GameObject enemy_unit = GameObject.Find("something");
            enemy_unit.destory();
            return;
        }
        
        // ally(wave) collision
        // suppose emeny_unit_id is emeny unit ID
        if(getWave(emeny_unit_id) == this.ally_unit_id)
        {
            this.destory();
        }
    }
}