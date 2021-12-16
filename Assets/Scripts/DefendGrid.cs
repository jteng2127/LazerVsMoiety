using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject defendTilePrefab;
    [SerializeField]
    private GameObject enemyPrefab;

    private float tileWidth = 0;
    private float tileHeight = 0;

    public float enemySpawnX;

    void Awake()
    {
        BoxCollider2D totalBox = GetComponent<BoxCollider2D>();
        tileWidth = totalBox.size.x / GameManager.tileColumnTotal;
        tileHeight = totalBox.size.y / GameManager.tileRowTotal;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public GameObject create_tile(int row, int col){
        float dx = tileWidth*(col-GameManager.tileColumnTotal/2);
        float dy = tileHeight*(row-GameManager.tileRowTotal/2);
        Vector3 deltaPosition = new Vector3(dx, dy, 0);
        GameObject tile = Instantiate(
            defendTilePrefab,
            transform.position + deltaPosition,
            Quaternion.identity,
            gameObject.transform
        ) as GameObject;
        tile.GetComponent<BoxCollider2D>().size = new Vector2(tileWidth, tileHeight);
        return tile;
    }

    public GameObject spawn_enemy(int row){
        float dy = tileHeight*(row-GameManager.tileRowTotal/2);
        Vector3 deltaPosition = new Vector3(enemySpawnX, dy, 0);
        GameObject enemy = Instantiate(
            enemyPrefab,
            transform.position + deltaPosition,
            Quaternion.identity,
            gameObject.transform
        ) as GameObject;
        return enemy;
    }
}
