using UnityEngine;

public class UIController : MonoBehaviour
{

    public AudioRecorder audioRecorder;

    public GameObject talkPanel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (audioRecorder == null)
        {
            audioRecorder = FindAnyObjectByType<AudioRecorder>();
        }
    }

    void Start()
    {
        CheckUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUI();
    }

    private void CheckUI()
    {
        if (audioRecorder.isRecording)
        {
            talkPanel.SetActive(true);
        }
        else
        {
            talkPanel.SetActive(false);
        }
    }
}
