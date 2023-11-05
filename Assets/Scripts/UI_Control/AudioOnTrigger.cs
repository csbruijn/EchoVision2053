using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioSource sound;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SoundCollision")
        {
            sound.Play();
        }
    }
}
