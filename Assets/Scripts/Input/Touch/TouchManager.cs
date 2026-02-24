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
    private InputAction touchHoldAction;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioRecorder audioRecorder;

    [Header("Spawnables")] public GameObject lavaBubbles;

    private bool isHolding = false;




    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
        touchHoldAction = playerInput.actions["TouchHold"];
    }


    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        touchPressAction.canceled += TouchReleased;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        touchPressAction.canceled -= TouchReleased;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {

        if (IsTouchingUI())
        {
            return;
        }

        if (audioRecorder.isRecording)
        {
            return;
        }

        isHolding = true;

        Vector2 touchPosition = touchPositionAction.ReadValue<Vector2>();

        Vector3 position = new Vector3(touchPosition.x, touchPosition.y, 10f);

        position = Camera.main.ScreenToWorldPoint(position);
        position.z = 0f;


       //circle.transform.position = position;
     
      PlayAudio(lowPitch: true);
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

    private bool IsTouchingUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.press.isPressed)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.touchId.ReadValue()))
                {
                    Debug.Log("Touching UI!");
                    return true;
                }
            }
        }

       

        return false;
    }
}
