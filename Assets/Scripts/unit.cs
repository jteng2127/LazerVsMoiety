using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(transform.gameObject);
        }
  }
}
