using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendTile : MonoBehaviour
{
    public GameObject cardPrefab;
    private int unit_id = 0;
    private GameObject unit_object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_defend_unit(int _id){
        if(unit_id == 0){
            unit_id = _id;
            unit_object = Instantiate(cardPrefab,
                                      transform.position,
                                      Quaternion.identity);
        }
        else remove_unit();
    }

    public void remove_unit(){
        if(unit_id != 0){
            unit_id = 0;
            Destroy(unit_object);
        }
    }
}
