using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedAudioClip;

    [SerializeField] AudioSource audioSource;

    [SerializeField] int sampleLength = 3;

    public bool isRecording = false;
    public Button recordButton;

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

        if (isRecording)
        {
            return;
        }
        StartCoroutine(RecordAudio());
    }

    private IEnumerator RecordAudio()
    {
        // permission checks for android
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            yield return new WaitForSeconds(1f);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Debug.LogError("Microphone permission denied");
            yield break;
        }
        #endif

        if (Microphone.devices.Length == 0)
        {
            Debug.Log("No microphone found");
        }


        isRecording = true;
        //Debug.Log(isRecording);
        recordButton.image.color = Color.red;
        string device = Microphone.devices[0];
        int sampleRate = 44100;
        //int sampleLength = 3;

        recordedAudioClip = Microphone.Start(device, false, sampleLength, sampleRate);
        //Debug.Log("Recording started");

        yield return new WaitForSeconds(sampleLength);

        Microphone.End(null);
        audioSource.clip = recordedAudioClip;

        isRecording = false;
        recordButton.image.color = new Color(0.84f, 0.3f, 0.3f, 1);
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

    //public void ChangeButtonIcon()
    //{

    //    if (isRecording)
    //    {
    //        recordButton.image.color = Color.red;
    //    }
    //    else
    //    {
    //        recordButton.image.color = new Color(217, 77, 77);
    //    }
    //}

}
