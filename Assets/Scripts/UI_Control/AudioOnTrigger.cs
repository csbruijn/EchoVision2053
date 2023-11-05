using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioSource sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NewSoundTag"))
        {
            sound.Play();
        }
    }
}
