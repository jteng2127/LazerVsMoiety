using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Unit: MonoBehaviour
{
    #region Debug

    protected static void Log(string s) {
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType + ": " + s);
    }

    #endregion

    #region Data

    protected int _id; // unit id
    protected string _pictureSrc; // picture Src
    protected SpriteRenderer _sprite;

    #endregion

    #region Method
         
    #endregion

    #region Interface

    #endregion

    #region MonoBehaviour
         
    #endregion
}