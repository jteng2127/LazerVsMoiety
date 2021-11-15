using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defendGrid;
    public const int tileRowTotal = 5;
    public const int tileColumnTotal = 9;
    private GameObject[,] defendTileArray = new GameObject[tileRowTotal, tileColumnTotal];

    void Start()
    {
        create_tiles();

    }

    void Update()
    {

    }

    void create_tiles(){
        for(int i = 0; i < tileRowTotal; i++){
            for(int j = 0; j < tileColumnTotal; j++){
                defendTileArray[i, j] = defendGrid.GetComponent<DefendGrid>().create_tile(i, j);
            }
        }
    }
}
