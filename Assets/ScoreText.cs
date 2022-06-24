using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    public int StageID = -1;

    private int score;

    void Start() {
        // from GameOverEventHandler
        if(DataManager.instance.userStagesData.ContainStageData(StageID)) {
            score = DataManager.instance.userStagesData.GetStageDataScore(StageID);
        } else {
            score = PlayerPrefs.GetInt("Stage" + StageID + " Best Score", -1);
        }
        if(score != -1) transform.GetComponent<Text>().text = " " + score;
        else transform.GetComponent<Text>().text = " 未通過";
        DataManager.UserStagesDataChanged += OnUserStagesDataChanged;
    }

    void OnUserStagesDataChanged(FireStoreData.UserStagesData data) {
        int new_score = DataManager.instance.userStagesData.GetStageDataScore(StageID);
        if(new_score != -1 && score < new_score) {
            score = new_score;
            PlayerPrefs.SetInt("Stage" + StageID + " Best Score", score);
            transform.GetComponent<Text>().text = " " + score;
        }
        else transform.GetComponent<Text>().text = " 未通過";
    }

    // IEnumerator Start() {
    //     yield return StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>{
    //         FireStoreManager.Instance.GetUserStagesData();
    //     }, 0.1f));
    //     Debug.Log("asdfasdf");
    //     DataManager.instance.userStagesData.AddStageData(1, 1, 1);
    //     // FireStoreData.UserStagesData userStagesData = 
    //     // userStagesData.AddStageData(1, 0, 0);
    //     StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>{
    //         FireStoreManager.Instance.UpdateUserStagesData();
    //     }, 0.1f));

    //     StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>{
    //         FireStoreManager.Instance.GetUserStagesData();
    //     }, 0.1f));

    //     // userStagesData = DataManager.instance.userStagesData;
    //     // foreach(Dictionary<string, object> stageData in userStagesData.stagesData) {
    //     //     int stage = (int)stageData["stage"];
    //     //     int score = (int)stageData["score"];
    //     //     int time = (int)stageData["time"];
    //     //     Debug.Log("Stage: " + stage + " Score: " + score + " Time: " + time);
    //     // }
    //     transform.GetComponent<Text>().text = " ";
    // }

}
