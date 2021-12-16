using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckpointManger: MonoBehaviour
{
    public int checkpoint_id;
    public int checkpoint_level;
    public string checkpoint_name;
    public string checkpoint_description;
    public double time_consuming;
    public class Coefficient{
        private double _wave_speed;
        private int _wave_quantity;
        private double _fg_speed;
        private int _fg_quantity;

        public Coefficient(int level){
            _wave_speed = min(Math.Pow(1.01, level) - 0.01, 1.6);
            _fg_speed = min(Math.Pow(1.01, level) - 0.01, 1.6) * 1.0;
            _wave_quantity = min(Math.Pow(1.051, level) - 0.051, 12) * 15;
            _fg_quantity = min(Math.Pow(1.051, level) - 0.051, 12) * 15;
        }
    }
    public Coefficient coefficient = new Coefficient(checkpoint_level);
    public Tuple<int, int> getRemainQuantity(){
        return new Tuple<int, int>(coefficient._wave_quantity, coefficient._fg_quantity);
    }
     
    public double getTimeConsuming(){
        time_consuming = Time.time;
        return time_consuming;
    }
}