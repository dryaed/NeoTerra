using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Adapt to touchscreen
public class PlayerCamera : MonoBehaviour
{
    public TouchManager touchManager;
    
    [SerializeField]
    private float sensitivity = 50f;
    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private PlayerController playerController;

    float verticalRotation = 0f;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        touchManager = FindObjectOfType<TouchManager>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // float mouseX = playerController.MousePosition.x * sensitivity * Time.deltaTime;
        // float mouseY = playerController.MousePosition.y * sensitivity * Time.deltaTime;

        Vector2 cameraNigger = touchManager.CameraMovement;
        
        Debug.Log($"x: {cameraNigger.x} y: {cameraNigger.y}");
        
        float mouseX = cameraNigger.x * sensitivity * Time.deltaTime;
        float mouseY = cameraNigger.y * sensitivity * Time.deltaTime;
        
        Debug.Log($"mouseX: {mouseX}, mouseY: {mouseY}");

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
