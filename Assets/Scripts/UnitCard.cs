using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : MonoBehaviour {

  public float speed;
  private SpriteRenderer sprite;

  void Start() {
    sprite = GetComponent<SpriteRenderer>();
  }

  void FixedUpdate() {
    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0.0f);
  }

  public GameObject generate_drag_preview() {
    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);

    GameObject go = new GameObject("cardPreview", typeof(SpriteRenderer), typeof(CanvasGroup));
    go.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
    go.GetComponent<CanvasGroup>().alpha = .6f;
    go.transform.position = transform.position;
    go.transform.localScale = transform.localScale;
    go.tag = "UnitCardPreview";
    return go;
  }
}
