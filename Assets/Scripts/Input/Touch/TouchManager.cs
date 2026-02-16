using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject circle;


    [Header("Input")]
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

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
       Vector3 position = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
       
       position.z = 0f;
       //circle.transform.position = position;

      PlayAudio();

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
}
