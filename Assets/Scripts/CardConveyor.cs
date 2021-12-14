using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConveyor : MonoBehaviour {
  public GameObject cardPrefab;
  private Vector3 cardSpawnPositionDelta;

  void Start() {
    float conveyorWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    float cardWidth = cardPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    cardSpawnPositionDelta = new Vector3(conveyorWidth / 2 + cardWidth / 2 + 0.1f, 0.0f, 0.0f);
    Debug.Log("cardSpawnPositionDelta" + cardSpawnPositionDelta);
  }

  public GameObject spawn_card() {
    GameObject card = Instantiate(
      cardPrefab,
      transform.position + cardSpawnPositionDelta,
      Quaternion.identity
    ) as GameObject;
    return card;
  }
}
