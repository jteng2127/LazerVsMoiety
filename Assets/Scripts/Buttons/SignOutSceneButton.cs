using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOutSceneButton : MonoBehaviour {
    public void Click() {
        AuthManager.Instance.SignOut();
    }
}
