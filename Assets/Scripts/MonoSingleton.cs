using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mono singleton generic class, can't initialize on load method
// -> can't use Update() when game start without calling Instance
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;
    private static object _instanceLock = new object();

    public static T Instance {
        get {
            lock(_instanceLock) {
                if (_instance == null) {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null) {
                        GameObject singleton = new GameObject(typeof(T).ToString());
                        _instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }
    }

    public static int testStatic = 0;
}
