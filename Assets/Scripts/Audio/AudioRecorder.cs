using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedAudioClip;

    [SerializeField] AudioSource audioSource;

    public void StartRecording()
    {
        string device = Microphone.devices[0];
        int sampleRate = 44100; 
        int sampleLength = 10;

        recordedAudioClip = Microphone.Start(device, false, sampleLength, sampleRate);
        Debug.Log("Recording started");
    }

    public void StopRecording()
    {
        Debug.Log("Recording stopped");
        if (recordedAudioClip != null)
        {
            Microphone.End(null);
            audioSource.clip = recordedAudioClip;
        }
    }

    public void PlayRecording()
    {
        Debug.Log("Playing clip");
        audioSource.clip = recordedAudioClip;
        audioSource.Play();
    }

}
