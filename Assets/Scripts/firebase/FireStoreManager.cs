using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase;

public class FireStoreManager : MonoBehaviour {
    #region Singleton

    protected static FireStoreManager s_Instance;

    public static FireStoreManager Instance {
        [RuntimeInitializeOnLoadMethod]
        get {
            if (s_Instance == null) {
                Debug.Log("FireStoreManager: Create new instance.");
                CreateDefault();
            }
            return s_Instance;
        }
    }

    static void CreateDefault() {
        GameObject go = new GameObject("FireStoreManager", typeof(FireStoreManager));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<FireStoreManager>();
    }

    void OnEnable() {
        Debug.Log("FireStoreManager Enable");
        _init();
    }

    #endregion

    private FirebaseFirestore db;
}
