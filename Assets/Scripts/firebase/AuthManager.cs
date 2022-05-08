using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class AuthManager : MonoBehaviour {
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

    [Header("SigninUI")]
    private Transform studentIDInput;
    private Transform passwordInput;
    private GameObject loginButton;

    private SignInSceneButton signInSceneButton;

    private void _init() {
        loginButton = GameObject.Find("Button");
        signInSceneButton = loginButton.GetComponent<SignInSceneButton>();

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
                signInSceneButton.SignInSuccess();
            }
        }
    }

    private void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    #region Methods

    public void SignInButton(string studentID, string password) {
        StartCoroutine(Signin(studentID, password));
    }

    public void SignOut() {
        auth.SignOut();
    }

    public bool isSignedIn() {
        return user != null;
    }

    public string getStudentEmail() {
        return user.Email;
    }

    #endregion

    #region SigninAndRegister

    private IEnumerator Signin(string studentID, string password) {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(studentID, password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null) {
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            bool isRegister = false;
            switch (errorCode) {
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
                    isRegister = true;
                    StartCoroutine(Register(studentID, password));
                    break;
            }
            Debug.Log(message);
            if (!isRegister) signInSceneButton.SignInFail(message);
        }
        else { // login success
            Debug.Log("Login Successful");
            signInSceneButton.SignInSuccess();
        }
    }

    private IEnumerator Register(string studentID, string password) {
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(studentID, password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        if (RegisterTask.Exception != null) {
            //If there are errors handle them
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Register Failed!";
            switch (errorCode) {
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
            signInSceneButton.SignInFail(message);
        }
        else {
            var User = RegisterTask.Result;

            if (User != null) {
                UserProfile profile = new UserProfile { DisplayName = studentID };
                var ProfileTask = User.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null) {
                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    Debug.Log("studentID Set Failed!");
                    signInSceneButton.SignInFail("studentID Set Failed!");
                }
                else {
                    Debug.Log("studentID Set Successfully");
                    signInSceneButton.SignInSuccess();
                }
            }
        }

    }

    #endregion

}
