using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveCameraAction;
    private InputAction _touchTest;
    private InputAction _movePlayer;
    private InputAction _moveCamera;
    private InputAction _jump;

    public Vector2 CameraMovement;
    public Vector3 PlayerMovement;
    public bool IsJumping { get; private set; }
    
    [SerializeField]
    private float sensitivity = 300f;
    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private PlayerController playerController;

    float verticalRotation = 0f;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();
        _moveCameraAction = _playerInput.actions["MoveCamera"];
        _touchTest = _playerInput.actions.FindAction("TouchTest");
        _movePlayer = _playerInput.actions.FindAction("MovePlayer");
        _moveCamera = _playerInput.actions.FindAction("MoveCameraNew");
        _jump = _playerInput.actions.FindAction("Jump");
    }

    private void Update()
    {
        PlayerMovement = new Vector3(_movePlayer.ReadValue<Vector2>().x, 0, _movePlayer.ReadValue<Vector2>().y);
        //CameraMovement = _moveCamera.ReadValue<Vector2>();
        var temp = _jump.ReadValue<float>();
        
        Debug.Log(temp);

        if (temp == 1.0f) IsJumping = true;
        else IsJumping = false;
        
        //Debug.Log($"Player Movement {PlayerMovement.x} {PlayerMovement.y} {PlayerMovement.z}");
        //Debug.Log($"Camera Movement {CameraMovement.x} {CameraMovement.y}");
    }

    private void OnEnable()
    {
        //_moveCameraAction.performed += MoveCamera;
        //_moveCameraAction.canceled += MoveCameraStop;

        _moveCamera.performed += MoveCamera;
        _moveCamera.canceled += MoveCameraStop;
        
        _touchTest.started += TouchTest;
    }

    private void MoveCameraStop(InputAction.CallbackContext obj)
    {
        CameraMovement = Vector2.zero;
    }

    private void OnDisable()
    {
        //_moveCameraAction.performed -= MoveCamera;
        //_moveCameraAction.canceled -= MoveCameraStop;
        
        _moveCamera.performed -= MoveCamera;
        _moveCamera.canceled -= MoveCameraStop;
        
        _touchTest.started -= TouchTest;
    }

    private void MoveCamera(InputAction.CallbackContext obj)
    {
        CameraMovement = obj.ReadValue<Vector2>();
        
        //Debug.Log($"{CameraMovement.x} {CameraMovement.y}");
        
        //CameraMovement = obj.ReadValue<Vector2>();
        //Debug.Log($"{CameraMovement.x}");
        
        // float mouseX = cameraMovement.x * sensitivity * Time.deltaTime;
        // float mouseY = cameraMovement.y * sensitivity * Time.deltaTime;
        //
        // verticalRotation -= mouseY;
        // verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        //
        // transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        // playerBody.Rotate(Vector3.up * mouseX);
    }

    private void TouchTest(InputAction.CallbackContext obj)
    {
        Vector2 touchPos = obj.ReadValue<Vector2>();
        Debug.Log($"x: {touchPos.x} y: {touchPos.y}");
    }
}
