using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAndGrow : MonoBehaviour
{
    public float startScale;
    public float currentScale;
    public bool shrinking = true;
    public float maxScale = 2.0f;
    public float rate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        var tempRate = rate * Time.deltaTime;
        if(shrinking)
        {
            transform.localScale -= new Vector3(tempRate, tempRate, tempRate);
            currentScale = transform.localScale.x;
            if(currentScale < startScale)
            {
                shrinking = false;
            }
        }
        else
        {
            transform.localScale += new Vector3(tempRate, tempRate, tempRate);
            currentScale = transform.localScale.x;
            if (currentScale > maxScale)
            {
                shrinking = true;
            }
        }

    }
}
