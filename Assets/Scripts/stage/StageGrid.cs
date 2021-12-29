using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour {

    #region Data

    RectTransform _rectTransform;

    int _gridRowTotal;
    int _gridColumnTotal;
    float _tileHeight;
    float _tileWidth;
    List<List<GameObject>> _gridList;
    public List<float> RowYList { get; set; }
    GameObject _gridTilePrefab;

    #endregion

    #region method

    GameObject CreateTile(float dx, float dy) {
        Vector3 deltaPosition = new Vector3(dx, dy, 0);
        GameObject tile = Instantiate(
            _gridTilePrefab,
            transform.position + deltaPosition,
            Quaternion.identity,
            gameObject.transform
        ) as GameObject;
        tile.GetComponent<BoxCollider2D>().size = new Vector2(_tileWidth, _tileHeight);
        return tile;
    }

    void InitialGridList() {
        _gridList = new List<List<GameObject>>();
        float dx, dy;
        for (var row = 0; row < _gridRowTotal; row++) {
            dy = _tileHeight * (row - _gridRowTotal / 2);
            RowYList.Add(transform.position.y + dy);
            List<GameObject> gridRow = new List<GameObject>();
            for (var col = 0; col < _gridColumnTotal; col++) {
                dx = _tileWidth * (col - _gridColumnTotal / 2);
                gridRow.Add(CreateTile(dx, dy));
            }
            _gridList.Add(gridRow);
        }
    }

    #endregion

    #region MonoBehavior

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _gridRowTotal = StageManager.Instance.Data.GridRowTotal;
        _gridColumnTotal = StageManager.Instance.Data.GridColumnTotal;
        _tileHeight = _rectTransform.sizeDelta.y / _gridRowTotal;
        _tileWidth = _rectTransform.sizeDelta.x / _gridColumnTotal;
        _gridList = new List<List<GameObject>>();
        RowYList = new List<float>();
        _gridTilePrefab = Resources.Load<GameObject>("Prefabs/GridTile");
    }


    void Start() {
        InitialGridList();
    }

    void Update() {

    }

    #endregion
}
