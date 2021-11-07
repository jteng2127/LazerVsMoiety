using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendGrid : MonoBehaviour
{
    [SerializeField]
    GameObject defendTilePrefab;
    public int tileRowTotal = 5;
    public int tileColumnTotal = 9;

    private List<List<GameObject> > defendTile = new List<List<GameObject> >();
    private float tileWidth = 0;
    private float tileHeight = 0;

    void Awake()
    {
        // BoxCollider2D box = defendTilePrefab.GetComponent<BoxCollider2D>();
        // tileWidth = box.size.x;
        // tileHeight = box.size.y;
        BoxCollider2D totalBox = GetComponent<BoxCollider2D>();
        tileWidth = totalBox.size.x / tileColumnTotal;
        tileHeight = totalBox.size.y / tileRowTotal;
        for(int i = 0; i < tileRowTotal; i++){
            List<GameObject> tileCol = new List<GameObject>();
            for(int j = 0; j < tileColumnTotal; j++){
                Vector3 deltaPosition = new Vector3(tileWidth*(j-tileColumnTotal/2), tileHeight*(i-tileRowTotal/2), 0);
                tileCol.Add(Instantiate(
                    defendTilePrefab,
                    transform.position + deltaPosition,
                    Quaternion.identity,
                    gameObject.transform
                    ) as GameObject);
                tileCol[j].GetComponent<BoxCollider2D>().size = new Vector2(tileWidth, tileHeight);
            }
            defendTile.Add(tileCol);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
