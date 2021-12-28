using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInSceneButton : MonoBehaviour
{
    RectTransform _rectTransform;
    GameObject _signInWindow;

    Vector3 _startPosition;
    Vector3 _signInWindowPosition;
    bool _isOpenSignInWindow;
    bool _isSignedIn;

    void Awake(){
        // Initial variables
        _signInWindow = GameObject.Find("SignInWindow");
        _rectTransform = transform.GetComponent<RectTransform>();
        _startPosition = new Vector3(0.0f, -335.5f, 0.0f);
        _signInWindowPosition = new Vector3(0.0f, -265.0f, 0.0f);

        // Initial state
        _isSignedIn = false;
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;
    }

    #region Click behavior

    void TriggerSignInWindow(){
        if(_isOpenSignInWindow == true){
            _isOpenSignInWindow = false;
            _signInWindow.SetActive(false);
            _rectTransform.anchoredPosition3D = _startPosition;
            _isSignedIn = true;
        }
        else{
            _isOpenSignInWindow = true;
            _signInWindow.SetActive(true);
            _rectTransform.anchoredPosition3D = _signInWindowPosition;
        }
    }

    void CheckSignedIn(){
        if(_isSignedIn){
            
        }
    }

    #endregion

    #region interface

    public void Click(){
        TriggerSignInWindow();
        
    }

    #endregion
}
