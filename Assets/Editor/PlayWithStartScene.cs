using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class PlayWithStartScene
{
    [MenuItem("Play/Execute starting scene _%h")]
    public static void RunMainScene()
    {
        Scene currentScene = EditorSceneManager.GetActiveScene();
        
        File.WriteAllText(".lastScene",currentScene.name);
        EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
        EditorApplication.isPlaying = true;
    }
   
    [MenuItem("Play/Reload editing scnee _%g")]
    public static void ReturnToLastScene()
    {
        string lastScene=File.ReadAllText(".lastScene");
        EditorSceneManager.OpenScene("Assets/Scenes/" + lastScene + ".unity");
    }
}
