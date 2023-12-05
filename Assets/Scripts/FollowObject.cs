using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform ObjectToFollow;
    private float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        yOffset = transform.position.y - ObjectToFollow.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraYOffset = yOffset + ObjectToFollow.transform.position.y;
        transform.position = new Vector3(ObjectToFollow.transform.position.x, cameraYOffset, ObjectToFollow.transform.position.z);
    }
}
