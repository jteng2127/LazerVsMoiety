﻿using System.Collections;
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
        SignedInStateInit();        
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;

    }

    private void SignedInStateInit(){
        bool isSignedIn = AuthManager.Instance.isSignedIn();
        if (isSignedIn) {
            _isSignedIn = true;
            _image.sprite = _startSprite;
        }
        else{
            _isSignedIn = false;
            _image.sprite = _signInSprite;
        }
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

    public void SignInSuccess() {
        _isSignedIn = true;
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;
        _image.sprite = _startSprite;
    }

    public void SignInFail(string message) {
        // TODO: Show error message
        _isSignedIn = false;
    }

    void SignIn() {
        AuthManager.Instance.SignInButton();
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
