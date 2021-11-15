using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            // Debug.DrawRay(ray.origin, ray.direction*20);
            if(touch.phase == TouchPhase.Ended){
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                foreach(RaycastHit2D hit in hitAll){
                    if(hit.collider.tag == "defendTile"){
                        hit.collider.gameObject.GetComponent<DefendTile>().set_defend_unit(1);
                        // Debug.Log("hit " + hit.collider.transform.position);
                    }
                }
            }
        }
    }
}
