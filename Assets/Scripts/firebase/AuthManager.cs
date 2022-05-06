using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class AuthManager : MonoBehaviour
{
    #region Singleton

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

    private SignInSceneButton signInSceneButton;
    
    private void _init(){
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                auth = FirebaseAuth.DefaultInstance;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);
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
        signInSceneButton = loginButton.GetComponent<SignInSceneButton>();
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.Email);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.Email);
            }
        }
    }

    private void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    #region Methods

    public void LoginButton(){
        string studentID = studentIDInput.GetComponent<InputField>().text + "@mail.ntou.edu.tw";
        string password = passwordInput.GetComponent<InputField>().text;
        Debug.Log(studentID + " " + password);
        StartCoroutine(Login(studentID, password));
    }

    #endregion

    #region LoginAndRegister

    private IEnumerator Login(string studentID, string password){
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(studentID, password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if(LoginTask.Exception != null){
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode){
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    StartCoroutine(Register(studentID, password));
                    break;
            }
            Debug.Log(message);   
            signInSceneButton.LoginSuccess();
        }
        else{ // login success
            Debug.Log("Login Successful");
            signInSceneButton.LoginSuccess();
        }
    }

    private IEnumerator Register(string studentID, string password){
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(studentID, password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        if (RegisterTask.Exception != null){
            //If there are errors handle them
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Register Failed!";
            switch (errorCode){
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    message = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use";
                    break;
            }
            signInSceneButton.LoginFail(message);
        }
        else{
            var User = RegisterTask.Result;

            if (User != null){
                UserProfile profile = new UserProfile { DisplayName = studentID };
                var ProfileTask = User.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null){
                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    Debug.Log("studentID Set Failed!");
                    signInSceneButton.LoginFail("studentID Set Failed!");
                }
                else{
                    Debug.Log("studentID Set Successfully");
                    signInSceneButton.LoginSuccess();
                }
            }
        }

    }

    #endregion

}
