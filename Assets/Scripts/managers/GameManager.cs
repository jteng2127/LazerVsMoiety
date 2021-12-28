using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ScriptableObject
{
    #region Singleton
    protected static GameManager s_Instance;

    public static GameManager Instance
    {
        get
        {
            if(s_Instance == null)
            {
                Debug.Log("GameManager: Create new instance.");
                s_Instance = ScriptableObject.CreateInstance<GameManager>();
            }
            return s_Instance;
        }
    }

    #endregion

    void OnEnable()
    {
        Debug.Log("GameManager Enable");
    }

    #region Scene
    public enum SceneType
    {
        Loading,
        Login,
        Menu,
        User,
        Setting,
        StageSelect,
        Stage,
        GameOver,
    }


    public void Test()
    {
        Debug.Log("test game manager");
    }
}