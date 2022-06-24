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

    #region MonoBehaviour

    void Update() {
        /// Get touch input
        if (StageState is StagePlayState && Input.touchCount > 0) {
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
        else if (StageState is StagePlayState && Input.GetMouseButton(0)) {
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

    #region data
    public int CannonLeft; // TODO: bad code, should move Cannon Left to some where else
    public StageState StageState { get; private set; }
    private StageSettingData StageSettingData;
    public List<StageStateReactBase> ReactList = new List<StageStateReactBase>();
    private EnemySpawnHandler EnemySpawnHandler;
    private AllyCardSpawnHandler AllyCardSpawnHandler;
    public EntityEventHandler EntityEventHandler;
    private GameOverEventHandler GameOverEventHandler;
    #endregion

    #region methods
    static public StageManager CreateNewStage(StageSettingData stageSettingData) {
        DestroyInstance();
        Instance.Initial(stageSettingData);
        return Instance;
    }

    // main stage initial method
    private void Initial(StageSettingData stageSettingData) {
        Log("StageManager.Initial()");
        StageState = new StageInitialState(this);
        StageSettingData = stageSettingData;
    }

    public void OnStageSceneLoaded(Scene scene, LoadSceneMode mode) {
        StageGrid stageGrid = GameObject.Find("StageGrid").GetComponent<StageGrid>();
        AllyCardSpawnHandler = AllyCardSpawnHandler
            .Create(allyCardTypes: StageSettingData.UnitType)
            .SetSpawnInterval(StageSettingData.AllyCardSpawnInterval)
            .SetSpawnIntervalDeviation(StageSettingData.AllyCardSpawnIntervalDeviation);
        EnemySpawnHandler = EnemySpawnHandler
            .Create(
                AllyCardSpawnHandler,
                stageGrid.RowYList,
                enemyTypes: StageSettingData.UnitType,
                movingSpeedMultiplier: StageSettingData.EnemySpeedMultiplier)
            .SetSpawnNumberTotal(StageSettingData.EnemySpawnNumberTotal)
            .SetSpawnInterval(StageSettingData.EnemySpawnInterval)
            .SetSpawnIntervalDeviation(StageSettingData.EnemySpawnIntervalDeviation);
        EntityEventHandler = EntityEventHandler
            .Create(EnemySpawnHandler, AllyCardSpawnHandler);
        GameOverEventHandler = GameOverEventHandler
            .Create(EnemySpawnHandler, StageSettingData.StageID);
        ScoreManager.NewScore(EnemySpawnHandler);

        RegisterStageStateReact(AllyCardSpawnHandler);
        RegisterStageStateReact(EnemySpawnHandler);
        RegisterStageStateReact(EntityEventHandler);
        RegisterStageStateReact(GameOverEventHandler);

        CannonLeft = StageGrid.DefGridRowTotal; 

        TriggerStart();
    }

    public void changeState(StageState state) {
        StageState = state;
        LogStageState();
    }
    public void RegisterStageStateReact(StageStateReactBase react) {
        ReactList.Add(react);
        if(!(StageState is StageInitialState) &&
           !(StageState is StageReadyState) && 
           StageState != null) {
            react.GameStart();
        }
        if(StageState is StagePauseState) {
            react.Pause();
        }
    }
    public void UnregisterStageStateReact(StageStateReactBase react) {
        ReactList.Remove(react);
    }

    public void LogStageState() {
        try {
            Log("Current Stage State: " + StageState.GetType().Name);
        }
        catch(Exception e) {
            Log("LogStageState Error: " + e.Message);
            if(StageState == null) {
                Log("StageState is null");
            }
        }
    }

    public void TriggerReady() {
        StageState.GameReady();
    }

    public void TriggerStart() {
        StageState.GameStart();
    }

    public void TriggerPause() {
        StageState.Pause();
    }

    public void TriggerContinue() {
        StageState.Continue();
    }

    public void TriggerGameLose() {
        StageState.GameLose();
    }

    public void TriggerGameWin() {
        StageState.GameWin();
    }

    public void TriggerRestart() {
        StageState.Restart();
    }

    public void TriggerUnitDestroyed(GameObject go) {
        StageState.UnitDestroyed(go);
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