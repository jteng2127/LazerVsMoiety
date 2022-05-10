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
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    #endregion

    private FirebaseFirestore db;

    public void Init() {
        Debug.Log("FireStoreManager Init");
        Debug.Log(FireStoreData.Test.TEST_STRING);
        db = FirebaseFirestore.DefaultInstance;
    }

    public async Task GetTest() {
        Debug.Log("GetTest");
        DocumentReference docRef = db.Collection("users").Document("00957301@mail.ntou.edu.tw");
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


}
