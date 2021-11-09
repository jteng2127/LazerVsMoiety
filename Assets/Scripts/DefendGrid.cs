using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendGrid : MonoBehaviour
{
    [SerializeField]
    GameObject defendTilePrefab;
    public int tileRowTotal = 5;
    public int tileColumnTotal = 9;

    // private List<List<GameObject> > defendTile = new List<List<GameObject> >();
    private float tileWidth = 0;
    private float tileHeight = 0;

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public GameObject create_tile(int row, int col){
        BoxCollider2D totalBox = GetComponent<BoxCollider2D>();
        tileWidth = totalBox.size.x / tileColumnTotal;
        tileHeight = totalBox.size.y / tileRowTotal;

        float dx = tileWidth*(col-tileColumnTotal/2);
        float dy = tileHeight*(row-tileRowTotal/2);
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
}
