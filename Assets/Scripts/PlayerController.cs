using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float gravityValue = -9.81f;
    
    InputSystem_Actions inputSystemActions;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Camera mainCamera;

    public event Action OnInteractionInput;
    public event Action OnPauseInput;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        inputSystemActions.Player.Pause.performed += _ => PlayerPauseInput();
        inputSystemActions.Player.Interact.performed += _ => PlayerInteractionInput();
    }

    private void OnEnable()
    {
        inputSystemActions.Enable();
    }

    private void OnDisable()
    {
        inputSystemActions.Disable();
    }

    private void Update()
    {
        var rawInput = inputSystemActions.Player.Move.ReadValue<Vector2>();
        var inputDir = new Vector3(rawInput.x, 0f, rawInput.y);
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);
        
        var cam = mainCamera.transform;
        
        var camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        var camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();
        
        var move = camForward * inputDir.z + camRight * inputDir.x;
        
        if (move != Vector3.zero) transform.forward = move; // Rotate toward movement direction
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        var finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        
        controller.Move(finalMove * Time.deltaTime);
        
        if (controller.isGrounded && playerVelocity.y < 0) playerVelocity.y = 0;
    }

    private void PlayerPauseInput()
    {
        OnPauseInput?.Invoke();
    }

    private void PlayerInteractionInput()
    {
        OnInteractionInput?.Invoke();
    }
}
