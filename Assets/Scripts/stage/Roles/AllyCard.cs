using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AllyCard : Unit {
    #region Data

    static GameObject _allyCardPrefab;

    Rigidbody2D _rigidbody2D;

    #endregion

    #region Method

    void Initial(int id) {
        _id = id;
        _pictureSrc = "Images/Ally/1x/Ally_" + id;
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = StageManager.Instance.Data.AllyCardSpeed;

        _sprite.sprite = Resources.Load<Sprite>(_pictureSrc);
    }

    #endregion

    #region Interface

    static public GameObject Spawn(int id, Vector3 position) {
        if (_allyCardPrefab == null) {
            _allyCardPrefab = Resources.Load<GameObject>("Prefabs/Stage/AllyCard");
        }
        Debug.Log(_allyCardPrefab);
        GameObject go = GameObject.Instantiate(
            _allyCardPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<AllyCard>().Initial(id);
        return go;
    }

    #endregion

    #region MonoBehaviour

    void FixedUpdate() {
        _rigidbody2D.velocity = new Vector2(-_speed, 0.0f);
    }

    #endregion

    /// <summary>
    /// Ally collision with Emeny
    /// </summary>
    // public void collision(int enemy_unit_id)
    // {
    //     if(_id == -1)
    //     {
    //         // mower collision
    //         // do something 
    //         GameObject enemy_unit = GameObject.Find("something");
    //         Destroy(enemy_unit);
    //         return;
    //     }

    //     // ally(wave) collision
    //     // suppose emeny_unit_id is emeny unit ID
    //     if(_id == enemy_unit_id)
    //     {
    //         Destroy(transform.gameObject);
    //     }
    // }
}