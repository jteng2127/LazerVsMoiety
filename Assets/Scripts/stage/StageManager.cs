using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
// using System.Xml.Serialization;
// using System.IO;

// TODO: rearrange StageManager.cs
public class StageManager : MonoSingleton<StageManager> {

    #region data

    public StageData Data { get; set; }

    private Transform _gameOverScreen;
    private Text _gameOverText;
    private Text _stageDataText;
    private Button _restartButton;

    private Dictionary<int, string> _enemyIdToName;

    #endregion
    
    #region private method

    private void InitialStage(StageData data) {
        GameManager.Instance.LoadScene(SceneType.Stage);
        Data = data;
        Data.GameState = 0;
        SpawnManager.CreateNewSpawner();
        ScoreManager.NewScore();

        _enemyIdToName = new Dictionary<int, string>{
            {1, "C-O"},
            {2, "C-N"},
            {3, "O=O"},
            {4, "C=C"},
            {5, "C=O"},
            {6, "C≡C"},
            {7, "C≡N"},
            {8, "C-H"},
            {9, "N-H"},
            {10, "O-H"}
        };
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SpawnManager.StartSpawn();
        Transform gameOverCanvas = GameObject.Find("GameOverCanvas").transform;
        _gameOverScreen = gameOverCanvas.Find("GameOverScreen");
        _gameOverText = _gameOverScreen.Find("GameOverText").GetComponent<Text>();
        _stageDataText = _gameOverText.transform.Find("StageDataText").GetComponent<Text>();
        _restartButton = _gameOverScreen.Find("RestartButton").GetComponent<Button>();
        Debug.Log("game over screen: " + _gameOverScreen);
        Debug.Log("game over text: " + _gameOverText);
    }

    private void StageStart() {
        Debug.Log("StageStart");
        Instance.Data.GameState = 1;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void GameOver() {
        if (Data.IsLose){
            Debug.Log("You Lose");
            AudioManager.Instance.Play("defeat", 0.6f, true, true);
        }
        else {
            Debug.Log("You Win!!!");
            ScoreManager.Instance.GameOver();
            AudioManager.Instance.Play("victory", 0.6f, true, true);
        }
        Debug.Log("You're Score: " + ScoreManager.Instance.TotalScore);

        String gameOverMessage = "";
        if (Data.IsLose) gameOverMessage = "You Lose...\n";
        else gameOverMessage = "You Win!!!\n";
        gameOverMessage += "Score: " + ScoreManager.Instance.TotalScore;

        String stageDataMessage = "";
        stageDataMessage += "敵人種類： ";
        int count = 0;
        bool firstItem = true;
        foreach (int id in Data.EnemyType) {
            if(!firstItem) stageDataMessage += ", ";
            count++;
            if(count == 6){
                stageDataMessage += "\n　　　　　 ";
            }
            firstItem = false;
            stageDataMessage += _enemyIdToName[id];
        }
        stageDataMessage += "\n敵人量： " + Data.EnemySpawnNumberTotal;
        stageDataMessage += "\n敵人移動速度： " + (Data.EnemySpeed / StageData.DefaultEnemySpeed);
        stageDataMessage += "\n敵人出現間隔： " + (Data.EnemySpawnInterval);

        _gameOverScreen.gameObject.SetActive(true);
        _gameOverText.text = gameOverMessage;
        _stageDataText.text = stageDataMessage;
        _restartButton.onClick.AddListener(RestartGame);

        Data.GameState = 2;
    }

    private void RestartGame() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameManager.Instance.LoadScene(SceneType.StageAdjust);
    }

    #endregion

    #region MonoBehaviour

    void Update() {
        /// Get touch input
        if (Data.GameState == 1 && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            /// show Debug
            // Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            // Debug.Log(pos);
            // Debug.DrawRay(ray.origin, ray.direction*20);
            // Debug.Log(touch.deltaPosition);

            /// touch detect
            if (touch.phase == TouchPhase.Began) {
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                if (!hitAll.Any(hit => hit.collider.tag == "AllyCardMask")) {
                    foreach (RaycastHit2D hit in hitAll) {
                        if (hit.collider.tag == "AllyCard") {
                            hit.collider.gameObject.GetComponent<AllyCard>().CreateDragPreview();
                            break;
                        }
                    }
                }
            }
        }

        /// Get mouse input
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /// show Debug
            // Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log(pos);
            // Debug.DrawRay(ray.origin, ray.direction*20);

            /// mouse detect
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                if (!hitAll.Any(hit => hit.collider.tag == "AllyCardMask")) {
                    foreach (RaycastHit2D hit in hitAll) {
                        if (hit.collider.tag == "AllyCard") {
                            hit.collider.gameObject.GetComponent<AllyCard>().CreateDragPreview();
                            break;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region public method

    static public void StartNewStage(StageData data) {
        DestroyInstance();
        Instance.InitialStage(data);
        Instance.StageStart();
    }

    static public void TriggerPause() {

    }

    static public void TriggerContinue() {

    }

    static public void TriggerGameOver() {

    }

    static public void TriggerGameWin() {

    }

    static public void CheckGameOver(bool isLose = false) {
        if (Instance.Data.GameState == 1) {
            if (Instance.Data.EnemyCount <= 0 &&
                Instance.Data.EnemySpawnNumberLeft <= 0) {
                Instance.GameOver();
            }
            if (isLose) {
                Instance.Data.IsLose = true;
                Instance.GameOver();
            }
        }
    }

    #endregion

    // static public string SerializeObject(StageData toSerialize)
    // {
    //     XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

    //     using(StringWriter textWriter = new StringWriter())
    //     {
    //         xmlSerializer.Serialize(textWriter, toSerialize);
    //         return textWriter.ToString();
    //     }
    // }

    /* TODO: wait to check up
    double _startTime;

    [SerializeField]
    public List<int> enemy_and_ally_id_list; // = queryEnemyAndAllyIdList(5);

    // TODO: add JsonManager
    public List<int> queryEnemyAndAllyIdList(int level) {
        // string load_unit_data = File.ReadAllText("../jsons/StageUnit.json");
        // List<EnemyUnitData> data_list = JsonUtility.FromJson<List<EnemyUnitData>>(load_enemy_unit_data);
        // return data_list;
        return new List<int>();
    }

    // public Coefficient coefficient = new Coefficient(checkpoint_level);
    // public Tuple<int, int> getRemainSpawnNumber(){
    //     return new Tuple<int, int>(coefficient._wave_quantity, coefficient._fg_quantity);
    // }
    */
}