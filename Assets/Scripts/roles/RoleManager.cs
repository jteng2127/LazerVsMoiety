using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager: MonoBehaviour
{
    public const string pictureSrc; // picture Src
    public double transparency; // picture transparency
    
    void _move(Vector3 destination, double time = Time.deltaTime)
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
        _move(posY, time);
    }

    public void moveAToBbySpeed()
    {
        // position A, postition B, Speed
        Vector3 des = new Vector3(B, y, 0);
        _move(des, speed);
    }

}