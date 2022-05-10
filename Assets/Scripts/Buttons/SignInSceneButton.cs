using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInSceneButton : MonoBehaviour {
    RectTransform _rectTransform;
    Image _image;
    GameObject _signInWindow;

    Vector3 _startPosition;
    Vector3 _signInWindowPosition;
    bool _isOpenSignInWindow;
    bool _isSignedIn;

    Sprite _signInSprite;
    Sprite _startSprite;

    private InputField _studentIDInput;
    private InputField _passwordInput;

    void Awake() {
        // Initial variables
        _signInWindow = GameObject.Find("SignInWindow");
        _rectTransform = transform.GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _startPosition = new Vector3(0.0f, -335.5f, 0.0f);
        _signInWindowPosition = new Vector3(0.0f, -335.5f, 0.0f);
        _signInSprite = Resources.Load<Sprite>("Images/SignIn/1x/sign_in_button");
        _startSprite = Resources.Load<Sprite>("Images/SignIn/1x/start_button");

        // Initial state
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;
        _isSignedIn = false;
        _image.sprite = _signInSprite;

        // Initial input field
        _studentIDInput = _signInWindow.transform.Find("StudentIDInput").GetComponent<InputField>();
        _passwordInput = _signInWindow.transform.Find("PasswordInput").GetComponent<InputField>();
    }

    #region Click behavior

    void TriggerSignInWindow() {
        if (_isOpenSignInWindow == true) {
            SignIn();            
        }
        else {
            _isOpenSignInWindow = true;
            _signInWindow.SetActive(true);
            _rectTransform.anchoredPosition3D = _signInWindowPosition;
        }
    }

    #endregion

    #region SignMethods

    public void SignInSuccess() {
        _isSignedIn = true;
        _image.sprite = _startSprite;
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;
    }

    public void SignOutSuccess() {
        _isSignedIn = false;
        _image.sprite = _signInSprite;
    }

    public void SignInFail(string message) {
        // TODO: Show error message
        _isSignedIn = false;
    }

    void SignIn() {
        string studentID = _studentIDInput.text + "@mail.ntou.edu.tw";
        string password = _passwordInput.text;
        AuthManager.Instance.SignInButton(studentID, password);
    }

    #endregion

    #region interface

    public void Click() {
        if (!_isSignedIn) TriggerSignInWindow();
        else if (_isSignedIn) {
            GameManager.Instance.LoadScene(GameManager.SceneType.StageAdjust);
            // StageManager.StartNewStage(
            //     new StageManager.StageData(
            //         new List<int> { 1, 2, 3 },
            //         enemySpawnInterval: 3,
            //         allyCardSpawnInterval: 1
            //     )
            // );
        }
    }

    #endregion
}
