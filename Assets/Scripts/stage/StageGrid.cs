using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour {

    #region Data

    RectTransform _rectTransform;

    int _gridRowTotal;
    int _gridColumnTotal;
    public float TileHeight { get; protected set; }
    public float TileWidth { get; protected set; }
    List<List<GameObject>> _gridList;
    public List<float> RowYList { get; set; }
    GameObject _stageGridTilePrefab;

    #endregion

    #region method

    GameObject CreateTile(float dx, float dy) {
        Vector3 deltaPosition = new Vector3(dx, dy, 0);
        GameObject tile = Instantiate(
            _stageGridTilePrefab,
            transform.position + deltaPosition,
            Quaternion.identity,
            gameObject.transform
        ) as GameObject;
        tile.GetComponent<BoxCollider2D>().size = new Vector2(TileWidth, TileHeight);
        tile.tag = "StageGridTile";
        return tile;
    }

    void InitialGridList() {
        _gridList = new List<List<GameObject>>();
        float dx, dy;
        for (var row = 0; row < _gridRowTotal; row++) {
            dy = TileHeight * (row - _gridRowTotal / 2);
            AllyUnit.Spawn(0, transform.position + new Vector3(
                TileWidth * (-1 - _gridColumnTotal / 2),
                dy
            ));

            RowYList.Add(transform.position.y + dy);
            List<GameObject> gridRow = new List<GameObject>();
            for (var col = 0; col < _gridColumnTotal; col++) {
                dx = TileWidth * (col - _gridColumnTotal / 2);
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
        TileHeight = _rectTransform.sizeDelta.y / _gridRowTotal;
        TileWidth = _rectTransform.sizeDelta.x / _gridColumnTotal;
        _gridList = new List<List<GameObject>>();
        RowYList = new List<float>();
        _stageGridTilePrefab = Resources.Load<GameObject>("Prefabs/Stage/StageGridTile");
    }


    void Start() {
        InitialGridList();
    }

    void Update() {

    }

    #endregion
}
