using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f,  -0.1f));
    }
}
