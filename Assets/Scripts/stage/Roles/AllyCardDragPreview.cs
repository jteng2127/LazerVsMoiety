using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCardDragPreview : MonoBehaviour {
    public GameObject Origin { get; set; }
    public Vector3 CurrentPosition { get; set; }
    Vector3 _lastMousePosition;

    StageGridTile GetHitGrid(Ray ray) {
        StageGridTile stageGridTile = null;
        RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
        foreach (RaycastHit2D hit in hitAll) {
            if (hit.collider.tag == "StageGridTile" &&
                hit.collider.GetComponent<StageGridTile>().CurrentAllyId == -1) {
                stageGridTile = hit.collider.GetComponent<StageGridTile>();
                break;
            }
        }
        return stageGridTile;
    }

    void Dragging(Vector3 deltaPos, Ray ray) {
        CurrentPosition += deltaPos;

        StageGridTile stageGridTile = GetHitGrid(ray);
        if (stageGridTile == null) transform.position = CurrentPosition;
        else transform.position = stageGridTile.gameObject.transform.position;
    }

    void EndDragging(Ray ray) {
        StageGridTile stageGridTile = GetHitGrid(ray);
        if (stageGridTile == null) transform.position = CurrentPosition;
        else transform.position = stageGridTile.gameObject.transform.position;

        if (stageGridTile != null) {
            AllyCard allyCard = Origin.GetComponent<AllyCard>();
            SpawnManager.DestroyUnit(allyCard.gameObject);
            stageGridTile.SetAllyUnit(allyCard.Id);
        }
        else {
            SpriteRenderer originSpriteRenderer = Origin.GetComponent<SpriteRenderer>();
            originSpriteRenderer.color = new Color(
                originSpriteRenderer.color.r,
                originSpriteRenderer.color.g,
                originSpriteRenderer.color.b,
                1.0f
            );
        }
        Destroy(transform.gameObject);
    }

    void Update() {
        Ray ray;

        /// touch
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Moved) {
                Vector3 deltaPos =
                    Camera.main.ScreenToWorldPoint(touch.position + touch.deltaPosition) -
                    Camera.main.ScreenToWorldPoint(touch.position);
                Dragging(deltaPos, ray);
            }
            else if (touch.phase == TouchPhase.Ended) {
                EndDragging(ray);
            }
        }
        else{
            /// mouse
            if(Input.GetMouseButton(0)){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Input.GetMouseButtonDown(0)) { }
                else{
                    if (Input.mousePosition != _lastMousePosition && _lastMousePosition != new Vector3(0, 0, 0)) {
                        Vector3 deltaPos =
                            Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                            Camera.main.ScreenToWorldPoint(_lastMousePosition);
                        Dragging(deltaPos, ray);
                    }
                }
            }
            else if(Input.GetMouseButtonUp(0)){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                EndDragging(ray);
                Destroy(transform.gameObject);
            }
            _lastMousePosition = Input.mousePosition;
        }
    }
}
