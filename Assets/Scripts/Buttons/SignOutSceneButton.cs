using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignOutSceneButton : MonoBehaviour {

    private Button ViewScoreButton;

    void Awake() {
        ViewScoreButton = transform.parent.Find("ViewScoreButton").GetComponent<Button>();
    }

    public void Click() {
        AuthManager.Instance.SignOut();
        transform.gameObject.SetActive(false);
        ViewScoreButton.gameObject.SetActive(false);
    }
}
