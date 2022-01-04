using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : Unit {
    #region Data

    static GameObject _allyUnitPrefab;

    #endregion
    #region Method

    void Initial(int id) {
        Id = id;
        _pictureSrc = "Images/Ally/1x/Ally_" + id;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // _speed = StageManager.Instance.Data.EnemySpeed;

        /// set PixelPerUnit by Screen height and camera size (half screen height of units, default is 5)
        Texture2D texture = Resources.Load<Texture2D>(_pictureSrc);
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            (float)Screen.height / 10
        );
        _spriteRenderer.sprite = sprite;
    }

    #endregion

    #region Interface

    static public GameObject Spawn(int id, Transform tile) {
        if (_allyUnitPrefab == null) {
            _allyUnitPrefab = Resources.Load<GameObject>("Prefabs/Stage/AllyUnit");
        }
        GameObject go = GameObject.Instantiate(
            _allyUnitPrefab,
            tile.position,
            Quaternion.identity,
            tile
        );
        go.GetComponent<AllyUnit>().Initial(id);
        return go;
    }

    public void DestroySelf() {
        Destroy(transform.gameObject);
    }

    #endregion

    #region MonoBehaviour

    #endregion
}
