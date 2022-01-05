using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : Unit {
    #region Data

    static GameObject _allyUnitPrefab;
    public Transform Tile { get; protected set; }

    BoxCollider2D _boxCollider2D;
    bool _isActivate;

    #endregion
    #region Method

    void Initial(int id, Transform tile = null) {
        Id = id;
        Tile = tile;
        if (id == 0) _pictureSrc = "Images/Stage/1x/laser_cannon";
        else if (id >= 1) _pictureSrc = "Images/Ally/1x/Ally_" + id;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _isActivate = true;
        _speed = 0.0f;

        /// lazer beam
        if (Id == 11) _speed = 20.0f;

        /// set PixelPerUnit by Screen height and camera size (half screen height of units, default is 5)
        Texture2D texture = Resources.Load<Texture2D>(_pictureSrc);
        Debug.Log("Spawn Unit: " + Screen.height + " " + texture.height);
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            (float)Screen.height / 10
        );
        _spriteRenderer.sprite = sprite;
    }

    void HitEnemy(GameObject enemy) {
        if (Id == 11) { /// lazer beam
            Destroy(enemy);
        }
        else if (_isActivate) {
            _isActivate = false;
            if (Id == 0) {
                Debug.Log("cannon!!");
                _boxCollider2D.size = new Vector2(100.0f, _boxCollider2D.size.y);
                Destroy(Spawn(11, transform.position), 5.0f);
                _spriteRenderer.color = new Color(
                    _spriteRenderer.color.r,
                    _spriteRenderer.color.g,
                    _spriteRenderer.color.b,
                    0.2f
                );
            }
            else if (Id == enemy.GetComponent<EnemyUnit>().Id) {
                Destroy(enemy);
                if (Tile != null) Tile.GetComponent<StageGridTile>().Clear();
            }
        }
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
        go.GetComponent<AllyUnit>().Initial(id, tile);
        return go;
    }

    static public GameObject Spawn(int id, Vector3 position) {
        if (_allyUnitPrefab == null) {
            _allyUnitPrefab = Resources.Load<GameObject>("Prefabs/Stage/AllyUnit");
        }
        GameObject go = GameObject.Instantiate(
            _allyUnitPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<AllyUnit>().Initial(id);
        return go;
    }

    #endregion
    #region MonoBehaviour

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyUnit") {
            HitEnemy(collision.gameObject);
        }
    }

    void FixedUpdate() {
        _rigidbody2D.velocity = new Vector2(_speed, 0.0f);
    }

    #endregion
}
