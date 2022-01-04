using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyUnit : Unit {
    #region Data

    static public GameObject _enemyPrefab;


    #endregion

    #region Method

    void Initial(int id) {
        Id = id;
        _pictureSrc = "Images/Enemy/1x/enemy_" + id;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _speed = StageManager.Instance.Data.EnemySpeed;

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

    static public GameObject Spawn(int id, Vector3 position) {
        if (_enemyPrefab == null) {
            _enemyPrefab = Resources.Load<GameObject>("Prefabs/Stage/EnemyUnit");
        }
        GameObject go = GameObject.Instantiate(
            _enemyPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<EnemyUnit>().Initial(id);
        return go;
    }

    #endregion

    #region MonoBehaviour

    void FixedUpdate() {
        transform.position = transform.position + new Vector3(-_speed, 0.0f, 0.0f);
    }

    #endregion
}