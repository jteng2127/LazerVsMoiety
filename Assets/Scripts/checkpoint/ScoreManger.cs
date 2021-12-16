using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManger : MonoBehaviour
{
    public int score;

    public int calcScore(){
        costTime = getRemainQuantity().Item1 * checkpoint.coefficient;
        score = max(num * costTime * 4 - time, 2) * 100 +
                120 * max(num * 1.5 - count, -10) + 
                mower * 300;
        return score;
    }

    public void uploadScore(){

    }

    public void shareScore(){
        
    }
}