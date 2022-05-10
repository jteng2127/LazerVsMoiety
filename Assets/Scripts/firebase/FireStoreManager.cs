using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using System.Threading.Tasks;

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
        // _init();
    }

    #endregion

    private FirebaseFirestore db;
    private AuthManager am;

    private void _init() {
        am = AuthManager.Instance;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus.ToString());
            if (dependencyStatus == DependencyStatus.Available) {
                db = FirebaseFirestore.DefaultInstance;
            }
            else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus.ToString());
            }
        });
    }

    private void CheckFireStore() {
        if(db == null) {
            db = FirebaseFirestore.DefaultInstance;
        }
    }

    public async Task GetUserData() {
        string uid = am.getUID();
        DocumentReference docRef = db.Collection("users").Document(uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists) {
            Debug.LogFormat("Document data for {0} document:", snapshot.Id);
            Dictionary<string, object> city = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in city) {
                Debug.LogFormat("{0}: {1}", pair.Key, pair.Value);
            }
        }
        else {
            Debug.LogFormat("Document {0} does not exist!", snapshot.Id);
        }
    }

    public async Task RegisterUserData(FireStoreData.UserData userData, string uid) {
        Debug.Log(db != null);
        DocumentReference docRef = db.Collection("cities").Document("sjdsd");
        Debug.Log("FireStoreManager: RegisterUserData");
        await docRef.SetAsync(userData);
    }

    public async Task Test() {
        CheckFireStore();
        Debug.Log("FireStoreManager: Test");
        Debug.Log(db != null);
        DocumentReference docRef = db.Collection("cities").Document("LA");
        Debug.Log("FireStoreManager: Test: docRef");
        Dictionary<string, object> city = new Dictionary<string, object>
            {
                { "name", "Los Angeles" },
                { "state", "CA" },
                { "country", "USA" }
           };
        Debug.Log("FireStoreManager: Test: city");
        await docRef.SetAsync(city);
        Debug.Log("FireStoreManager: Test: end");
    }
}
