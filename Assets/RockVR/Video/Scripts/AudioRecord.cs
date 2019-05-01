using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecord : MonoBehaviour
{

    public static AudioSource audioSource; // used in VideoPlayer.cs

    // Start recording with built-in Microphone and play the recorded audio right away
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start("Built-in Microphone", true, 3600, 44100); //10, 44100);
        audioSource.Play();
    }
}
