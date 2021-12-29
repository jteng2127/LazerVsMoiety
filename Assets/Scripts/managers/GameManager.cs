using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    protected static GameManager s_Instance;

    public static GameManager Instance
    {
        // [RuntimeInitializeOnLoadMethod]
        get
        {
            if(s_Instance == null)
            {
                Debug.Log("GameManager: Create new instance.");
                // s_Instance = ScriptableObject.CreateInstance<GameManager>();
                CreateDefault();
            }
            return s_Instance;
        }
    }

    static void CreateDefault(){
        GameObject go = new GameObject("GameManager", typeof(GameManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<GameManager>();
        // reduce fps
        // #if UNITY_EDITOR
        //     Application.targetFrameRate = -1;
        //     Debug.Log("Using Editor performance cap for my own sanity");
        // #endif
    }

    void OnEnable()
    {
        Debug.Log("GameManager Enable");
        _initGameSceneManage();
    }

    #endregion

    #region Scene

    public enum SceneType
    {
        /// <summary> Loding page </summary>
        Loading,
        SignIn,
        Menu,
        /// <summary> Logout etc. </summary>
        User,
        Setting,
        StageSelect,
        Stage,
        GameOver,
    }

    Dictionary<SceneType, string> _sceneTypeToString;

    void _initGameSceneManage(){
        Debug.Log("Initial SceneTypeDictionary");
        _sceneTypeToString = new Dictionary<SceneType, string>{
            {SceneType.Loading, "Loading"},
            {SceneType.SignIn, "SignIn"},
            {SceneType.Menu, "Menu"},
            {SceneType.User, "User"},
            {SceneType.Setting, "Setting"},
            {SceneType.StageSelect, "StageSelect"},
            {SceneType.Stage, "Stage"},
            {SceneType.GameOver, "GameOver"},
        };
    }

    IEnumerator LoadingSceneAsync(SceneType scene){
        SceneManager.LoadScene(_sceneTypeToString[SceneType.Loading]);

        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneTypeToString[scene]);

        operation.allowSceneActivation = false;

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            Debug.Log("progress: " + progress);

            yield return null;
        }
        yield return new WaitForSeconds(5);
        operation.allowSceneActivation = true;
    }

    #endregion

    #region Interface



    public void LoadScene(SceneType scene, bool showLoading = true){
        Debug.Log("Loading " + _sceneTypeToString[scene] + "...");

        StartCoroutine(LoadingSceneAsync(scene));
        // SceneManager.LoadScene(_sceneTypeToString[scene]);
    }

    #endregion
}