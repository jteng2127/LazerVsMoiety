using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManger : MonoBehaviour
{
    public int score;

    /// <summary>
    /// Calculate the score of the player.
    /// </summary>
    public int calcScore(){
        int score = 0;
        // cost_time = stage.coefficient.enemy_spawn_time;
        // num = stage.coefficient.enemy_quantity;
        // count = 0; // count the number of ally
        // mower = 0; // count the number of mower
        // score = max(num * cost_time * 4 - time, 2) * 100 +
        //         120 * max(num * 1.5 - count, -10) + 
        //         mower * 300;
        return score;
    }

    /// <summary>
    /// Using firebase to upload the score.
    /// </summary>
    public void uploadScore(){

    }

    // public void shareScore(){
        
    // }
}