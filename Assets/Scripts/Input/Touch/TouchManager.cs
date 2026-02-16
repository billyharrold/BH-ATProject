using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;


public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject circle;


    [Header("Input")]
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioRecorder audioRecorder;

    [Header("Spawnables")] public GameObject lavaBubbles;

    




    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
    }


    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {

        //if (IsTouchingUI())
        //{
        //    return;
        //}

        Vector2 touchPosition = touchPositionAction.ReadValue<Vector2>();

        Vector3 position = new Vector3(touchPosition.x, touchPosition.y, 10f);

        position = Camera.main.ScreenToWorldPoint(position);
        position.z = 0f;


       //circle.transform.position = position;
       if (audioRecorder.isRecording)
       {
           return;
       }
      PlayAudio();
      SpawnLavaBubbles(position);

    }

    private void PlayAudio()
    {

        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogWarning("AudioSource or clip is missing");
            return;
        }

        audioSource.Play();
    }

    private void SpawnLavaBubbles(Vector3 position)
    {
        Instantiate(lavaBubbles, position, Quaternion.identity);
    }

    private bool IsTouchingUI()
    {
        var uiModule = EventSystem.current.currentInputModule as InputSystemUIInputModule;

        if (uiModule == null)
        {
            return false;
        }

        return uiModule.IsPointerOverGameObject(0);


    }
}
