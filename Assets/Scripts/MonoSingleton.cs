using UnityEngine;
using System.Reflection;

// Mono singleton generic class, can't initialize on load method
// -> can't use Update() when game start without calling Instance
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;

    private static object _instanceLock = new object();

    public static T Instance {
        get {
            lock(_instanceLock) {
                if (_instance == null) {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    _instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                    Debug.Log("[Singleton] Created singleton object of type " + typeof(T).ToString());
                }
                return _instance;
            }
        }
    }

    protected static void DestroyInstance() {
        if (_instance != null) {
            Destroy(_instance.gameObject);
            _instance = null;
                Debug.Log("[Singleton] Destroy singleton object of type " + typeof(T).ToString());
        }
    }

    protected static void Log(string s) {
        Debug.Log("[" + typeof(T) + "] " + s);
    }

}
