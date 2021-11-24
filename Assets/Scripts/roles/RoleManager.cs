using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleType
{
    string _type;
    string roleType {get {return _type;}}

    public RoleType(int type){
        _type = type;
    }
}

public class RoleManager: MonoBehaviour
{
    public RoleType roleType;
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

    void _transparent(double alpha = 1.0f)
    {
        // change transparency
        transparency = alpha;
        var render = gameObject.GetComponentInChildren(Renderer);    
        render.material.color.a = alpha;
    }

    public void show()
    {
        _transparent(1.0f)
    }

    public void hide()
    {
        _transparent(0.0f)
    }

    public void destory()
    {
        Destory();
    }
}

public class FunctionalGroup : RoleManager
{
    public string functinoalgroup;
    public int blood;

    public FunctionalGroup()
    {
        roleType = new RoleType("FunctionalGroup");
    }
}

public class Wave : RoleManager
{
    public int wave;

    public Wave()
    {
        roleType = new RoleType("Wave");
    }
}