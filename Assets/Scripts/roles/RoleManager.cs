using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoleManager: MonoBehaviour
{
    public RoleType roleType;
    public readonly string pictureSrc; // picture Src
    public double transparency; // picture transparency
    public vector3 positionX;
    public vector3 positionY;

    [SerializeField]
    private class FWData
    {
        // functaional Group and wave
        public int fg;
        public int wave;
    }

    private List<FWData> _getFWData()
    {
        LoadFWdata = File.ReadAllText("../jsons/FW.json");
        DataList = JsonUtility.FromJson<List<FWData>>(LoadFWdata);
    }

    // Can get Functaional Group with wave
    public int getFunctional(int waveValue){
        List<FWData> DataList = _getFWData();
        foreach (FWData data in DataList)
        {
            if (data.wave == waveValue)
            {
                return data.fg;
            }
        }
    }

    
    // Can get wave with Functaional Group
    public int getWave(int functionalValue){
        List<FWData> DataList = _getFWData();
        return DataList[functionalValue].wave;
    }
    
    private void _move(Vector3 destination, double time = Time.deltaTime)
    {
        // object move(x, y)
        transform.position = 
            Vector3.MoveTowards(
                transform.position, 
                destination, 
                time
            );
    }

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
    /// <param name="direction">Direction, 1 is right, -1 is left</param>
    /// <param name="speed">speed</param>
    public void moveAToBbySpeed(int direction, int speed)
    {
        // position A, position B, speed
        Vector3 posY = new vector3(B, direction * y, 0);
        InvokeRepeating("_move", 0, speed);
        _move(posY, speed);
    }

    private void _transparent(double alpha = 1.0f)
    {
        // change transparency
        transparency = alpha;
        this.moveAToBbySpeed(1,1);
        var render = gameObject.GetComponentInChildren(Renderer);    
        render.material.color.a = alpha;
    }

    public void show()
    {
        _transparent(1.0f);
    }

    public void hide()
    {
        _transparent(0.0f);
    }

    private void destory()
    {
        _destory();
    }
}
