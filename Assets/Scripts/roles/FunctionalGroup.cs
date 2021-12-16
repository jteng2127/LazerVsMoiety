using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FunctionalGroup : RoleManager
{
    public string functinoalgroup;
    public int blood;

    public FunctionalGroup()
    {
        roleType = new RoleType("FunctionalGroup");
    }

    [SerializeField]
    private class FGName
    {
        public int id;
        public string name;
    }

    private string _getFunctionalGroupName(int id)
    {
        LoadFWdata = File.ReadAllText("../jsons/FW.json");
        DataList = JsonUtility.FromJson<List<FGName>>(LoadFWdata);
        return DataList[id].name;
    }

    // Functional Group move left
    public void moveLeft(){
        moveAToBbySpeed();
    }
}