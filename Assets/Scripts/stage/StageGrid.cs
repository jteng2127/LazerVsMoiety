using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour {
    Renderer _renderer;
    RectTransform _rectTransform;

    readonly int _gridRowTotal = 5;
    readonly int _gridColumnTotal = 9;
    float _tileHeight;
    float _tileWidth;
    List<List<GameObject>> _gridList;
    GameObject _gridTilePrefab;

    void Awake() {
        _renderer = GetComponent<Renderer>();
        _rectTransform = GetComponent<RectTransform>();
        _tileHeight = _rectTransform.sizeDelta.y / _gridRowTotal;
        _tileWidth = _rectTransform.sizeDelta.x / _gridColumnTotal;
        _gridTilePrefab = Resources.Load<GameObject>("Prefabs/GridTile");
        Debug.Log("StageGrid: " + _renderer.bounds.size + " " + _tileHeight);
    }

    GameObject CreateTile(int row, int col) {
        float dy = _tileHeight * (row - _gridRowTotal / 2);
        float dx = _tileWidth * (col - _gridColumnTotal / 2);
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
        for (var i = 0; i < _gridRowTotal; i++) {
            List<GameObject> gridRow = new List<GameObject>();
            for (var j = 0; j < _gridColumnTotal; j++) {
                gridRow.Add(CreateTile(i, j));
            }
            _gridList.Add(gridRow);
        }
        Debug.Log("grid init " + _gridList.Count + " " + _gridList[0][0]);
    }

    void Start() {
        InitialGridList();
    }

    void Update() {

    }
}
