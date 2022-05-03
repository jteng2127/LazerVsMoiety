using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    public string emailInput;
    public string passwordInput;
    public FirebaseAuth auth;
    public FirebaseUser User; 

    public void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                OnFirebaseInitialized.Invoke();
                InitializeFirebase();
            }
        });
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate
        {
            BtnClick();
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
    }

    // test function
    public void BtnClick()
    {
        Debug.Log("Btn Click");
        auth = FirebaseAuth.DefaultInstance;
        string testUser = "0095715";
        string testPass = "123456";

        /// NOTE: Sign in with email and password
        StartCoroutine(Login(testUser, testPass));
    }

    private void RequireRegister(string _username, string _password)
    {
        // TODO: call dialog window to check if user want to register
        StartCoroutine(Register(_username, _password));
    }

    private IEnumerator Login(string _username, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_username + "@mail.ntou.edu.tw", _password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);
        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
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
                    RequireRegister(_username, _password);
                    break;
            }
            Debug.Log(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
        }
    }

    private IEnumerator Register(string _username, string _password)
    {
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_username + "@mail.ntou.edu.tw", _password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        if (RegisterTask.Exception != null)
        {
            //If there are errors handle them
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Register Failed!";
            switch (errorCode)
            {
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
            Debug.Log(message);
        }
        else
        {
            User = RegisterTask.Result;

            if (User != null)
            {
                UserProfile profile = new UserProfile { DisplayName = _username };
                var ProfileTask = User.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null)
                {
                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    Debug.Log("Username Set Failed!");
                }
                else
                {
                    Debug.Log("Username Set Successfully");
                }
            }
        }

    }
}
