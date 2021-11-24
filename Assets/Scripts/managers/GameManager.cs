using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defendGrid;
    public const int tileRowTotal = 5;
    public const int tileColumnTotal = 9;

    private GameObject[,] defendTileArray = new GameObject[tileRowTotal, tileColumnTotal];

    public float enemySpawnInterval;
    private float enemySpawnDelay;

    void Start()
    {
        create_tiles();
        if(enemySpawnInterval == 0.0f) enemySpawnInterval = 3.0f;
    }

    void Update()
    {
        if(enemySpawnDelay <= 0.0f) enemySpawnDelay = enemySpawnInterval;
        enemySpawnDelay -= Time.deltaTime;
        if(enemySpawnDelay <= 0.0f){
            defendGrid.GetComponent<DefendGrid>().spawn_enemy(Random.Range(0, 5));
            Debug.Log(Random.Range(0, 5));
        }
    }

    void create_tiles(){
        for(int i = 0; i < tileRowTotal; i++){
            for(int j = 0; j < tileColumnTotal; j++){
                defendTileArray[i, j] = defendGrid.GetComponent<DefendGrid>().create_tile(i, j);
            }
        }
    }
}
