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
        public List<Dictionary<string, int>> stagesData { get; set; }

        public void AddStageData (int stage, int score, int time) {
            Dictionary<string, int> stageData = new Dictionary<string, int>();
            stageData.Add("stage", stage);
            stageData.Add("score", score);
            stageData.Add("time", time);
            stagesData.Add(stageData);
        }

        public void UpdateStageData (int stage, int score, int time) {
            for (int i = 0; i < stagesData.Count; i++) {
                if (stagesData[i]["stage"].ToString() == stage.ToString()) {
                    if(stagesData[i]["score"] < score) stagesData[i]["score"] = score;
                    if(stagesData[i]["time"] > time) stagesData[i]["time"] = time;
                }
            }
        }

        public bool ContainStageData (int stage) {
            foreach (Dictionary<string, int> stageData in stagesData) {
                if (stageData["stage"].ToString() == stage.ToString()) {
                    return true;
                }
            }
            return false;
        }

        public int GetStageDataScore (int stage) {
            if (stagesData.Count == 0) {
                return -1;
            }
            foreach (Dictionary<string, int> stageData in stagesData) {
                if (stageData["stage"].ToString() == stage.ToString()) {
                    return (int)stageData["score"];
                }
            }
            return -1;
        }

        public bool hasData() {
            return stagesData.Count != 0;
        }
    }
}
