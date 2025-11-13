using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class RotateCameraX : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;
    InputAction _camera => _actionAsset.FindAction("CameraRotation");
    
    private float speed = 200;
    public GameObject player;
    float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (_camera.ReadValue<float>() == 0) { return; }
        
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
    }
    public void OnCameraRotation(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<float>(); 
    }
}
