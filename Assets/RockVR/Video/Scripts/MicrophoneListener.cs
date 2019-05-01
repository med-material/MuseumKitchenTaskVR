// script retrieved from https://answers.unity.com/questions/1113690/microphone-input-in-unity-5x.html
// Written in part by Benjamin Outram
// modified by Bianca, removed some functiona

using UnityEngine;
using System.Collections;
using UnityEngine.Audio; // required for dealing with audiomixers
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneListener : MonoBehaviour
{
    private bool microphoneListenerOn = true;
    private bool stopMicrophoneListener = false;

    private float timeSinceRestart = 0;

    public static AudioSource src; // used in VideoPlayer.cs

    //make an audio mixer from the "create" menu, then drag it into the public field on this script.
    //double click the audio mixer and next to the "groups" section, click the "+" icon to add a 
    //child to the master group, rename it to "microphone".  Then in the audio source, in the "output" option, 
    //select this child of the master you have just created.
    //go back to the audiomixer inspector window, and click the "microphone" you just created, then in the 
    //inspector window, right click "Volume" and select "Expose Volume (of Microphone)" to script,
    //then back in the audiomixer window, in the corner click "Exposed Parameters", click on the "MyExposedParameter"
    //and rename it to "Volume"
    [SerializeField] private AudioMixer masterMixer;

    void Start()
    {
        RestartMicrophoneListener();

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }

    void Update()
    {
        MicrophoneIntoAudioSource(microphoneListenerOn);
    }

    // restart microphone removes the clip from the audiosource
    public void RestartMicrophoneListener()
    {
        src = GetComponent<AudioSource>();

        //remove any soundfile in the audiosource
        src.clip = null;

        timeSinceRestart = Time.time;

        //reset parameter to false because only want to execute once
        //startMicrophoneListener = false;
        microphoneListenerOn = true;
    }

    //puts the mic into the audiosource
    void MicrophoneIntoAudioSource(bool MicrophoneListenerOn)
    {
        if (MicrophoneListenerOn)
        {
            //pause a little before setting clip to avoid lag and bugginess
            if (Time.time - timeSinceRestart > 0.5f && !Microphone.IsRecording(null))
            {
                src.clip = Microphone.Start("3- USB Audio Device", true, 3600, 44100); // error? Built-in Microphone

                //wait until microphone position is found (?)
                while (!(Microphone.GetPosition(null) > 0)) { }

                src.Play(); // Play the audio source
            }
        }
    }

}