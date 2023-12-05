using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    public void FixedUpdate()
    {
        transform.Rotate(0, 10, 0);
    }
}
