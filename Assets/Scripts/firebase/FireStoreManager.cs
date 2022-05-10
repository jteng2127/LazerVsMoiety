using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using System.Threading.Tasks;

public class FireStoreManager : ScriptableObject {
    #region Singleton

    protected static FireStoreManager s_Instance;
    private FirebaseFirestore db;
    public static FireStoreManager Instance {
        [RuntimeInitializeOnLoadMethod]
        get {
            if (s_Instance == null) {
                s_Instance = ScriptableObject.CreateInstance<FireStoreManager>();
				s_Instance.hideFlags = HideFlags.HideAndDontSave;
            } 
            return s_Instance;
        }
    }

    void OnEnable() {
        Debug.Log("FireStoreManager Enable");
        _init();
    }

    private void _init() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                db = FirebaseFirestore.DefaultInstance;
            }
            else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus.ToString());
            }
        });
    }

    #endregion

    #region Firestore API
    private void CheckFireStore() {
        if(db == null) {
            db = FirebaseFirestore.DefaultInstance;
        }
    }

    public async Task GetUserData() {
        string uid = AuthManager.Instance.getUID();
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
        await docRef.SetAsync(userData);
    }
    
    #endregion
}
