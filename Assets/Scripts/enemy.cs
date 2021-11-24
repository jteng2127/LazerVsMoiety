using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        // GetComponent<Rigidbody2D>().velocity = new Vector2(-3.0f, 0.0f);
        transform.position = transform.position + new Vector3(-speed, 0.0f, 0.0f);
    }
}
