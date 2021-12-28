using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInSceneButton : MonoBehaviour
{
    RectTransform _rectTransform;
    Image _image;
    GameObject _signInWindow;

    Vector3 _startPosition;
    Vector3 _signInWindowPosition;
    bool _isOpenSignInWindow;
    bool _isSignedIn;

    Sprite _signInSprite;
    Sprite _startSprite;

    void Awake(){
        // Initial variables
        _signInWindow = GameObject.Find("SignInWindow");
        _rectTransform = transform.GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _startPosition = new Vector3(0.0f, -335.5f, 0.0f);
        _signInWindowPosition = new Vector3(0.0f, -265.0f, 0.0f);
        _signInSprite = Resources.Load<Sprite>("SignIn/sign_in_button");
        _startSprite = Resources.Load<Sprite>("SignIn/start_button");

        // Initial state
        _isSignedIn = false;
        _isOpenSignInWindow = false;
        _signInWindow.SetActive(false);
        _rectTransform.anchoredPosition3D = _startPosition;
        _image.sprite = _signInSprite;
    }

    #region Click behavior

    void TriggerSignInWindow(){
        if(_isOpenSignInWindow == true){
            _isOpenSignInWindow = false;
            _signInWindow.SetActive(false);
            _rectTransform.anchoredPosition3D = _startPosition;
            SignIn();
        }
        else{
            _isOpenSignInWindow = true;
            _signInWindow.SetActive(true);
            _rectTransform.anchoredPosition3D = _signInWindowPosition;
        }
    }

    void SignIn(){
        _isSignedIn = true;
        _image.sprite = _startSprite;
    }

    #endregion

    #region interface

    public void Click(){
        if(!_isSignedIn) TriggerSignInWindow();
        else if(_isSignedIn){
            StageManager.StartNewStage(new List<int>(), 0, 0f);
            // GameManager.Instance.LoadScene(GameManager.SceneType.Stage);
        }
    }

    #endregion
}
