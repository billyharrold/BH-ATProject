using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;


public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject circle;

    

    [Header("Input")]
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchHoldAction;
    private InputAction touchDoubleTap;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    public AudioRecorder audioRecorder;

    [Header("Spawnables")] public GameObject lavaBubbles;

    private bool isHolding = false;




    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
        touchHoldAction = playerInput.actions["TouchHold"];
        touchDoubleTap = playerInput.actions["TouchDoubleTap"];

        if (audioRecorder == null)
        {
            
            audioRecorder = FindAnyObjectByType<AudioRecorder>();
        }
        

      
    }


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        touchPressAction.performed += TouchPressed;
        touchPressAction.canceled += TouchReleased;
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        touchPressAction.performed -= TouchPressed;
        touchPressAction.canceled -= TouchReleased;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = Vector2.zero;
        if (Touch.activeTouches.Count > 0)
        {
            touchPosition = Touch.activeTouches[0].screenPosition;
        }
        else
        {
            touchPosition = touchPositionAction.ReadValue<Vector2>();
        }


        if (IsTouchingUI(touchPosition))
        {
            return;
        }

        if (audioRecorder.isRecording)
        {
            return;
        }

        isHolding = true;
        PlayAudio(lowPitch: true);

        //Vector2 touchPosition = touchPositionAction.ReadValue<Vector2>();

        Vector3 position = new Vector3(touchPosition.x, touchPosition.y, 10f);

        position = Camera.main.ScreenToWorldPoint(position);
        position.z = 0f;


        //circle.transform.position = position;


        SpawnLavaBubbles(position);

    }

    private void TouchReleased(InputAction.CallbackContext context)
    {
        isHolding = false;
        if (audioSource.clip != null)
        {
            audioSource.pitch = 1f;
        }
    }

    private void PlayAudio(bool lowPitch = false)
    {

        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogWarning("AudioSource or clip is missing");
            return;
        }

        audioSource.pitch = lowPitch ? 0.8f : 1f;
        audioSource.Play();
    }

    private void SpawnLavaBubbles(Vector3 position)
    {
        Instantiate(lavaBubbles, position, Quaternion.identity);
    }

    private bool IsTouchingUI(Vector2 screenPosition)
    {
       if (EventSystem.current == null)
        {
            return false;
        }

        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;

    }
}
