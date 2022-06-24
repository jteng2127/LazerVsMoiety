using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCard : Unit {
    #region Data

    static GameObject _allyCardPrefab;

    #endregion
    #region Method

    void Initial(int id, float speed) {
        Id = id;
        _pictureSrc = "Images/Ally/1x/Ally_" + Id;
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
        if (_allyCardPrefab == null) {
            _allyCardPrefab = Resources.Load<GameObject>("Prefabs/Stage/AllyCard");
        }
        GameObject go = GameObject.Instantiate(
            _allyCardPrefab,
            position,
            Quaternion.identity
        );
        go.GetComponent<AllyCard>().Initial(id, speed);
        return go;
    }

    public GameObject CreateDragPreview() {
        _spriteRenderer.color = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            0.2f
        );

        GameObject go = new GameObject("AllyCardDragPreview", typeof(SpriteRenderer), typeof(AllyCardDragPreview));
        go.transform.position = transform.position;
        go.transform.localScale = transform.localScale;
        go.tag = "AllyCardDragPreview";

        SpriteRenderer goSpriteRenderer = go.GetComponent<SpriteRenderer>();
        goSpriteRenderer.sprite = _spriteRenderer.sprite;
        goSpriteRenderer.sortingLayerName = "UI";
        goSpriteRenderer.sortingOrder = 10;

        AllyCardDragPreview goAllyCardDragPreview = go.GetComponent<AllyCardDragPreview>();
        goAllyCardDragPreview.Origin = transform.gameObject;
        goAllyCardDragPreview.CurrentPosition = transform.position;

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