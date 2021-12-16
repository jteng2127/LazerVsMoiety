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