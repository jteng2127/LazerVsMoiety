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

        public void AddStageData (int score, int time) {
            Dictionary<string, object> stageData = new Dictionary<string, object>();
            stageData.Add("score", score);
            stageData.Add("time", time);
            stagesData.Add(stageData);
        }

        public void UpdataStageData (int index, int score, int time) {
            Dictionary<string, object> stageData = stagesData[index];
            stageData["score"] = score;
            stageData["time"] = time;
        }
    }
}
