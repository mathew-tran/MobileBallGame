using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCustom : MonoBehaviour
{

    public Transform player;
    public float speed = 5.0f;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
        }
        if(Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
        }
        else
        {
            touchStart = false;
        }
        */
    }

    private void FixedUpdate()
    {
        if(touchStart)
        {
            Vector3 offset = pointB - pointA;
            Vector3 direction = Vector3.ClampMagnitude(offset, 1.0f);
            Debug.Log(direction);
            moveCharacter(direction * -1);
        }
    }

    void moveCharacter(Vector3 direction)
    {

        player.Translate(direction * speed * Time.deltaTime);
    }
}
