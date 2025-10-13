using UnityEngine;
using UnityEngine.InputSystem;

public class HeadRotationUpDown : MonoBehaviour
{
    private float _currentRotation;
    private InputSystem_Actions input = null;
    private float maxYAngle = 80f;

    private float sensitivity = 0.5f;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.CameraVertical.performed += Rotate;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.CameraVertical.performed -= Rotate;
    }

    private void Rotate(InputAction.CallbackContext value)
    {
        _currentRotation -= value.ReadValue<float>() * sensitivity;
        _currentRotation = Mathf.Clamp(_currentRotation, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(_currentRotation, 0, 0);
    }
}
