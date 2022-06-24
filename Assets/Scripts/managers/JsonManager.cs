using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

// TODO: rearrange JsonManager.cs
public class JsonManager : MonoBehaviour {

    #region Data

    protected static JsonManager s_Instance;

    public static JsonManager Instance {
        get {
            if (s_Instance == null) {
                CreateDefault();
            }
            return s_Instance;
        }
    }

    static void CreateDefault() {
        GameObject go = new GameObject("JsonManager", typeof(JsonManager));
        go.name = "JsonManager";
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<JsonManager>();
        // reduce fps
        // #if UNITY_EDITOR
        //     Application.targetFrameRate = -1;
        //     Debug.Log("Using Editor performance cap for my own sanity");
        // #endif
    }

    // void OnEnable() {
    //     Debug.Log("JsonManager Enable");
    //     _initSceneTypeDictionary();
    // }

    /*
        TODO:
        put all data into this class as static vairable
        use sign
    */

    #endregion

    #region JSONHELP

    public static class JsonHelper {
        public static T[] FromJson<T>(string json) {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array) {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint) {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T> {
            public T[] Items;
        }
    }

    #endregion

    #region AllyJSON

    [Serializable]
    public class AllyJson {
        // functaional Group and wave
        public int ally_id;
        public string ally_name;
    }
    static private AllyJson[] _AllyDataList;


    /// <summary>
    /// Query the Enemy and Ally data list from the Json.
    /// </summary>
    /// <returns> 
    /// Return the Enemy and Ally datalist. <br/>
    /// Data list format: <br/>
    /// [ {"enemy_unit_id": 1, "ally_unit_id": 1100},...]
    /// </returns>
    static private void _loadAllyDataList() {
        string json = File.ReadAllText("../jsons/AllyData.json");
        _AllyDataList = JsonHelper.FromJson<AllyJson>(json);
    }

    static public string GetAllyName(int query_ally_id) {
        // _loadAllyDataList();
        foreach (AllyJson data in _AllyDataList) {
            if (data.ally_id == query_ally_id) {
                return data.ally_name;
            }
        }
        return "";
    }

    #endregion

    #region EnemyJSON

    [Serializable]
    public class EnemyData {
        public int enemy_id;
        public string name;
    }

    static private EnemyData[] _enemyAndAllyDataList;

    static private void _loadEnemyDataList() {
        string json = File.ReadAllText("../jsons/EnemyData.json");
        _enemyAndAllyDataList = JsonHelper.FromJson<EnemyData>(json);
    }

    /// <summary>
    /// Query the enemy unit data from the Json.
    /// </summary>
    static public string GameEnemyName(int query_enemy_id) {
        // _loadEnemyDataList();
        foreach (EnemyData enemy in _enemyAndAllyDataList) {
            if (enemy.enemy_id == query_enemy_id) {
                return enemy.name;
            }
        }
        return "";
    }

    #endregion

    #region StageJSON

    [Serializable]
    public class StageData {
        public int stage_id;
        public int unit_quantity;
        public List<int> unit_id_list;
        public int enemy_quantity;
        public float enemy_speed_multiplier;
    }

    static private StageData[] _stageDataList;

    static void _loadStageDataList() {
        string json = File.ReadAllText("../jsons/StageData.json");
        _stageDataList = JsonHelper.FromJson<StageData>(json);
    }

    #endregion
}
