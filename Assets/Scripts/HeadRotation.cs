using UnityEngine;
using UnityEngine.InputSystem;

public class HeadRotation : MonoBehaviour
{
    private float _currentRotation;
    private InputSystem_Actions input = null;

    //Rotation
    private float sensitivity = 0.30f;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        input.Enable();
        input.Player.CameraHorizontal.performed += Rotate;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.CameraHorizontal.performed -= Rotate;
    }


    private void Rotate(InputAction.CallbackContext value)
    {
        _currentRotation += value.ReadValue<float>() * sensitivity;
        _currentRotation = Mathf.Repeat(_currentRotation, 360);
        transform.localRotation = Quaternion.Euler(0, _currentRotation, 0);
    }

    public Vector3 GetRotatedForwardVector()
    {
        return transform.forward;
    }

    public Vector3 GetRotatedRightVector()
    {
        return transform.right;
    }
    
}
