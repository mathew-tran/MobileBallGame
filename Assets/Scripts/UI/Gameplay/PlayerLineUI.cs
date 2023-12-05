using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLineUI : MonoBehaviour
{
    public Player playerReference;
    public LineRenderer line;

    public void Start()
    {
        playerReference = FindObjectOfType<Player>();
        playerReference.OnTarget += OnTarget;
    }

    public void OnTarget()
    {
        var stick = playerReference.joystick;
        var lineLength = playerReference.charge * 0.03f;
        line.SetPosition(1, new Vector3(stick.Horizontal * lineLength, 0, stick.Vertical * lineLength));
        
    }
}
