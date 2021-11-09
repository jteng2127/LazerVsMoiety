using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defendGrid;
    private List<List<DefendTile> > defendTileList = new List<List<DefendTile> >();
    public static int tileRowTotal = 5;
    public static int tileColumnTotal = 9;
    private GameObject[,] defendTileArray = new GameObject[tileRowTotal, tileColumnTotal];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(10);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void create_tiles(){
        for(int i = 0; i < tileRowTotal; i++){
            List<DefendTile> tileCol = new List<DefendTile>();
            for(int j = 0; j < tileColumnTotal; j++){
                defendTileArray[i, j] = defendGrid.GetComponent<DefendGrid>().create_tile(i, j);
            }
            defendTileList.Add(tileCol);
        }
    }
}
