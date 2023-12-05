using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    public float shrinkRatePerSecond = 0.1f;
    public void Update()
    {
        if (gameObject.transform.localScale.x > 0.0f)
        {
            gameObject.transform.localScale -= new Vector3(shrinkRatePerSecond, shrinkRatePerSecond, shrinkRatePerSecond);            
        }
    }
}
