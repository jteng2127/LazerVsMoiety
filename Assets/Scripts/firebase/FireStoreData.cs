using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

namespace FireStoreData {
   
    [FirestoreData]
    public class UserData {
        [FirestoreProperty]
        public string UID { get; set; }
        [FirestoreProperty]
        public string studentID {get; set; }
        [FirestoreProperty]
        public string actualName {get; set;}
        [FirestoreProperty]
        public bool admin {get; set;}
    }

    [FirestoreData]
    public class UserScoreData {
        [FirestoreProperty]
        public string UID { get; set; }
        [FirestoreProperty]
        public ArrayList score { get; set; }
    }

    public static class Test {
        public static string TEST_STRING = "eeeeeeeeeee";
    }
}
