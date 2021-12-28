using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject draggingObject;

    void Start()
    {

    }

    void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            // Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            // Debug.Log(pos);
            // Debug.DrawRay(ray.origin, ray.direction*20);
            // Debug.Log(touch.deltaPosition);

            // touch detect
            if(touch.phase == TouchPhase.Began){
                Debug.Log("touch began");
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                foreach(RaycastHit2D hit in hitAll){
                    if(hit.collider.tag == "UnitCard"){
                        isDragging = true;
                        draggingObject = hit.collider.gameObject.GetComponent<UnitCard>().generate_drag_preview();
                        break;
                    }
                }
            }
            else if(touch.phase == TouchPhase.Moved){
                if(isDragging){
                    Vector3 deltaPos = 
                        Camera.main.ScreenToWorldPoint(touch.position + touch.deltaPosition) -
                        Camera.main.ScreenToWorldPoint(touch.position);
                    draggingObject.transform.position += deltaPos;
                }
            }
            else if(touch.phase == TouchPhase.Stationary){
            }
            else if(touch.phase == TouchPhase.Ended){
                Debug.Log("touch ended");
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                if(isDragging){
                    if(draggingObject.tag == "UnitCardPreview"){
                        foreach(RaycastHit2D hit in hitAll){
                            if(hit.collider.tag == "DefendTile"){
                                hit.collider.gameObject.GetComponent<DefendTile>().set_defend_unit(1);
                                break;
                            }
                        }
                        Destroy(draggingObject);
                    }
                    isDragging = false;
                }
            }
        }
    }
}
