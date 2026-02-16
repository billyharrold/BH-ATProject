using System.Collections;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedAudioClip;

    [SerializeField] AudioSource audioSource;

    public void StartRecording()
    {
        //string device = Microphone.devices[0];
        //int sampleRate = 44100; 
        //int sampleLength = 3;

        //recordedAudioClip = Microphone.Start(device, false, sampleLength, sampleRate);
        //Debug.Log("Recording started");

        //if (recordedAudioClip != null)
        //{
        //    Debug.Log("Recording end");
        //    Microphone.End(null);
        //    audioSource.clip = recordedAudioClip;
        //}

        StartCoroutine(RecordAudio());

    }

    private IEnumerator RecordAudio()
    {
        string device = Microphone.devices[0];
        int sampleRate = 44100;
        int sampleLength = 3;

        recordedAudioClip = Microphone.Start(device, false, sampleLength, sampleRate);
        //Debug.Log("Recording started");

        yield return new WaitForSeconds(sampleLength);

        Microphone.End(null);
        audioSource.clip = recordedAudioClip;
    }

    // Debug now I've condensed down the functions to 1 button
    public void StopRecording()
    {
        Debug.Log("Recording stopped");
        if (recordedAudioClip != null)
        {
            Microphone.End(null);
            audioSource.clip = recordedAudioClip;
        }
    }

    // Debug now I've condensed down the functions to 1 button
    public void PlayRecording()
    {
        Debug.Log("Playing clip");
        audioSource.clip = recordedAudioClip;
        audioSource.Play();
    }

}
