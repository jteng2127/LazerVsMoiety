using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public const int tileRowTotal = 5;
  public const int tileColumnTotal = 9;

  private GameObject[,] defendTileArray = new GameObject[tileRowTotal, tileColumnTotal];

  public GameObject defendGridObject;
  private DefendGrid defendGrid;
  public float enemySpawnInterval;
  private float enemySpawnDelay;

  public GameObject cardConveyorObject;
  private CardConveyor cardConveyor;
  public float cardSpawnInterval;
  private float cardSpawnDelay;

  void Start() {
    defendGrid = defendGridObject.GetComponent<DefendGrid>();
    cardConveyor = cardConveyorObject.GetComponent<CardConveyor>();
    if (enemySpawnInterval == 0.0f) enemySpawnInterval = 3.0f;
    if (cardSpawnInterval == 0.0f) cardSpawnInterval = 3.0f;
    create_tiles();
  }

  void Update() {
    // enemy spawn
    if (enemySpawnDelay <= 0.0f) enemySpawnDelay = enemySpawnInterval;
    enemySpawnDelay -= Time.deltaTime;
    if (enemySpawnDelay <= 0.0f) {
      int spawnRow = Random.Range(0, 5);
      defendGrid.spawn_enemy(spawnRow);
      Debug.Log("Enemy spawn at " + spawnRow);
    }

    // card spawn
    if (cardSpawnDelay <= 0.0f) cardSpawnDelay = cardSpawnInterval;
    cardSpawnDelay -= Time.deltaTime;
    if (cardSpawnDelay <= 0.0f) {
      cardConveyor.spawn_card();
    }
  }

  void create_tiles() {
    for (int i = 0; i < tileRowTotal; i++) {
      for (int j = 0; j < tileColumnTotal; j++) {
        defendTileArray[i, j] = defendGrid.create_tile(i, j);
      }
    }
  }
}
