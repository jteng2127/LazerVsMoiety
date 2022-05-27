using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {
    #region Data

    static GameObject _enemyUnitPrefab;

    #endregion
    #region Method

    void Initial(int id, float speed) {
        Id = id;
        _pictureSrc = "Images/Enemy/1x/enemy_" + id;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = speed;

        /// set PixelPerUnit by Screen height and camera size (half screen height of units, default is 5)
        Texture2D texture = Resources.Load<Texture2D>(_pictureSrc);
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            GameManager.ImagePixelHeightReference / 10
        );
        _spriteRenderer.sprite = sprite;
    }

    #endregion

    #region Interface

    static public GameObject Spawn(int id, Vector3 position, float speed) {
        if (_enemyUnitPrefab == null) {
            _enemyUnitPrefab = Resources.Load<GameObject>("Prefabs/Stage/EnemyUnit");
        }
        GameObject go = GameObject.Instantiate(
            _enemyUnitPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<EnemyUnit>().Initial(id, speed);
        return go;
    }

    #endregion

    #region MonoBehaviour

    void FixedUpdate() {
        if (IsPaused) {
            _rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        } else {
            _rigidbody2D.velocity = new Vector2(-_speed, 0.0f);
        }
    }

    void Update() {
        Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPortPoint.x < 0.0) {
            StageManager.Instance.TriggerGameLose();
            Debug.Log("Game Lose by Enemy");
        }
    }

    #endregion
}