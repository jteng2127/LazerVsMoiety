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

    void OnEnable()
    {
        Debug.Log("GameManager Enable");
        _initSceneTypeDictionary();
    }

    #endregion

    #region Scene

    public enum SceneType
    {
        /// <summary> Loding page </summary>
        Loading,
        Login,
        Menu,
        /// <summary> Logout etc. </summary>
        User,
        Setting,
        StageSelect,
        Stage,
        GameOver,
    }

    Dictionary<SceneType, string> _sceneTypeToString;
    void _initSceneTypeDictionary(){
        Debug.Log("Initial SceneTypeDictionary");
        _sceneTypeToString = new Dictionary<SceneType, string>{
            {SceneType.Loading, "Loading"},
            {SceneType.Login, "Login"},
            {SceneType.Menu, "Menu"},
            {SceneType.User, "User"},
            {SceneType.Setting, "Setting"},
            {SceneType.StageSelect, "StageSelect"},
            {SceneType.Stage, "Stage"},
            {SceneType.GameOver, "GameOver"},
        };
    }

    public void LoadScene(SceneType scene){
        SceneManager.LoadScene(_sceneTypeToString[scene]);
    }

    #endregion


    public void Test()
    {
        Debug.Log("test game manager");
    }
}