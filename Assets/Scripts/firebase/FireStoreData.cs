using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

namespace FireStoreData {
   
    [FirestoreData]
    public class UserData {
        [FirestoreProperty]
        public string uid { get; set; }
        [FirestoreProperty]
        public string studentID {get; set; }
        [FirestoreProperty]
        public string actualName {get; set;}
        [FirestoreProperty]
        public bool admin {get; set;}
    }

    [FirestoreData]
    public class UserStagesData {
        [FirestoreProperty]
        public List<Dictionary<string, object>> stagesData { get; set; }
    }

    public static class Test {
        public static string TEST_STRING = "eeeeeeeeeee";
    }
}
