using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-3.0f, 0.0f);
    }
}
