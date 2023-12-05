using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource source;
    public void Play()
    {
        AudioManager._instance.PlaySFX(source.clip);
    }

}
