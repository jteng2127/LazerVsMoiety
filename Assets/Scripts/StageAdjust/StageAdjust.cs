using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageAdjust : MonoBehaviour {
    #region Data
    [SerializeField]
    private Transform _enemyTypeToggleList;
    [SerializeField]
    private Transform _enemySpawnNumberTotal;
    [SerializeField]
    private Transform _enemySpeedMultiplier;
    [SerializeField]
    private Transform _enemySpawnInterval;

    private Dictionary<string, Slider> _sliders;

    private StageSettingData StageSettingData;
    private List<int> UnitType;
    #endregion

    #region Method
    public void StartStage() {
        UpdateUnitTypeToggle();
        StageSettingData = new StageSettingData(
            unitType: UnitType,
            enemySpawnNumberTotal: (int)_sliders["EnemySpawnNumberTotal"].value,
            enemySpeedMultiplier: _sliders["EnemySpeedMultiplier"].value * 0.1f,
            enemySpawnInterval: _sliders["EnemySpawnInterval"].value * 0.1f
        );

        StageManager
            .CreateNewStage(StageSettingData)
            .TriggerReady();
    }

    private void ToggleEnemyType(bool isSelect, int type) {
        if (isSelect && !UnitType.Contains(type)) {
            UnitType.Add(type);
        }
        if (!isSelect && UnitType.Contains(type)) {
            UnitType.Remove(type);
        }
    }

    private void ConnectSliderAndInput(Slider slider, InputField input, float mul = 1) {
        slider.onValueChanged.AddListener(
            value => {
                if (float.TryParse(input.text, out float result)) {
                    input.text = "" + (value * mul);
                }
            }
        );

        input.onValueChanged.AddListener(
            value => {
                if (float.TryParse(value, out float result)) {
                    slider.value = result / mul;
                }
                else {
                    slider.value = slider.minValue;
                }
            }
        );
        input.onEndEdit.AddListener(
            value => {
                input.text = "" + (slider.value * mul);
            }
        );
        input.text = "" + (slider.value * mul);
    }
    private void UpdateUnitTypeToggle() {
        foreach (Transform child in _enemyTypeToggleList) {
            Toggle toggle = child.GetComponent<Toggle>();
            if (toggle.isOn) {
                UnitType.Add(int.Parse(child.name));
            }
        }
    }
    #endregion

    #region MonoBahaviour
    void Start() {
        UnitType = new List<int>();
        UpdateUnitTypeToggle();

        foreach (Transform child in _enemyTypeToggleList) {
            Toggle toggle = child.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(
                isSelect => {
                    if (!isSelect && UnitType.Count <= 1) toggle.isOn = true;
                    else ToggleEnemyType(isSelect, Int32.Parse(child.name));
                }
            );
        }

        _sliders = new Dictionary<string, Slider>();
        _sliders["EnemySpawnNumberTotal"] = _enemySpawnNumberTotal.transform.Find("Slider").GetComponent<Slider>();
        _sliders["EnemySpeedMultiplier"] = _enemySpeedMultiplier.transform.Find("Slider").GetComponent<Slider>();
        _sliders["EnemySpawnInterval"] = _enemySpawnInterval.transform.Find("Slider").GetComponent<Slider>();
        ConnectSliderAndInput(
            _sliders["EnemySpawnNumberTotal"],
            _enemySpawnNumberTotal.transform.Find("InputField").GetComponent<InputField>()
        );
        ConnectSliderAndInput(
            _sliders["EnemySpeedMultiplier"],
            _enemySpeedMultiplier.transform.Find("InputField").GetComponent<InputField>(),
            0.1f
        );
        ConnectSliderAndInput(
            _sliders["EnemySpawnInterval"],
            _enemySpawnInterval.transform.Find("InputField").GetComponent<InputField>(),
            0.1f
        );
    }
    #endregion
}
