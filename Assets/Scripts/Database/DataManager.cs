using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager {
    private static DataManager _instance;
    public static DataManager instance {
        get {
            if (_instance == null) {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    public FireStoreData.UserData _userData;
    public FireStoreData.UserData userData {
        get {
            return _userData;
        }
        set {
            if (value != _userData) {
                if (UserDataChanged != null) UserDataChanged(value);
            }
            _userData = value;
        }
    }
    public FireStoreData.UserStagesData _userStagesData;
    public FireStoreData.UserStagesData userStagesData {
        get {
            return _userStagesData;
        }
        set {
            if (value != _userStagesData) {
                if (UserStagesDataChanged != null) UserStagesDataChanged(value);
            }
            _userStagesData = value;
        }
    }

    public static System.Action<FireStoreData.UserData> UserDataChanged;
    public static System.Action<FireStoreData.UserStagesData> UserStagesDataChanged;
}
