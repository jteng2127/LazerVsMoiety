using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager> {

    #region data

    public const int ImagePixelHeightReference = 1440;

    private Dictionary<SceneType, string> _sceneTypeToString;
    private Dictionary<string, SceneType> _stringToSceneType;

    #endregion

    #region public method

    public void LoadScene(SceneType scene, bool isAsync = false, bool showLoading = true) {
        Log("LoadScene: " + _sceneTypeToString[scene]);

        if (isAsync) StartCoroutine(LoadingSceneAsync(scene));
        else SceneManager.LoadScene(_sceneTypeToString[scene]);
    }

    public SceneType GetCurrentScene() {
        return _stringToSceneType[SceneManager.GetActiveScene().name];
    }

    #endregion

    #region private method

    [RuntimeInitializeOnLoadMethod]
    private static void _initGameSceneManager() {
        AudioManager.Instance.StartBackgroundMusic();

        Instance._sceneTypeToString = new Dictionary<SceneType, string>{
            {SceneType.Loading, "Loading"},
            {SceneType.SignIn, "SignIn"},
            {SceneType.Menu, "Menu"},
            {SceneType.User, "User"},
            {SceneType.Setting, "Setting"},
            {SceneType.StageSelector, "StageSelector"},
            {SceneType.StageAdjust, "StageAdjust"},
            {SceneType.Stage, "Stage"},
            {SceneType.GameOver, "GameOver"},
        };
        Instance._stringToSceneType = new Dictionary<string, SceneType>{
            {"Loading", SceneType.Loading},
            {"SignIn", SceneType.SignIn},
            {"Menu", SceneType.Menu},
            {"User", SceneType.User},
            {"Setting", SceneType.Setting},
            {"StageSelector", SceneType.StageSelector},
            {"StageAdjust", SceneType.StageAdjust},
            {"Stage", SceneType.Stage},
            {"GameOver", SceneType.GameOver},
        };
    }

    // TODO: async didn't work
    private IEnumerator LoadingSceneAsync(SceneType scene) {
        SceneManager.LoadScene(_sceneTypeToString[SceneType.Loading]);

        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneTypeToString[scene]);

        operation.allowSceneActivation = false;

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            Debug.Log("progress: " + progress);

            yield return null;
        }
        yield return new WaitForSeconds(5);
        operation.allowSceneActivation = true;
    }

    #endregion

}

public enum SceneType {
    /// <summary> Loding page </summary>
    Loading,
    SignIn,
    Menu,
    /// <summary> Logout etc. </summary>
    User,
    Setting,
    StageSelector,
    StageAdjust,
    Stage,
    GameOver,
}