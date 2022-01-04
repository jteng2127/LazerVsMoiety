using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCardDragPreview : MonoBehaviour {
    public GameObject Origin { get; set; }

    void Dragging(Touch touch) {
        Vector3 deltaPos =
            Camera.main.ScreenToWorldPoint(touch.position + touch.deltaPosition) -
            Camera.main.ScreenToWorldPoint(touch.position);
        transform.position += deltaPos;
    }

    void EndDragging(Ray ray) {
        RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
        bool isHitGrid = false;
        foreach (RaycastHit2D hit in hitAll) {
            if (hit.collider.tag == "StageGridTile") {
                hit.collider.gameObject.GetComponent<StageGridTile>().SetAllyUnit(Origin.GetComponent<AllyCard>().Id);
                isHitGrid = true;
                break;
            }
        }

        if (isHitGrid) {
            Origin.GetComponent<AllyCard>().DestroySelf();
        }
        else {
            SpriteRenderer originSpriteRenderer = Origin.GetComponent<SpriteRenderer>();
            originSpriteRenderer.color = new Color(
                originSpriteRenderer.color.r, originSpriteRenderer.color.g, originSpriteRenderer.color.b, 1.0f);
        }
        Destroy(transform.gameObject);
    }

    void Update() {
        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        if (touch.phase == TouchPhase.Moved) {
            Dragging(touch);
        }
        else if (touch.phase == TouchPhase.Ended) {
            EndDragging(ray);
        }
    }
}
