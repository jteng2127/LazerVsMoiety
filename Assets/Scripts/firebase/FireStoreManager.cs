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
        Debug.Log("[FireStoreManager] FireStoreManager Enable");
    }

    private FirebaseFirestore db {
        get {
            return FirebaseFirestore.DefaultInstance;
        }
    }

    #endregion

    #region Firestore API
    public async Task GetUserData() {
        string uid = AuthManager.Instance.getUID();
        DocumentReference docRef = db.Collection("users").Document(uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists) {
            FireStoreData.UserData data = snapshot.ConvertTo<FireStoreData.UserData>();
            DataManager.instance.userData = data;
        }
        else {
            // Firestore Error
            // https://firebase.google.com/docs/reference/unity/namespace/firebase/firestore#firestoreerror
            Debug.LogError("[FireStoreManager] No such document");
        }
    }

    public async Task UpdateUserData() {
        string uid = AuthManager.Instance.getUID();
        DocumentReference docRef = db.Collection("users").Document(uid);
        await docRef.SetAsync(DataManager.instance.userData);
    }

    public async Task UpdateUserData(string actualName) {
        string uid = AuthManager.Instance.getUID();
        DocumentReference docRef = db.Collection("users").Document(uid);
        await docRef.UpdateAsync("actualName", actualName);
    }

    public async Task RegisterUserData(FireStoreData.UserData userData, string uid) {
        DocumentReference docRef = db.Collection("users").Document(uid);
        await docRef.SetAsync(userData);
    }

    public async Task GetUserStagesData() {
        string uid = AuthManager.Instance.getUID();
        DocumentReference docRef = db.Collection("userStages").Document(uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists) {
            FireStoreData.UserStagesData data = snapshot.ConvertTo<FireStoreData.UserStagesData>();
            DataManager.instance.userStagesData = data;
        }
        else {
            Debug.LogError("[FireStoreManager] No such document");
        }
    }

    public async Task UpdateUserStagesData() {
        string uid = AuthManager.Instance.getUID();
        DocumentReference docRef = db.Collection("userStages").Document(uid);
        await docRef.SetAsync(DataManager.instance.userStagesData);
    }

    #endregion
}

