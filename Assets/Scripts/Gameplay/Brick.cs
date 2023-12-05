using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager mManagerReference;

    public void Start()
    {
        mManagerReference = FindObjectOfType<GameManager>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            gameObject.AddComponent<Shrink>();
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            var addVelocity = Vector3.ClampMagnitude(rb.velocity * 20f, 20f);
            rb.AddForce(addVelocity);
            mManagerReference.AddTime(5.0f);
            mManagerReference.RemoveBlock();
            Destroy(this.gameObject, 0.3f);
        }
    }
}
