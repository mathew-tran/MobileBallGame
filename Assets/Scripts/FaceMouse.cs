using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    private Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //normalize difference  
        diff.Normalize();

        //calculate rotation
        var rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

}
