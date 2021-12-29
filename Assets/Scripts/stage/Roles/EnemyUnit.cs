using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyUnit : Unit
{
    #region Data

    static public GameObject _enemyPrefab;

    #endregion

    #region Method

    void Initial(int id){
        _id = id;
        _pictureSrc = "Images/Enemy/0.5x/enemy_" + id + "@0.5x";
        _sprite = GetComponent<SpriteRenderer>();

        _sprite.sprite = Resources.Load<Sprite>(_pictureSrc);
    }

    #endregion

    #region Interface

    static public GameObject Spawn(int id, Vector3 position){
        if(_enemyPrefab == null){
            _enemyPrefab = Resources.Load<GameObject>("Prefabs/Stage/EnemyUnit");
        }
        Debug.Log(_enemyPrefab);
        GameObject go = GameObject.Instantiate(
            _enemyPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<EnemyUnit>().Initial(id);
        return go;
    }

    #endregion
}