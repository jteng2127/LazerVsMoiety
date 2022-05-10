using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOutSceneButton : MonoBehaviour {
    public void Click() {
        Debug.Log("SignOutSceneButton: Click");
        AuthManager.Instance.SignOut();

        FireStoreManager.Instance.Test();
    }
}
