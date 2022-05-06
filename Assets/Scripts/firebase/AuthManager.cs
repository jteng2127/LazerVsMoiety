using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class AuthManager : MonoBehaviour
{
    #region SingletQon

    protected static AuthManager s_Instance;

    public static AuthManager Instance {
        [RuntimeInitializeOnLoadMethod]
        get {
            if (s_Instance == null) {
                Debug.Log("AuthManager: Create new instance.");
                CreateDefault();
            }
            return s_Instance;
        }
    }

    static void CreateDefault() {
        GameObject go = new GameObject("AuthManager", typeof(AuthManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<AuthManager>();
    }

    void OnEnable() {
        Debug.Log("AuthManager Enable");
        _init();
    }

    #endregion

    [Header("Firebase")]
    private DependencyStatus dependencyStatus;
    private FirebaseAuth auth;
    private FirebaseUser user;

    [Header("LoginUI")]
    private Transform studentIDInput;
    private Transform passwordInput;
    private GameObject loginButton;
    
    private void _init(){
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                auth = FirebaseAuth.DefaultInstance;
            } 
            else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus.ToString());
            }
        });

        Debug.Log("AuthManager test sInit");

        Transform SignInWindow = GameObject.Find("Canvas").transform.Find("SignInWindow");
    
        studentIDInput = SignInWindow.Find("StudentIDInput");
        passwordInput = SignInWindow.Find("PasswordInput");
        loginButton = GameObject.Find("Button");
    }

    public void LoginButton(){
        string studentID = studentIDInput.GetComponent<InputField>().text + "@mail.ntou.edu.tw";
        string password = passwordInput.GetComponent<InputField>().text;
        Debug.Log(studentID + " " + password);
        StartCoroutine(Login(studentID, password));
    }

    private IEnumerator Login(string studentID, string password){
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(studentID, password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if(LoginTask.Exception != null){
            Debug.LogError(LoginTask.Exception.ToString());            
        }
        else{ // login success
            Debug.Log("Login Successful");
            loginButton.GetComponent<SignInSceneButton>().LoginSuccess();
        }
    }

}
