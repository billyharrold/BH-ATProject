using UnityEngine;

public class UIController : MonoBehaviour
{

    public AudioRecorder audioRecorder;

    public GameObject talkPanel;

    public GameObject helpPanel;

    private bool isPanelActive = false;



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
        helpPanel.SetActive(false);
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


    public void ShowPanel()
    {
        helpPanel.SetActive(true);
        talkPanel.SetActive(false);
    }

    public void HidePanel()
    {
        helpPanel.SetActive(false);
    }
}
