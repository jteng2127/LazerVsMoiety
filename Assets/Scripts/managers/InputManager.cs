using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            // RaycastHit hit;
            // Debug.DrawRay(ray.origin, ray.direction*10);
            // if(Physics.Raycast(ray, out hit)){
            //     Debug.Log("hit.collider.name");
            //     Debug.Log(hit.collider.name);
            // }

            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.DrawRay(ray.origin, ray.direction*20);
            if(touch.phase == TouchPhase.Ended){
                RaycastHit2D[] hitAll = Physics2D.RaycastAll(ray.origin, ray.direction);
                foreach(RaycastHit2D hit in hitAll){
                    if(hit.collider.tag == "defendTile"){
                        hit.collider.gameObject.GetComponent<DefendTile>().set_defend_unit(1);
                        // Instantiate(cardPrefab,
                        //             hit.collider.gameObject.transform.position,
                        //             Quaternion.identity);
                        // Debug.Log("hit " + hit.collider.transform.position);
                        // Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
